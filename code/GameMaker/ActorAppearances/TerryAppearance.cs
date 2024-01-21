using Sandbox;
using ShartCoding.GameMaker.Engine;

namespace ShartCoding.GameMaker.ActorAppearances;

public class TerryAppearance : ActorAppearance
{
	// TODO:
	public override BBox BBox => new();
	
	// TODO: citizen model, animation helper, outfit
	
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
