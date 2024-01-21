using System;
using System.Linq;
using Sandbox;
using Sandbox.Diagnostics;
using ShartCoding.ShartCode;

namespace ShartCoding.GameMaker.Engine;

public class GameMakerComponent : Component
{
	[Property] public GameObject RoomStorage;
	[Property] public GameObject RoomPreviewStorage;
	[Property] public bool Playing { get; private set; } = false;
	[Property] public RoomSunComponent Sun;
	[Property] public EditorCameraComponent EditorCamera;
	[Property] public CameraComponent GameCamera;

	public GameMakerProject Current { get; private set; }
	public Room CurrentEditorRoom { get; private set; }
	public ShartCodeContext GlobalContext { get; private set; } = new();

	[Property]
	public RoomObjectGizmoComponent EditorCurrentSelected =>
		_editorRayLastHighlight.IsValid() ? _roRayLastHighlight : null;

	public Ray? EditorRay
	{
		get => _editorRay;
		set
		{
			bool equal = _editorRay == value;
			_editorRay = value;

			if ( !equal )
				if ( _editorRay is null )
					EditorRayOff();
				else
					EditorRayTrace();
		}
	}

	private Ray? _editorRay;
	private GameObject _editorRayLastHighlight;
	private RoomObjectGizmoComponent _roRayLastHighlight;

	protected override void OnAwake()
	{
		base.OnAwake();

		Scene.GetSystem<ActorSystem>().Register( this );
	}

	public void CreateNewProject( GameMakerProjectDescription description )
	{
		// TODO: dispose the loaded project
		Current = GameMakerProjectRepository.The.Make( description );
		// TODO: make it async
		Current.Preload().Wait();
		Log.Info( Current );

		ToEditor();
		SetEditorRoom( Current.InitialRoom );
	}

	// Clean up the state
	private void ToEditor()
	{
		RoomStorage.Clear();
		RoomPreviewStorage.Enabled = true;

		EditorCamera.Enabled = true;
		GameCamera.Enabled = false;
	}

	private void ToGame()
	{
		RoomPreviewStorage.Enabled = false;
		SetRoom( Current.InitialRoom );

		GameCamera.Enabled = true;
		EditorCamera.Enabled = false;
	}

	public void Start()
	{
		if ( Current is null || Playing )
			return;

		Playing = true;

		Assert.NotNull( Current );

		ToGame();
	}

	public void Stop()
	{
		if ( Current is null || !Playing )
			return;

		Playing = false;

		Assert.NotNull( Current );

		ToEditor();
	}

	public void SetEditorRoom( Room room )
	{
		if ( Current is null || Playing || CurrentEditorRoom == room )
			return;

		if ( CurrentEditorRoom != null )
		{
			CurrentEditorRoom.OnAdded = null;
			CurrentEditorRoom.OnRemoved = null;
		}

		CurrentEditorRoom = room;

		RoomPreviewStorage.Clear();

		foreach ( var roomObject in room.RoomObjects )
		{
			EditorMakeRoomObjectGizmo( roomObject );
		}

		CurrentEditorRoom.OnAdded = EditorMakeRoomObjectGizmo;
		CurrentEditorRoom.OnRemoved = null; // TODO:

		Sun.Room = CurrentEditorRoom;
	}

	private void EditorMakeRoomObjectGizmo( RoomObject roomObject )
	{
		var go = new GameObject();
		go.Tags.Add( "actor" );
		// TODO: make a preview object with gizmo
		var gizmo = go.Components.Create<RoomObjectGizmoComponent>();
		gizmo.RoomObject = roomObject;
		go.SetParent( RoomPreviewStorage );
	}

	public void SetRoom( Room room )
	{
		if ( Current is null || !Playing )
			return;

		Sun.Room = room;

		RoomStorage.Clear();

		try
		{
			room.Populate( RoomStorage );
		}
		catch ( Exception ex )
		{
			Log.Trace( ex );
			ToEditor();
		}
	}

	private void EditorRayTrace()
	{
		if ( EditorRay is null )
			return;

		var result = Scene.Trace.UseRenderMeshes().Ray( EditorRay.Value, 1000 ).WithTag( "actor" ).Run();
		var go = result.GameObject;
		if ( go != _editorRayLastHighlight )
		{
			EditorSetRoomObject( go );
		}
	}

	private void EditorRayOff()
	{
		if ( _editorRayLastHighlight.IsValid() )
		{
			_editorRayLastHighlight.Components.Get<ModelRenderer>().Tint = Color.White;
			_editorRayLastHighlight = null;
			_roRayLastHighlight = null;
		}
	}

	private void EditorSetRoomObject( GameObject go )
	{
		EditorRayOff();

		if ( go?.Components.Get<RoomObjectGizmoComponent>() is { } gizmoComponent )
		{
			go.Components.Get<ModelRenderer>().Tint = Color.Red;
			_editorRayLastHighlight = go;
			_roRayLastHighlight = gizmoComponent;
		}
	}

	public RoomObjectGizmoComponent EditorSearchRoomObject( RoomObject roomObject ) => Scene
		.GetAllComponents<RoomObjectGizmoComponent>()
		.FirstOrDefault( ro => ro.RoomObject == roomObject );


	public void EditorSelectRoomObject( RoomObject roomObject )
	{
		if ( EditorSearchRoomObject( roomObject ) is { } rogc )
		{
			EditorSetRoomObject( rogc.GameObject );
		}
	}

	public void EditorSetCameraTarget( RoomObject roomObject )
	{
		if ( EditorSearchRoomObject( roomObject ) is { } rogc )
		{
			EditorCamera.Target = rogc;
		}
	}
}
