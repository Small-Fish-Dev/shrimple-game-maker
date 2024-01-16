using System.Threading.Tasks;
using Sandbox;
using Sandbox.Diagnostics;

namespace ShartCoding.GameMaker.Resources;

public class ModelResource : AssetPartyResource
{
	public Model Model { get; protected set; }

	public override bool IsValid => Model is { IsError: false };

	public ModelResource( string ident ) : base( ident )
	{
	}

	public override async Task Preload()
	{
		await base.Preload();

		Assert.AreEqual( Package.PackageType, Package.Type.Model );

		_ = await Package.MountAsync();

		var path = Package.GetMeta<string>( "PrimaryAsset" ) ?? Package.GetMeta<string>( "SingleAssetSource" );
		Assert.AreNotEqual( path, default );
		Model = Model.Load( path );
	}
}
