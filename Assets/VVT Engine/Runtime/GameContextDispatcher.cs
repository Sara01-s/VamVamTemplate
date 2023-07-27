using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VVT.Runtime {

    internal sealed class GameContextDispatcher : MonoBehaviour {

        internal static List<VVTGameContextEvent> GameContextListeners = new List<VVTGameContextEvent>();

        private List<VVTGameContextEvent> _toggleListeners;

        private IGameContextService _gameContext;

        private void OnEnable () => _gameContext.OnContextChanged += ToggleContextListeners;
        private void OnDisable() => _gameContext.OnContextChanged -= ToggleContextListeners;

        private void Awake() {
            GameContextListeners = Object.FindObjectsOfType<VVTGameContextEvent>(true).ToList();
            _toggleListeners = GameContextListeners.FindAll((listener) => listener.EnableOnContext);
            _gameContext = Services.Instance.GetService<IGameContextService>();
        }

        private void ToggleContextListeners(GameContext previous, GameContext newContext) {
            _toggleListeners.ForEach((listener) => listener?.gameObject.SetActive(newContext == listener.ListenTo));
        }

    }
}