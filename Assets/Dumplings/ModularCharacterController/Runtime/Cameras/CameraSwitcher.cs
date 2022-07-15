// Creator: Kieran
// Creation Time: 2022/06/08 16:29
using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController.Cameras
{
	public class CameraSwitcher : MonoBehaviour
	{
		[SerializeField] private MultiCamera multiCamera;
		[SerializeField] private MultiMotor multiMotor;
		private bool switchCameraInput;
		
		private void Start()
		{
			multiCamera.ActivateCamera(0);
			multiMotor.ActivateMotor(0);
		}

		private void Update()
		{
			if (switchCameraInput)
			{
				SwitchCamera();
				Debug.Log("Swap Input");
			}
		}
		
		private void SwitchCamera()
		{
			multiCamera.NextCamera();
			multiMotor.NextMotor();
			switchCameraInput = false;
		}
		
		public void OnSwitchCamera(InputAction.CallbackContext context)
		{
			switchCameraInput = context.action.triggered;
		}
		
		private void Reset()
		{
			GetInChildrenMultiCameraAndMultiMotor();
		}
		
		[Button]
		private void GetInChildrenMultiCameraAndMultiMotor()
		{
				multiCamera = GetComponentInChildren<MultiCamera>();

				if(multiCamera != null)
					Debug.Log($"Please add a MultiCamera || {multiCamera}", this);
			
				multiMotor = GetComponentInChildren<MultiMotor>();

				if(multiMotor != null)
					Debug.Log($"Please add a MultiMotor || {multiMotor}", this);
		}
	}
}