using UnityEngine.Events;
using UnityEngine;

namespace VVT.Runtime {

    // ! Currently, you can't use this script in script that live in normal scenes. 
    // ! Only use in Don't Destroy On Load scene for now.

    internal sealed class VVTGameContextEvent : MonoBehaviour {

        [field:SerializeField] internal GameContext ListenTo { get; private set; }
        [field:SerializeField] internal bool EnableOnContext { get; private set; }

        [Header("Optional")]
        [SerializeField] private UnityEvent CustomEvent;
        
        private IContextService _gameContext;

        private void Awake() {
            _gameContext = Services.Instance.GetService<IContextService>();
            gameObject.SetActive(_gameContext.Info.CurrentContext == ListenTo);
        }

        private void OnEnable () => _gameContext.OnContextChanged += OnContextChange;
        private void OnDisable() => _gameContext.OnContextChanged -= OnContextChange;

        private void OnContextChange(GameContext previous, GameContext newContext) {
            if (newContext == ListenTo) {
                CustomEvent?.Invoke();
            }
        }

    }
}
