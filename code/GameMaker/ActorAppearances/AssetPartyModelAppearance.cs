using Sandbox;
using Sandbox.Diagnostics;
using ShartCoding.Attributes;
using ShartCoding.GameMaker.Engine;
using ShartCoding.GameMaker.Resources;
using ShartCoding.UI.AppearancePanels;

namespace ShartCoding.GameMaker.ActorAppearances;

[GameMakerAppearance( typeof(AssetPartyAppearancePanel) )]
public class AssetPartyModelAppearance : ActorAppearance
{
	public ModelResource Model
	{
		get => _model;
		set
		{
			_model = value;

			OnChange( this );
		}
	}

	private ModelResource _model;

	public override BBox BBox => Model.Model.RenderBounds;

	public AssetPartyModelAppearance( ModelResource model )
	{
		Model = model;
		Model.Change += resource => OnChange( this );
	}

	public override void Dress( ActorComponent actor )
	{
		if ( !Model.IsValid )
		{
			// TODO: not proud of this code
			Model.Preload().Wait();
		}

		Assert.True( Model.IsValid );

		var modelRenderer = actor.Components.Create<ModelRenderer>();
		modelRenderer.Model = Model.Model;
	}

	public override void Undress( ActorComponent actor )
	{
		actor.Components.Get<ModelRenderer>()?.Destroy();
	}

	public override void DressGizmo( GameObject gizmo )
	{
		var modelRenderer = gizmo.Components.GetOrCreate<ModelRenderer>();
		modelRenderer.Model = Model.Model;
	}
}
