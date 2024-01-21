using Sandbox;

namespace ShartCoding.GameMaker.Engine;

public class RoomSunComponent : DirectionalLight
{
	[Property] public Room Room;

	protected override void OnFixedUpdate()
	{
		base.OnFixedUpdate();

		if ( Room != null )
		{
			Transform.Rotation = Rotation.From( Room.SunAngles );
			LightColor = Room.SunlightColor;
			SkyColor = Room.SkyColor;
			Shadows = Room.CastShadows;
		}
	}
}
