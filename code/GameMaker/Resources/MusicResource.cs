using System;
using System.Threading.Tasks;

namespace ShartCoding.GameMaker.Resources;

public class MusicResource : AssetPartyResource
{
	public MusicResource( string ident ) : base( ident )
	{
		throw new NotImplementedException();
	}

	public override async Task Preload()
	{
		await base.Preload();

		throw new NotImplementedException();
	}
}
