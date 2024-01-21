using System;
using Sandbox;
using ShartCoding.GameMaker.Engine;

namespace ShartCoding.GameMaker;

public abstract class ActorAppearance
{
	public abstract BBox BBox { get; }
	
	public abstract void Dress( ActorComponent actor );

	public abstract void Undress( ActorComponent actor );

	public abstract void DressGizmo( GameObject gizmo );
	
	public event Action<ActorAppearance> Change;

	protected virtual void OnChange( ActorAppearance obj )
	{
		Change?.Invoke( obj );
	}
}
