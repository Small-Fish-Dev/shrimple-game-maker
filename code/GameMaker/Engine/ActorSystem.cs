using System.Linq;
using Sandbox;

namespace ShartCoding.GameMaker.Engine;

public class ActorSystem : GameObjectSystem
{
	public GameMakerComponent GameMakerComponent { get; private set; }

	public ActorSystem( Scene scene ) : base( scene )
	{
		Listen( Stage.FinishUpdate, 0, TickActors, "TickActors" );
	}

	public void Register( GameMakerComponent gameMaker )
	{
		GameMakerComponent = gameMaker;
	}

	private void TickActors()
	{
		if ( GameMakerComponent is null || !GameMakerComponent.Playing )
			return;

		var allActors = Scene.GetAllComponents<ActorComponent>();
		foreach ( var actor in allActors )
		{
			if ( !actor.Spawned )
			{
				actor.SystemSpawn( this );
			}
			actor.SystemTick( this );
		}
	}
}
