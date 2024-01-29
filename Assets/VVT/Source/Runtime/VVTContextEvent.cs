using UnityEngine.Events;
using UnityEngine;

namespace VVT.Runtime {

    // ! Currently, you can't use this script in script that live in normal scenes. 
    // ! Only use in Don't Destroy On Load scene for now.

    internal sealed class VVTContextEvent : MonoBehaviour {

        [field:SerializeField] internal Context ListenTo { get; private set; }
        [field:SerializeField] internal bool EnableOnContext { get; private set; }

        [Header("Context Events")]
        [SerializeField] private UnityEvent _onContextEnter;
		[SerializeField] private UnityEvent _onContextExit;
        
        private IContextService _contextService;
		private bool _enteredTargetContext = false;

        private void Awake() {
            _contextService = Services.Instance.GetService<IContextService>();

			if (!EnableOnContext) return;
            gameObject.SetActive(_contextService.ContextsInfo.CurrentContext == ListenTo);
        }

        private void OnEnable () => _contextService.OnContextChanged += OnContextChange;
        private void OnDisable() => _contextService.OnContextChanged -= OnContextChange;

        private void OnContextChange(Context previous, Context newContext) {

			if (newContext != ListenTo && _enteredTargetContext) {
				_enteredTargetContext = false;
				_onContextExit?.Invoke();
				return;
			}

            if (newContext == ListenTo) {
				_enteredTargetContext = true;
                _onContextEnter?.Invoke();
            }

        }

    }
}
