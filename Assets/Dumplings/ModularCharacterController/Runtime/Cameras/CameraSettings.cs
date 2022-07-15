// Creator: Kieran & James
// Creation Time: 2022/04/11 09:01
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController.Cameras
{
	[CreateAssetMenu(fileName = "New Camera Settings", menuName = "MCC/Camera Settings", order = 0)]
	public class CameraSettings : ScriptableObject
	{
		public float GetSensitivity(bool _controller) => _controller ? controllerSensitivity : mouseSensitivity;
		public float VerticalLookBounds => verticalLookBounds;
		
		[SerializeField, Range(0, 3)] private float mouseSensitivity = .5f;
		[SerializeField, Range(0, 3)] private float controllerSensitivity = .5f;
		[SerializeField, Range(0, 90)] private float verticalLookBounds = 90;
		[SerializeField] private bool turnOnAudioListener = false;
		public bool TurnOnAudioListener => turnOnAudioListener;
	}
}