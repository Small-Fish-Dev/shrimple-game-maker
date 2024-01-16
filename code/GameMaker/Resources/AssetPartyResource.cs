using System.Threading.Tasks;
using Sandbox;

namespace ShartCoding.GameMaker.Resources;

public abstract class AssetPartyResource : GameMakerResource
{
	public string Ident { get; set; }

	protected Package Package;

	protected AssetPartyResource( string ident )
	{
		Ident = ident;
	}

	public override async Task Preload()
	{
		// TODO: partial packages????
		Package = await Package.FetchAsync( Ident, false );
	}

	public override bool IsValid => Package.IsMounted();
}
