using Sandbox.UI;
using ShartCoding.GameMaker;

namespace ShartCoding.UI.AppearancePanels;

public abstract class AbstractAppearancePanel : Panel, IAppearancePanel
{
	public abstract ActorAppearance Appearance { get; set; }
}
