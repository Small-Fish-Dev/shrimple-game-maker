using System;
using System.Threading.Tasks;
using Sandbox;

namespace ShartCoding.GameMaker;

public abstract class GameMakerResource : IValid
{
	public abstract Task Preload();
	public abstract bool IsValid { get; }
	
	public event Action<GameMakerResource> Change;

	protected virtual void OnChange( GameMakerResource obj )
	{
		Change?.Invoke( obj );
	}
}
