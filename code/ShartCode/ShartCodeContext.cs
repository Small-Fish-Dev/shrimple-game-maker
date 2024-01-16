using System;
using System.Collections.Generic;
using Sandbox.Utility;

namespace ShartCoding.ShartCode;

public class ShartCodeContext
{
	public struct ContextFrame
	{
		public Dictionary<string, ShartCodeVariable> Variables;
		public Dictionary<string, ShartCodeFunction> Functions;
	}

	private readonly Stack<ContextFrame> _contextFrames = new();

	public ShartCodeVariable ResolveVariable( string name )
	{
		foreach ( var frame in _contextFrames )
		{
			if ( frame.Variables != null && frame.Variables.TryGetValue( name, out var variable ) )
			{
				return variable;
			}
		}

		return null;
	}

	public ShartCodeFunction ResolveFunction( string name )
	{
		foreach ( var frame in _contextFrames )
		{
			if ( frame.Functions != null && frame.Functions.TryGetValue( name, out var variable ) )
			{
				return variable;
			}
		}

		return null;
	}

	public IDisposable Use( ContextFrame contextFrame )
	{
		_contextFrames.Push( contextFrame );
		return new DisposeAction( () => _contextFrames.Pop() );
	}

	public IDisposable Use( Dictionary<string, ShartCodeVariable> arguments )
	{
		_contextFrames.Push( new ContextFrame { Variables = arguments } );
		return new DisposeAction( () => _contextFrames.Pop() );
	}
}
