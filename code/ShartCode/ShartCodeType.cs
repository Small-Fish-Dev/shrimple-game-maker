namespace ShartCoding.ShartCode;

public abstract class ShartCodeType
{
	public abstract bool Check( ShartCodeVariable variable );
	public abstract object Default { get; }
}
