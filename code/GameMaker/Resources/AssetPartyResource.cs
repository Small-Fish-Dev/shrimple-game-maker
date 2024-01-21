using System;
using System.Threading.Tasks;
using Sandbox;

namespace ShartCoding.GameMaker.Resources;

public abstract class AssetPartyResource : GameMakerResource
{
	public string Ident
	{
		get => _ident;
		set
		{
			Package = null;
			
			if ( !Package.TryParseIdent( value, out var ident ) )
			{
				_ident = "";
			}
			else
			{
				_ident = value;
			}
			// TODO: urgh, async!
			try
			{
				Preload().Wait();
			}
			catch ( Exception ex )
			{
				Log.Trace( ex );
				_ident = "";
			}

			OnChange( this );
		}
	}

	protected Package Package;

	private string _ident;

	protected AssetPartyResource( string ident )
	{
		Ident = ident;
	}

	public override async Task Preload()
	{
		// TODO: partial packages????
		Package = await Package.FetchAsync( Ident, false );
	}

	public override bool IsValid => Package?.IsMounted() ?? false;
}
