using System.Threading.Tasks;
using Sandbox;

namespace ShartCoding.GameMaker;

public abstract class GameMakerResource : IValid
{
	public abstract Task Preload();
	public abstract bool IsValid { get; }
}
