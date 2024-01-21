using System.Collections.Generic;
using System.Threading.Tasks;
using Sandbox.Diagnostics;
using ShartCoding.GameMaker.ActorAppearances;
using ShartCoding.GameMaker.Resources;
using ShartCoding.ShartCode;
using ShartCoding.ShartCode.CodeBlocks;
using ShartCoding.ShartCode.Types;

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
			new ActorTemplate
			{
				Name = "Example",
				Appearance = new AssetPartyModelAppearance( modelResource ),
				CodeObject = new ShartCodeObject
				{
					Fields = new Dictionary<string, ShartCodeType>(),
					Functions = new Dictionary<string, ShartCodeFunction>
					{
						{
							"Spawn", new ShartCodeFunction( "Spawn",
								new Dictionary<string, ShartCodeType>(),
								new List<ShartCodeBlock> { new LogBlock() } )
						},
						{
							"Tick", new ShartCodeFunction( "Tick",
								new Dictionary<string, ShartCodeType>(),
								new List<ShartCodeBlock> { new SpinBlock( 100 ) } )
						}
					}
				}
			};

		return new GameMakerProject
		{
			Description = description,
			Resources = new List<GameMakerResource> { modelResource },
			Actors = new List<ActorTemplate> { exampleActor },
			Rooms = new List<Room>
			{
				new(new List<RoomObject> { new(Vector3.Zero, exampleActor) }) { Name = "First room" }
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
