using VamVam.Source.Core;
using UnityEngine.Events;
using UnityEngine;

namespace VamVam.Scripts.Core {

    // ! Currently, you can't use this script in script that live in normal scenes. 
    // ! Only use in Don't Destroy On Load scene for now.

    internal sealed class VVTGameContextEvent : MonoBehaviour {

        [field:SerializeField] internal GameContext ListenTo { get; private set; }
        [field:SerializeField] internal bool EnableOnContext { get; private set; }

        [Header("Optional")]
        [SerializeField] private UnityEvent CustomEvent;

        private void Awake() {
            gameObject.SetActive(GameContextData.Context == ListenTo);
        }

        private void OnEnable() {
            GameContextUpdater.OnGameContextChanged += OnContextChange;
        }

        private void OnDisable() {
            GameContextUpdater.OnGameContextChanged -= OnContextChange;
        }

        private void OnContextChange(GameContext previous, GameContext newContext) {
            if (newContext == ListenTo) {
                CustomEvent?.Invoke();
            }
        }

    }
}
