// Creator: 
// Creation Time: 2022/06/06 12:48
using ModularCharacterController.Cameras;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController.Motors
{
	public class ThirdPersonMotor : BaseMotorModularBehaviour
	{
		
		[Header("ThirdPersonMotor")]
		[SerializeField] private Transform renderTransform;
		[SerializeField] private float renderAlignmentSpeed = 3f;
		
		private new ThirdPersonCamera camera;

		public override void Init(IMCCPlayer _playerInterface)
		{
			base.Init(_playerInterface);
			if(_playerInterface.TryGetBehaviour(out ThirdPersonCamera cam))
				camera = cam;

			if(_playerInterface.TryGetBehaviour(out MultiCamera multiCamera))
			{
				if(multiCamera.TryGetBehaviour(out cam))
					camera = cam;
			}
		}
		
		protected override void HandleMovement(Vector2 _axis)
		{
			
			if(_axis.magnitude > 0)
				renderTransform.localRotation = Quaternion.Slerp(renderTransform.localRotation, Quaternion.LookRotation(camera.transform.forward), Time.deltaTime * renderAlignmentSpeed);

			base.HandleMovement(_axis);
		}

	}
}