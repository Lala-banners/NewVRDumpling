// Creator: Kieran
// Creation Time: 2022/06/06 11:01
using System;
using ModularCharacterController.Cameras;

using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController.Motors
{
	public class FpsMotor : BaseMotorModularBehaviour
	{
		private new FpsCamera camera;

		public override void Init(IMCCPlayer _playerInterface)
		{
			base.Init(_playerInterface);
			if (_playerInterface.TryGetBehaviour(out FpsCamera cam))
				camera = cam;
			if (_playerInterface.TryGetBehaviour(out MultiCamera multiCamera))
			{
				if (multiCamera.TryGetBehaviour(out cam))
					camera = cam;
			}
		}
	}
}