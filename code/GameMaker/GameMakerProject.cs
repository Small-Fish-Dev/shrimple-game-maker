﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Sandbox.Diagnostics;
using ShartCoding.GameMaker.ActorAppearances;
using ShartCoding.GameMaker.Resources;

namespace ShartCoding.GameMaker;

public class GameMakerProject
{
	public const string DescriptionExtension = ".gmpd.json";
	public const string ProjectExtension = ".gmp.json";

	public GameMakerProjectDescription Description { get; set; } = new("untitled", "Untitled project", "Coming soon!");

	public List<ActorTemplate> Actors { get; set; } = new();

	public List<GameMakerResource> Resources { get; set; } = new();

	public List<Room> Rooms { get; set; } = new();
	public int InitialRoomId { get; set; } = 0;

	// TODO: assert
	public Room InitialRoom
	{
		get
		{
			// Won't be valid when the room list is empty
			Assert.True( InitialRoomId >= 0 && InitialRoomId < Rooms.Count );
			return Rooms[InitialRoomId];
		}
	}
    
	public static GameMakerProject Empty( GameMakerProjectDescription description )
	{
		var modelResource = new ModelResource( "fish.hs_hampter" );
		var exampleActor =
			new ActorTemplate { Name = "Example", Appearance = new AssetPartyModelAppearance( modelResource ) };

		return new GameMakerProject
		{
			Description = description,
			Resources = new List<GameMakerResource> { modelResource },
			Actors = new List<ActorTemplate> { exampleActor },
			Rooms = new List<Room>
			{
				new()
				{
					Name = "First room",
					RoomObjects = new List<RoomObject>
					{
						new() { Position = Vector3.Zero, Actor = exampleActor }
					}
				}
			}
		};
	}

	public async Task Preload()
	{
		foreach ( var resource in Resources )
		{
			await resource.Preload();
		}
	}
}
