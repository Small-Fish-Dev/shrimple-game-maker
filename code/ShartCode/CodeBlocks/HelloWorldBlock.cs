using System;
using ShartCoding.Attributes;
using ShartCoding.GameMaker.Engine;
using ShartCoding.ShartCode.Types;
using ShartCoding.UI.CodeBlockPanels;

namespace ShartCoding.ShartCode.CodeBlocks;

[ShartCodeBlock( typeof(HelloWorldPanel) )]
public class HelloWorldBlock : ShartCodeBlock
{
	private readonly ShartCodeActor ActorType = new();
	
	public override void Evaluate( ShartCodeContext context )
	{
		var actor = context.ResolveVariable( "this" );
		if ( actor is null )
		{
			throw new Exception($"Cannot find \"this\"");
		}
		
		if ( !ActorType.Check( actor ) || actor.Value is not ActorComponent actorComponent )
		{
			throw new Exception($"Invalid actor");
		}
		
		// TODO: replace with a Say() "native" function
		// TODO: show a little cloud above actor's head
		
		Log.Info( $"{actorComponent.GameObject}: Hellorld!" );
	}
}
