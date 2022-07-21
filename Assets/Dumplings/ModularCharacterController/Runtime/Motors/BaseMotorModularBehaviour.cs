using ModularCharacterController.Cameras;
using UnityEngine;
using UnityEngine.InputSystem;
namespace ModularCharacterController.Motors
{
    public abstract class BaseMotorModularBehaviour : ModularBehaviour
    {
        [Header("BaseMotorModularBehaviour")]
        [SerializeField] protected MovementSettings settings;
        [SerializeField] protected bool skipMovementRayChecks = true;
        public bool IsGrounded { get; protected set; }
        public Rigidbody Rigidbody { get; protected set; }

        protected new CapsuleCollider collider;
        protected new Rigidbody rigidbody;
        protected Transform player;
        
        protected const float SPEED_ON_GROUND_MODIFIER = 1;
        protected const float SPEED_IN_AIR_MODIFIER = 1;
        protected const float IN_AIR_GROUND_CHECK_DELAY = 0.1f;
        
        protected float lastTimeInAir;
        [SerializeField] protected bool isJumpPressed;
        protected Vector2 moveInput;
        protected bool movingUsingThisComponent;
        
        public override void Init(IMCCPlayer _playerInterface)
        {
            base.Init(_playerInterface);
            Rigidbody = _playerInterface.Rigidbody;
            collider = (CapsuleCollider) _playerInterface.Collider;
            player = _playerInterface.Transform;
            rigidbody = _playerInterface.Rigidbody;
        }
        
        protected override void OnProcess(UpdatePhase _phase)
        {
            if (!movingUsingThisComponent)
                return;
            CheckGrounded();
            HandleMovement(moveInput);
            ApplyExtraGravity();
        }
        
        /// <summary>
        /// Detect if the player motor is actually grounded; ie; not jumping or over nothing
        /// </summary>
        private void CheckGrounded()
        {
            // Todo: Fix this so it correctly ground checks!!
            // Use a smaller ground distance check if in air, to prevent suddenly snapping to ground
            float chosenGroundCheckDistance = settings.GetGroundDistanceCheck(IsGrounded);
            // If we aren't grounded and still going up skip this check.
            if(IsGrounded == false && rigidbody.velocity.y >= 0.01) return;
            // Check if the current time is greater than the required jump check time
            if(Time.time >= lastTimeInAir + IN_AIR_GROUND_CHECK_DELAY)
            {
                // Get all layers below us in the correct distance
                RaycastHit[] hits = CapsuleCastAllInDirection(-player.up, chosenGroundCheckDistance);

                // If there is actually anything in the array, we can loop through it, otherwise, we aren't grounded
                if(hits.Length > 0)
                {
                    foreach(RaycastHit hit in hits)
                    {
                        // Check if we are touching the ground, if so, set grounded to true and return
                        if(hit.transform.CompareTag(settings.GroundTag) || hit.transform.gameObject.layer == 0)
                        {
                            IsGrounded = true;
                            return;
                        }
                    }
                }
                else
                {
                    // We are standing above nothing so we aren't grounded
                    IsGrounded = false;
                }
            }
        }
        
        
        /// <summary>
        /// Handles the movement of the player by the axis of the moveAction
        /// </summary>
        /// <param name="_axis">The axis the controller or keyboard is requesting</param>
        protected virtual void HandleMovement(Vector2 _axis)
        {
            // Calculate the max speed and the speed modifier by the grounded state
            float maxSpeed = settings.GetMaxSpeed(IsGrounded);
            float modifier = IsGrounded ? SPEED_ON_GROUND_MODIFIER : SPEED_IN_AIR_MODIFIER;

            // Calculate the correct velocity by the axis of the input
            Vector3 forward = player.forward * _axis.y;
            Vector3 right = player.right * _axis.x;
            Vector3 desiredVelocity = (forward + right) * (maxSpeed * modifier) - Rigidbody.velocity;

            // Check we can move this way, if we can apply the velocity
            if(CanMoveInDirection(desiredVelocity) || skipMovementRayChecks)
                Rigidbody.AddForce(new Vector3(desiredVelocity.x, 0, desiredVelocity.z), ForceMode.Impulse);
        }
        
        protected override void OnEnabledStateChanged(bool _newState)
        {
            movingUsingThisComponent = _newState;
        }
        
        /// <summary>
        /// Cast a Capsule in the passed direction and get all hit objects in that direction.
        /// </summary>
        /// <param name="_direction">The direction to cast the capsule</param>
        /// <param name="_distance">How far the capsule will travel</param>
        private RaycastHit[] CapsuleCastAllInDirection(Vector3 _direction, float _distance)
        {
            // Todo: Fix this and push to FPSMotor, looks like it is currently always returning hits and breaking everything that uses it!!
            Vector3 top = transform.position + collider.center + Vector3.up * ((collider.height * 0.5f) - collider.radius);
            Vector3 bot = transform.position + collider.center - Vector3.up * ((collider.height * 0.5f) - collider.radius);

            // Cast the capsule in the passed direction and distance
            return Physics.CapsuleCastAll(top, bot, collider.radius * 0.95f, _direction, _distance, settings.LayerChecks);
        }
        
        
        /// <summary>
        /// Attempt to apply extra gravity depending on the state of the rigidbody and jump button
        /// </summary>
        private void ApplyExtraGravity()
        {
            // Check if we are falling, if we are apply normal snappy fall
            if(Rigidbody.velocity.y < 0)
            {
                Rigidbody.velocity += Vector3.up * (Physics.gravity.y * settings.FallMultiplier * Time.deltaTime);
            }
            // We are rising, but we aren't pressing the jump button, so fall faster
            else if(Rigidbody.velocity.y > 0 && !isJumpPressed)
            {
                Rigidbody.velocity += Vector3.up * (Physics.gravity.y * settings.LowJumpMultiplier * Time.deltaTime);
            }
        }
        
        
        private bool CanMoveInDirection(Vector3 _targetDir)
        {
            // Find everything in the direction we are attempting to move at least 1cm away
            RaycastHit[] hits = CapsuleCastAllInDirection(_targetDir, 0.01f);
            foreach(RaycastHit hit in hits)
            {
                if(hit.collider.CompareTag(settings.WallTag) || hit.transform.gameObject.layer == 0)
                {
                    // We will walk into a wall so don't move that way
                    return false;
                }
            }

            // We can move this way since we won't walk into a wall
            return true;
        }

        #region Movement Inputs
        public void OnMove(InputAction.CallbackContext context)
        {
            if (!movingUsingThisComponent)
                return;
            moveInput = context.ReadValue<Vector2>();
        }
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if (!movingUsingThisComponent)
                return;
			
            if (context.performed)
            {
                OnJumpPerformed();
            }
            if (context.canceled)
            {
                OnJumpCanceled();
            }
        }

        /// <summary>
        /// Fired when the jump action is pressed.
        /// </summary>
        private void OnJumpPerformed()
        {
            isJumpPressed = true;
			
            // If we are grounded jump and store the jump time
            if(IsGrounded)
            {
                Rigidbody.AddForce(Vector3.up * settings.JumpForce, ForceMode.Impulse);
                IsGrounded = false;

                lastTimeInAir = Time.time;
            }
        }
		
        /// <summary>
        /// Fired when the jump action either a) fails or b) is released
        /// </summary>
        private void OnJumpCanceled() => isJumpPressed = false;
        #endregion
    }
}
