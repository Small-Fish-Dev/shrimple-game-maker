using Sandbox.UI;
using ShartCoding.ShartCode;

namespace ShartCoding.UI.CodeBlockPanels;

public abstract class AbstractCodeBlockPanel : Panel, ICodeBlockPanel
{
	public abstract ShartCodeBlock Block { get; set; }
}
