using System;
using ShartCoding.Attributes;
using ShartCoding.GameMaker.Engine;
using ShartCoding.ShartCode.Types;
using ShartCoding.UI.CodeBlockPanels;

namespace ShartCoding.ShartCode.CodeBlocks;

[ShartCodeBlock( typeof(LogPanel) )]
public class LogBlock : ShartCodeBlock
{
	private readonly ShartCodeActorReference _actorReferenceType = new();
	public string Message { get; set; }

	public LogBlock( string message = "Hellorld!" )
	{
		Message = message;
	}
	
	public override void Evaluate( ShartCodeContext context )
	{
		var actor = context.ResolveVariable( "this" );
		if ( actor is null )
		{
			throw new Exception($"Cannot find \"this\"");
		}
		
		if ( !_actorReferenceType.Check( actor ) || actor.Value is not ActorComponent actorComponent )
		{
			throw new Exception($"Invalid actor");
		}
		
		// TODO: replace with a Say() "native" function
		// TODO: show a little cloud above actor's head
		
		Log.Info( $"{actorComponent}: {Message}" );
	}
}
