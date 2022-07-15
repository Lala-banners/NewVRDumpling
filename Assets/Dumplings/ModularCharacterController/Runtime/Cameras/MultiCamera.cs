 // Creator: Kieran
// Creation Time: 2022/06/06 12:38
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModularCharacterController.Cameras
{
	public class MultiCamera : ModularBehaviour
	{
		[SerializeField] private List<ModularBehaviour> subCameras = new List<ModularBehaviour>();

		private int currentCameraIndex;
		
		public bool TryGetBehaviour<BEHAVIOUR>(out BEHAVIOUR _found) where BEHAVIOUR : ModularBehaviour
		{
			foreach(ModularBehaviour behaviour in subCameras)
			{
				if(behaviour.GetType() == typeof(BEHAVIOUR))
				{
					_found = (BEHAVIOUR)behaviour;
					return true;
				}
			}

			_found = null;
			return false;
		}
		
		public override void Init(IMCCPlayer _playerInterface)
		{
			foreach(ModularBehaviour subCamera in subCameras)
			{
				subCamera.Init(_playerInterface);
				subCamera.Enabled = false;
			}
			
			subCameras[currentCameraIndex].Enabled = true;
		}
		
		public void ActivateCamera(int _camIndex)
		{
			for (int i = 0; i < subCameras.Count; i++)
			{
				subCameras[i].enabled = false;
				subCameras[i].GetComponentInChildren<Camera>().enabled = false;
			}
			currentCameraIndex = _camIndex;
			
			subCameras[currentCameraIndex].GetComponentInChildren<Camera>().enabled = true;
			subCameras[currentCameraIndex].Enabled = true;
		}

		public void NextCamera()
		{
			int oldIndex = currentCameraIndex;
			
			currentCameraIndex++;
			if(currentCameraIndex >= subCameras.Count)
				currentCameraIndex = 0;
			
			Camera oldCamera = subCameras[oldIndex].GetComponentInChildren<Camera>();
			Rect oldCameraRect = oldCamera.rect;
			
			Camera newCamera = subCameras[currentCameraIndex].GetComponentInChildren<Camera>();
			newCamera.rect = oldCameraRect;
			subCameras[currentCameraIndex].Enabled = true;
			newCamera.enabled = true;
			
			subCameras[oldIndex].Enabled = false;
			oldCamera.enabled = false;
		}

		protected override void OnProcess(UpdatePhase _phase)
		{
			UpdatePhase currentCamPhase = subCameras[currentCameraIndex].UpdatePhase;

			if ((_phase != currentCamPhase && _phase != UpdatePhase.Any) || !Enabled)
				return;

			subCameras[currentCameraIndex].Process(_phase);
		}
	}
}