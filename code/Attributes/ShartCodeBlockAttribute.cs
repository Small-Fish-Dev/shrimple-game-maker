using System;
using System.Linq;
using Sandbox.Diagnostics;
using ShartCoding.UI;

namespace ShartCoding.Attributes;

[AttributeUsage( AttributeTargets.Class )]
public class ShartCodeBlockAttribute : Attribute
{
	public Type EditorPanel { get; set; }

	public ShartCodeBlockAttribute( Type editorPanel )
	{
		// TODO: it doesn't work right now
		if ( editorPanel != null )
		{
			var type = TypeLibrary.GetType( editorPanel );
			Log.Info( $"{type}" );

			if ( type != null )
			{
				foreach ( var t in type.Interfaces )
					Log.Info( $"{t}" );

				Assert.True(
					TypeLibrary.GetType( editorPanel )?.Interfaces.Any( t => t == typeof(ICodeBlockPanel) ) ??
					false,
					"Editor panel should implement ICodeBlockPanel" );
			}
		}

		EditorPanel = editorPanel;
	}
}
