using Sandbox;

namespace ShartCoding.GameMaker.Engine;

public class ActorComponent : Component
{
	private ActorAppearance _currentAppearance;

	public void SetAppearance( ActorAppearance appearance )
	{
		_currentAppearance?.Undress( this );

		_currentAppearance = appearance;
		_currentAppearance?.Dress( this );
	}
	
	// TODO: execute the code
}
