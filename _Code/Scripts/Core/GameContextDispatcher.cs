using System.Collections.Generic;
using VamVam.Source.Core;
using System.Linq;
using UnityEngine;

namespace VamVam.Scripts.Core {

    internal sealed class GameContextDispatcher : MonoBehaviour {

        internal static List<VVTGameContextEvent> GameContextListeners = new List<VVTGameContextEvent>();

        private List<VVTGameContextEvent> _toggleListeners;

        private void OnEnable() {
            GameContextUpdater.OnGameContextChanged += ToggleContextListeners;
        }

        private void OnDisable() {
            GameContextUpdater.OnGameContextChanged -= ToggleContextListeners;
        }

        private void Awake() {
            GameContextListeners = Object.FindObjectsOfType<VVTGameContextEvent>(true).ToList();
            _toggleListeners = GameContextListeners.FindAll((listener) => listener.EnableOnContext);
        }

        private void ToggleContextListeners(GameContext previous, GameContext newContext) {
            _toggleListeners.ForEach((listener) => listener?.gameObject.SetActive(newContext == listener.ListenTo));
        }

    }
}