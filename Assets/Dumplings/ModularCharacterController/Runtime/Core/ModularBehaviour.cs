// Creator: Kieran & James
// Creation Time: 2022/04/11 09:01
using UnityEngine;

namespace ModularCharacterController
{
	/// <summary>
	/// These are an abstract class that is used as a base of all all components.
	/// E.g. Camera and Motion.
	/// </summary>
	public abstract class ModularBehaviour : MonoBehaviour
	{
		public bool Enabled
		{
			get => intEnabled;
			set
			{
				intEnabled = value;
				OnEnabledStateChanged(intEnabled);
			}
		}

		public bool LocalOnly => localOnly;
		public bool toBeUsedInMMCParent = false;

		public UpdatePhase UpdatePhase => updatePhase;
		
		[SerializeField] private bool localOnly = true;
		[SerializeField] private UpdatePhase updatePhase = UpdatePhase.Update;
		private bool intEnabled = true;

		public virtual void Init(IMCCPlayer _playerInterface) { }
		
		public void Process(UpdatePhase _phase)
		{
			if((_phase != updatePhase && _phase != UpdatePhase.Any) || !Enabled)
				return;
			
			OnProcess(_phase);
		}

		protected abstract void OnProcess(UpdatePhase _phase);

		protected virtual void OnEnabledStateChanged(bool _newState) { }
	}
}