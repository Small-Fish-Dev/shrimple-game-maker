using Sandbox;

namespace ShartCoding.GameMaker.Engine;

public class RoomObjectGizmoComponent : Component
{
	public RoomObject RoomObject { get; set; }
	
	// TODO: draw the axis helper using SceneObject

	protected override void OnFixedUpdate()
	{
		base.OnFixedUpdate();

		Transform.Position = RoomObject.Position;
	}
}
