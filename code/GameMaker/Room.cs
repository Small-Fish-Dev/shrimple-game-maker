using System.Collections.Generic;
using Sandbox;

namespace ShartCoding.GameMaker;

public class RoomObject
{
	// TODO: rotation
	public Vector3 Position { get; set; }
	public ActorTemplate Actor { get; set; }
}

public class Room
{
	public string Name { get; set; }

	public Angles SunAngle { get; set; } = new Angles( 35, 50, 0 );
	public Color SunlightColor { get; set; } = Color.FromRgba( 0xE9FAFFFF );
	public Color SkyColor { get; set; } = Color.FromRgba( 0x0F1315FF );
	public bool CastShadows { get; set; } = true;

	public List<RoomObject> RoomObjects { get; set; }

	public void Populate( GameObject root )
	{
		foreach ( var roomObject in RoomObjects )
		{
			var actor = roomObject.Actor.Make();
			actor.SetParent( root );
			actor.Transform.Position = roomObject.Position;
		}
	}
}
