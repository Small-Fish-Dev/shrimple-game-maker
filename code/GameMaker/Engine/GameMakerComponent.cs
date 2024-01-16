﻿using Sandbox;
using Sandbox.Diagnostics;
using ShartCoding.ShartCode;

namespace ShartCoding.GameMaker.Engine;

public class GameMakerComponent : Component
{
	[Property] public GameObject RoomStorage;
	[Property] public GameObject RoomPreviewStorage;
	[Property] public bool Playing { get; private set; } = false;
	[Property] public DirectionalLight Sun;
	[Property] public CameraComponent EditorCamera;
	[Property] public CameraComponent GameCamera;

	public GameMakerProject Current { get; private set; }
	public Room CurrentEditorRoom { get; private set; }
	public ShartCodeContext GlobalContext { get; private set; } = new();

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

	protected void SetSunSettings( Room room )
	{
		Sun.LightColor = room.SunlightColor;
		Sun.SkyColor = room.SkyColor;
		Sun.Shadows = room.CastShadows;
		Sun.Transform.Rotation = Rotation.From( room.SunAngle );
	}

	public void SetEditorRoom( Room room )
	{
		if ( Current is null || Playing || CurrentEditorRoom == room )
			return;

		CurrentEditorRoom = room;

		RoomPreviewStorage.Clear();

		foreach ( var roomObject in room.RoomObjects )
		{
			var go = new GameObject();
			// TODO: make a preview object with gizmo
			var gizmo = go.Components.Create<RoomObjectGizmoComponent>();
			gizmo.RoomObject = roomObject;
			roomObject.Actor.Appearance.DressGizmo( go );
			go.SetParent( RoomPreviewStorage );
		}

		SetSunSettings( CurrentEditorRoom );
	}

	public void SetRoom( Room room )
	{
		if ( Current is null || !Playing )
			return;

		RoomStorage.Clear();
		room.Populate( RoomStorage );

		SetSunSettings( room );
	}
}
