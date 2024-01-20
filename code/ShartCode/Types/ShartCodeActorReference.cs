using ShartCoding.Attributes;
using ShartCoding.GameMaker.Engine;
using ShartCoding.UI.CodeTypePanels;

namespace ShartCoding.ShartCode.Types;

[ShartCodeType(typeof(ActorReferencePanel))]
public class ShartCodeActorReference : ShartCodeType
{
	public override object Default => null;

	public override bool Check( ShartCodeVariable variable )
	{
		return variable.Value is null or ActorComponent;
	}
}
