using Sandbox;
using Sandbox.Diagnostics;
using ShartCoding.GameMaker.Engine;
using ShartCoding.GameMaker.Resources;

namespace ShartCoding.GameMaker.ActorAppearances;

public class AssetPartyModelAppearance : ActorAppearance
{
	public ModelResource Model { get; set; }

	private ModelRenderer _modelRenderer;

	public AssetPartyModelAppearance( ModelResource model )
	{
		Model = model;
	}

	public override void Dress( ActorComponent actor )
	{
		if ( !Model.IsValid )
		{
			// TODO: not proud of this code
			Model.Preload().Wait();
		}
		
		Assert.True( Model.IsValid );

		_modelRenderer = actor.Components.Create<ModelRenderer>();
		_modelRenderer.Model = Model.Model;
	}

	public override void Undress( ActorComponent actor )
	{
		_modelRenderer?.Destroy();
	}

	public override void DressGizmo( GameObject gizmo )
	{
		var modelRenderer = gizmo.Components.Create<ModelRenderer>();
		modelRenderer.Model = Model.Model;
	}
}
