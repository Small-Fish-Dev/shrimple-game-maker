namespace ShartCoding.ShartCode;

public class ShartCodeVariable
{
	public string Name { get; set; }
	// public ShartCodeType Type { get; set; }
	public object Value { get; set; }

	public ShartCodeVariable( string name, object value )
	{
		Name = name;
		Value = value;
	}
}
