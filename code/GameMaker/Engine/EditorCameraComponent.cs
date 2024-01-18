using Sandbox;

namespace ShartCoding.GameMaker.Engine;

public class EditorCameraComponent : Component
{
	[Property] public float Speed = 10.0f;
	[Property] public float FastSpeed = 20.0f;
	[Property] public bool Captured = false;

	private CameraComponent _camera;
	private Angles _eyeAngles;

	protected override void OnAwake()
	{
		base.OnAwake();

		_camera = Components.Get<CameraComponent>();
	}

	protected override void OnEnabled()
	{
		base.OnEnabled();

		_camera.Enabled = true;
	}

	protected override void OnDisabled()
	{
		base.OnDisabled();

		_camera.Enabled = false;
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();

		// TODO: can't do any other way
		if ( Input.Released( "EditorCamCapture" ) )
			Captured = false;

		if ( Captured )
		{
			var e = _eyeAngles;
			e += Input.AnalogLook;
			e.pitch = e.pitch.Clamp( -90, 90 );
			e.roll = 0.0f;
			_eyeAngles = e;

			Transform.Rotation = new Angles( _eyeAngles.pitch, _eyeAngles.yaw, 0 );
			// TODO: InputAnalog.Move didn't work for me
			var movement = (
				               Vector3.Forward * (Input.Down( "Forward" ) ? 1 : 0)
				               + Vector3.Backward * (Input.Down( "Backward" ) ? 1 : 0)
				               + Vector3.Left * (Input.Down( "Left" ) ? 1 : 0)
				               + Vector3.Right * (Input.Down( "Right" ) ? 1 : 0)
			               ) * Transform.Rotation
			               + Vector3.Up * (Input.Down( "Up" ) ? 1 : 0)
			               + Vector3.Down * (Input.Down( "Down" ) ? 1 : 0);
			movement *= Input.Down( "Run" ) ? FastSpeed : Speed;
			Transform.Position += movement * Time.Delta;
		}
	}
}
