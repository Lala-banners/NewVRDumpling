// Creator: Kieran & James
// Creation Time: 2022/04/11 09:01
using UnityEngine;
using UnityEngine.InputSystem;
using ModularCharacterController;

namespace ModularCharacterController.Cameras
{
	[RequireComponent(typeof(Camera))]
	public class FpsCamera : ModularBehaviour
	{
		private const string CONTROLLER_SCHEME_NAME = "Gamepad";
		
		[SerializeField] private CameraSettings settings;
		
		private new Camera camera;
		private IMCCPlayer playerInterface;
		private PlayerInput input;
		private Transform player;

		private Vector2 rotation = Vector2.zero;
		private Vector2 lookInput;

		public override void Init(IMCCPlayer _playerInterface)
		{
			input = _playerInterface.Input;
			player = _playerInterface.Transform;
			
			camera = gameObject.GetComponent<Camera>();
			camera.enabled = true;
			input.camera = camera;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			
			playerInterface = _playerInterface;
			playerInterface.Input.camera = camera;
		}

		protected override void OnProcess(UpdatePhase _phase)
		{
			Vector2 lookVector = lookInput;
			
			rotation.x += lookVector.x * settings.GetSensitivity(input.currentControlScheme == CONTROLLER_SCHEME_NAME);
			rotation.y += lookVector.y * settings.GetSensitivity(input.currentControlScheme == CONTROLLER_SCHEME_NAME);
			rotation.y = Mathf.Clamp(rotation.y, -settings.VerticalLookBounds, settings.VerticalLookBounds);
			
			transform.localRotation = Quaternion.AngleAxis(rotation.y, Vector3.left);
			player.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
		}

		
		public void OnLook(InputAction.CallbackContext context)
		{
			lookInput = context.ReadValue<Vector2>();
		}
		
		protected override void OnEnabledStateChanged(bool _newState)
		{
			if (camera == null)
				camera = gameObject.GetComponent<Camera>();

			if (settings.TurnOnAudioListener)
			{
				gameObject.GetComponent<AudioListener>().enabled = _newState;
				camera.enabled = _newState;
			}

			if (_newState)
			{
				playerInterface.Input.camera = camera;
				
				rotation.x = player.localRotation.eulerAngles.y;
				player.localRotation = Quaternion.AngleAxis(rotation.x, Vector3.up);
			}
		}
		
		private void Reset()
		{
			// Find the input in scene if there is only one
			if(input == null)
			{
				PlayerInput[] playerInputsInScene = FindObjectsOfType<PlayerInput>();

				if(playerInputsInScene.Length == 1)
				{
					input = playerInputsInScene[0];
					if (input!= null)
						Debug.Log($"Added input {input}",this);
				}
			}
		}
	}
}