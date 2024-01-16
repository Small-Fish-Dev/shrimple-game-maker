using System;
using System.Threading.Tasks;
using Sandbox;

namespace ShartCoding.GameMaker.Resources;

public abstract class ImageResource : GameMakerResource
{
	public string Url { get; set; }
	public Texture Texture { get; protected set; }

	public override bool IsValid => Texture is { IsLoaded: true };

	public ImageResource( string url )
	{
		Url = url;
	}

	public override async Task Preload()
	{
		var filesystem = FileSystem.Data;
		if ( Uri.TryCreate( Url, UriKind.Absolute, out var uriResult )
		     && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps) )
		{
			filesystem = null;
		}

		Texture = await Texture.LoadAsync( filesystem, Url );
	}
}
