using Sandbox;
using ShartCoding.GameMaker.Engine;

namespace ShartCoding.GameMaker;

public abstract class ActorAppearance
{
	public abstract void Dress( ActorComponent actor );

	public abstract void Undress( ActorComponent actor );

	public abstract void DressGizmo( GameObject gizmo );
}
