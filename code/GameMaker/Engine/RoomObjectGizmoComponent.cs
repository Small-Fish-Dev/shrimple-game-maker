using Sandbox;

namespace ShartCoding.GameMaker.Engine;

public class RoomObjectGizmoComponent : Component
{
	public RoomObject RoomObject
	{
		get => _roomObject;
		set
		{
			if ( _roomObject != null )
			{
				_roomObject.Actor.Appearance.Change -= Dress;
			}

			_roomObject = value;
			_roomObject.Actor.Appearance.Change += Dress;
			Dress( _roomObject.Actor.Appearance );
		}
	}

	private RoomObject _roomObject;

	// TODO: draw the axis helper using SceneObject

	protected override void OnFixedUpdate()
	{
		base.OnFixedUpdate();

		Transform.Position = RoomObject.Position;
	}

	private void Dress( ActorAppearance appearance )
	{
		appearance.DressGizmo( GameObject );
	}
}
