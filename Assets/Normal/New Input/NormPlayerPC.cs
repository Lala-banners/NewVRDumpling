#if NORMCORE


using UnityEngine;
using UnityEngine.InputSystem;


namespace Normal.Realtime.Examples
{
	public class NormPlayerPC : MonoBehaviour
	{
		private Realtime _realtime;
		[SerializeField] private GameObject _prefab;

		// Constant
		const float jumpCheckPreventionTime = 0.5f;

		// Callback
		public delegate void CollectCoinCallback();

		public CollectCoinCallback onCollectCoin;

		// Public
		[Header("Physic Setting")] public LayerMask groundLayerMask;

		[Header("Move & Jump Setting")] public float moveSpeed = 10;
		public float fallWeight = 5.0f;
		public float jumpWeight = 0.5f;
		public float jumpVelocity = 100.0f;

		// Internal Data

		// State of the player (jumping or not)
		protected bool jumping = false; // state of player (jumping or not )

		//
		protected Vector3 moveVec = Vector3.zero; // movement speed of player
		protected float jumpTimestamp; // start jump timestamp

		protected Animator animator; // reference to the animator
		protected Rigidbody rigidbody; // reference to the rigidbody


		// Start is called before the first frame update
		private void Awake()
		{
			// Get the Realtime component on this game object
			_realtime = GetComponent<Realtime>();

			// Notify us when Realtime successfully connects to the room
			_realtime.didConnectToRoom += DidConnectToRoom;

			animator = GetComponentInChildren<Animator>();
			rigidbody = GetComponent<Rigidbody>();
		} 

		private void DidConnectToRoom(Realtime realtime)
		{
			// Instantiate the CubePlayer for this client once we've successfully connected to the room. Position it 1 meter in the air.
			var options = new Realtime.InstantiateOptions
			{
				ownedByClient = true, // Make sure the RealtimeView on this prefab is owned by this client.
				preventOwnershipTakeover = true, // Prevent other clients from calling RequestOwnership() on the root RealtimeView.
				useInstance = realtime // Use the instance of Realtime that fired the didConnectToRoom event.
			};
			Realtime.Instantiate(_prefab.name, Vector3.up, Quaternion.identity, options);
		}

		void UpdateWhenJumping()
		{
			bool isFalling = rigidbody.velocity.y <= 0;

			float weight = isFalling ? fallWeight : jumpWeight;

			// Assign new velocity
			rigidbody.velocity = new Vector3(moveVec.x * moveSpeed, rigidbody.velocity.y, moveVec.z * moveSpeed);
			rigidbody.velocity += Vector3.up * Physics.gravity.y * weight * Time.deltaTime;

			GroundCheck();
		}

		void UpdateWhenGrounded()
		{
			// 1 
			rigidbody.velocity = moveVec * moveSpeed;

			// 2
			if(moveVec != Vector3.zero)
			{
				transform.LookAt(this.transform.position + moveVec.normalized);
			}

			// 3
			CheckShouldFall();
		}

		private void FixedUpdate()
		{
			if(jumping == false)
			{
				// 2
				UpdateWhenGrounded();
			}
			else
			{
				// 3
				UpdateWhenJumping();
			}
		}

		// Update is called once per frame
		void Update()
		{
			UpdateAnimation();
		}

		public void OnJump()
		{
			HandleJump();
		}

		public void OnMove(InputValue input)
		{
			Vector2 inputVec = input.Get<Vector2>();

			moveVec = new Vector3(inputVec.x, 0, inputVec.y);
		}

		#region Jump & Fall & Ground Logic

		protected bool HandleJump()
		{
			if(jumping)
			{
				return false;
			}

			jumping = true;
			jumpTimestamp = Time.time;
			rigidbody.velocity = new Vector3(0, jumpVelocity, 0); // Set initial jump velocity

			return true;
		}

		void CheckShouldFall()
		{
			if(jumping)
			{
				return; // No need to check if in the air
			}

			bool hasHit = Physics.CheckSphere(transform.position, 0.1f, groundLayerMask);

			if(hasHit == false)
			{
				jumping = true;
			}
		}

		void GroundCheck()
		{
			if(jumping == false)
			{
				return; // No need to check
			}

			if(Time.time < jumpTimestamp + jumpCheckPreventionTime)
			{
				return;
			}

			bool hasHit = Physics.CheckSphere(transform.position, 0.1f, groundLayerMask);

			if(hasHit)
			{
				jumping = false;
			}
		}

		#endregion

		void UpdateAnimation()
		{
			if(animator == null)
			{
				return;
			}

			animator.SetBool("jumping", jumping);
			animator.SetFloat("moveSpeed", moveVec.magnitude);
		}

		#region Coin Collect Logic

		private void OnTriggerEnter(Collider other)
		{
			if(other.transform.tag == "Coin")
			{
				HandleCoinCollect(other);
			}
		}

		void HandleCoinCollect(Collider collision)
		{
			Coin coin = collision.transform.GetComponent<Coin>();
			if(coin == null)
			{
				return;
			}

			coin.Collect();

			if(onCollectCoin != null)
			{
				onCollectCoin();
			}
		}

		#endregion
	}
}
#endif