// Creator: Kieran
// Creation Time: 2022/06/06 16:41
using UnityEngine;
using UnityEngine.InputSystem;

namespace ModularCharacterController.Combat
{
    public class CombatBehaviour : ModularBehaviour
    {
        [HideInInspector] public bool shootInput;

        protected override void OnProcess(UpdatePhase _phase)
        {
            if(shootInput)
            {
                Shoot();
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            shootInput = context.action.triggered;
        }
        
        private void Shoot()
        {
            Debug.Log("Bang");
            shootInput = false;
        }
        
    }
}