using Sandbox;
using ShartCoding.GameMaker.Engine;

namespace ShartCoding.GameMaker;

public class ActorTemplate
{
	public string Name { get; set; }

	public ActorAppearance Appearance { get; set; }

	public GameObject Make()
	{
		var go = new GameObject();

		// TODO:
		var actorComponent = go.Components.Create<ActorComponent>();
		actorComponent.SetAppearance( Appearance );

		return go;
	}
}
