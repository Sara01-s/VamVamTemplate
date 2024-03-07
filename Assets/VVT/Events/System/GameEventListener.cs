using UnityEngine.Events;
using UnityEngine;

namespace VVT {

	internal sealed class GameEventListener : MonoBehaviour {
		
		[SerializeField] private GameEvent _gameEvent;
		[SerializeField] private bool _subscribeManually;
		[SerializeField] private UnityEvent _callback;

		private bool _registered;

		internal void Register() {
			if (!_subscribeManually) {
				Debug.LogError("Tried to register to game event manually, but configuration indicates automatic suscription");
				return;
			}

			if (_registered) {
				Debug.LogError("Tried to register to already registered game event.");
				return;
			}

			_gameEvent.RegisterListener(this);
			_registered = true;
		}

		internal void Unregister() {
			if (!_subscribeManually) {
				Debug.LogError("Tried to unregister to game event manually, but manual subscription is not being used.");
				return;
			}

			if (!_registered) {
				Debug.LogError("Tried to unregister from none game event.");
				return;
			}
			
			_gameEvent.RegisterListener(this);
			_registered = false;
		}

		private void OnEnable() {
			if (_subscribeManually) return;
			_gameEvent.RegisterListener(this);
		}

		private void OnDisable() {
			if (_subscribeManually) return;
			_gameEvent.UnregisterLister(this);
		}

		internal void OnEventRaised() {
			_callback?.Invoke();
		}

	}
}