using System.Collections.Generic;
using Sandbox;
using Sandbox.Diagnostics;
using ShartCoding.ShartCode;

namespace ShartCoding.GameMaker.Engine;

public class ActorComponent : Component
{
	[Property] public ActorTemplate Template { get; private set; }

	private ActorAppearance _currentAppearance;
	private ShartCodeContext.ContextFrame _contextFrame;
	private ShartCodeFunction _onTickFunction;

	public void SystemTick( ActorSystem actorSystem )
	{
		Assert.NotNull( Template );

		using ( actorSystem.GameMakerComponent.GlobalContext.Use( _contextFrame ) )
		{
			_onTickFunction?.Execute( actorSystem.GameMakerComponent.GlobalContext,
				new Dictionary<string, ShartCodeVariable>() );
		}
	}

	public void SetAppearance( ActorAppearance appearance )
	{
		_currentAppearance?.Undress( this );

		_currentAppearance = appearance;
		_currentAppearance?.Dress( this );
	}

	// TODO: execute the code
	public void SetTemplate( ActorTemplate actorTemplate )
	{
		Assert.NotNull( actorTemplate );

		Template = actorTemplate;

		_contextFrame = actorTemplate.CodeObject.GetContext();
		_contextFrame.Variables.Add( "this", new ShartCodeVariable( "this", this ) );
		if ( actorTemplate.CodeObject.Functions.TryGetValue( "Tick", out var onTickFunction ) )
		{
			_onTickFunction = onTickFunction;
		}

		SetAppearance( Template.Appearance );
	}
}
