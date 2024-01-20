using System;
using Sandbox;
using ShartCoding.Attributes;
using ShartCoding.GameMaker.Engine;
using ShartCoding.ShartCode.Types;
using ShartCoding.UI.CodeBlockPanels;

namespace ShartCoding.ShartCode.CodeBlocks;

[ShartCodeBlock( typeof(SpinPanel) )]
public class SpinBlock : ShartCodeBlock
{
	public float Speed = 100;
	
	private readonly ShartCodeActorReference _actorReferenceType = new();

	public SpinBlock( float speed )
	{
		Speed = speed;
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

		actorComponent.Transform.Rotation =
			actorComponent.Transform.Rotation.RotateAroundAxis( Vector3.Up, Speed * Time.Delta );
	}
}
