using System;
using System.Collections.Generic;

namespace ShartCoding.ShartCode;

public class ShartCodeFunction
{
	// TODO: is it really needed?
	public string Name { get; set; }
	public Dictionary<string, ShartCodeType> Arguments { get; set; }
	public List<ShartCodeBlock> CodeBlocks { get; set; }

	public ShartCodeFunction( string name, Dictionary<string, ShartCodeType> arguments,
		List<ShartCodeBlock> codeBlocks )
	{
		Name = name;
		Arguments = arguments;
		CodeBlocks = codeBlocks;
	}

	public void Execute( ShartCodeContext context, Dictionary<string, ShartCodeVariable> arguments )
	{
		foreach ( var requiredArgument in Arguments )
		{
			if ( arguments.TryGetValue( requiredArgument.Key, out var argument ) )
			{
				if ( !requiredArgument.Value.Check( argument ) )
				{
					throw new ArgumentException(
						$"Argument \"{requiredArgument.Key}\" should be \"{requiredArgument.Value}\", got \"{argument.Value.GetType()}\" instead." );
				}
			}
			else
			{
				throw new ArgumentException(
					$"Argument \"{requiredArgument.Key}\" is not found." );
			}
		}

		// TODO:
		using var ctx = context.Use( arguments );
		foreach ( var block in CodeBlocks )
		{
			block.Evaluate( context );
		}
	}
}
