using Sandbox;
using ShartCoding.GameMaker.Engine;

namespace ShartCoding.GameMaker.ActorAppearances;

public class FlatAppearance : ActorAppearance
{
	// TODO:
	public override BBox BBox => new BBox();

	public override void Dress( ActorComponent actor )
	{
		throw new System.NotImplementedException();
	}

	public override void Undress( ActorComponent actor )
	{
		throw new System.NotImplementedException();
	}

	public override void DressGizmo( GameObject gizmo )
	{
		throw new System.NotImplementedException();
	}
}
