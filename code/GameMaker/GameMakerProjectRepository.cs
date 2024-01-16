using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sandbox;

namespace ShartCoding.GameMaker;

public class GameMakerProjectRepository
{
	public static GameMakerProjectRepository The { get; } = new();

	public IReadOnlyList<GameMakerProjectDescription> All => _allProjects.AsReadOnly();

	private List<GameMakerProjectDescription> _allProjects = new();

	protected GameMakerProjectRepository()
	{
		// TODO: all this shite is untested

		FileSystem.Data.CreateDirectory( "projects" );
		var result = FileSystem.Data.FindFile( "projects", $"*{GameMakerProject.DescriptionExtension}" );
		foreach ( var descriptionFileName in result )
		{
			var projectFileName = descriptionFileName[..^GameMakerProject.DescriptionExtension.Length]
			                      + GameMakerProject.ProjectExtension;
			if ( !FileSystem.Data.FileExists( projectFileName ) )
			{
				Log.Error( $"Found a description file without the project file: {descriptionFileName}" );
				continue;
			}

			using ( var descriptionStream = FileSystem.Data.OpenRead( descriptionFileName ) )
			using ( var descriptionStreamReader = new StreamReader( descriptionStream ) )
			{
				var description = Json.Deserialize<GameMakerProjectDescription>( descriptionStreamReader.ReadToEnd() );
				_allProjects.Add( description );
			}

			FileSystem.Data.OpenRead( projectFileName );
			// TODO: the rest of the fucking owl
		}
	}

	public GameMakerProject Load( GameMakerProjectDescription description )
	{
		throw new NotImplementedException();
	}

	public GameMakerProject Make( GameMakerProjectDescription description )
	{
		if ( FileSystem.Data.FindFile( "projects", $"{description.FileName}{GameMakerProject.DescriptionExtension}" )
			    .ToList().Count > 0 )
		{
			throw new Exception( $"Project with file name {description.FileName} already exists" );
		}

		return GameMakerProject.Empty( description );
	}
}
