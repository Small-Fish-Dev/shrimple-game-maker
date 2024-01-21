using System;
using System.Collections.Generic;
using Sandbox;
using ShartCoding.ShartCode;

namespace ShartCoding.GameMaker;

public class RoomObject
{
	public string Name { get; set; }

	// TODO: rotation
	public Vector3 Position { get; set; }
	public ActorTemplate Actor { get; set; }

	public RoomObject( Vector3 position, ActorTemplate actor, string name = default )
	{
		Position = position;
		Actor = actor;

		Name = name ?? actor.Name;
	}
}

public class Room
{
	public string Name { get; set; }

	public Angles SunAngles { get; set; } = new(35, 50, 0);
	public Color SunlightColor { get; set; } = Color.FromRgba( 0xE9FAFFFF );
	public Color SkyColor { get; set; } = Color.FromRgba( 0x0F1315FF );
	public bool CastShadows { get; set; } = true;

	public IReadOnlyList<RoomObject> RoomObjects => _roomObjects.AsReadOnly();

	public ShartCodeObject Object { get; set; } = new(); // TODO: Call Enter and Exit functions

	public Action<RoomObject> OnAdded;
	public Action<RoomObject> OnRemoved; // TODO:

	private List<RoomObject> _roomObjects;

	public Room( IEnumerable<RoomObject> roomObjects )
	{
		_roomObjects = new List<RoomObject>( roomObjects ?? Array.Empty<RoomObject>() );
	}

	public void Add( RoomObject roomObject )
	{
		_roomObjects.Add( roomObject );
		OnAdded?.Invoke( roomObject );
	}

	public void Populate( GameObject root )
	{
		foreach ( var roomObject in _roomObjects )
		{
			Log.Info( roomObject );
			
			var actor = roomObject.Actor.Make();
			actor.SetParent( root );
			actor.Transform.Position = roomObject.Position;
		}
	}
}
