using System.Collections.Generic;
using System.Linq;

namespace ShartCoding.ShartCode;

public class ShartCodeObject
{
	public Dictionary<string, ShartCodeType> Fields { get; set; } = new();
	public Dictionary<string, ShartCodeFunction> Functions { get; set; } = new();

	public ShartCodeContext.ContextFrame GetContext()
	{
		return new ShartCodeContext.ContextFrame
		{
			Functions = Functions,
			Variables = Fields.ToDictionary( kv => kv.Key, kv => new ShartCodeVariable( kv.Key, kv.Value.Default ) )
		};
	}
}
