using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VVT.Runtime {

    internal sealed class ContextEventDispatcher : MonoBehaviour {

        internal static List<VVTContextEvent> GameContextListeners = new();

        private List<VVTContextEvent> _toggleListeners;
        private IContextService _contextService;

        private void Awake() {
            GameContextListeners = FindObjectsOfType<VVTContextEvent>(includeInactive: true).ToList();
            
            _toggleListeners = GameContextListeners.FindAll((listener) => listener.EnableOnContext);
            _contextService = Services.Instance.GetService<IContextService>();
        }

		private void OnEnable () => _contextService.OnContextChanged += ToggleContextListeners;
        private void OnDisable() => _contextService.OnContextChanged -= ToggleContextListeners;

        private void ToggleContextListeners(Context previous, Context newContext) {
			foreach (var listener in _toggleListeners) {
				if (listener != null)
                    listener.gameObject.SetActive(newContext == listener.ListenTo);
			}
        }

    }
}