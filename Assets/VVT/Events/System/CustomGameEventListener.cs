using UnityEngine.Events;
using UnityEngine;

namespace VVT {

	[System.Serializable]
	internal sealed class GameEventWithParams : UnityEvent<Component, object> 
	{
	}

	internal sealed class CustomGameEventListener : MonoBehaviour {
		
		[SerializeField] private CustomGameEvent _gameEvent;
		[SerializeField] private GameEventWithParams _callback;

		private void OnEnable() {
			_gameEvent.RegisterListener(this);
		}

		private void OnDisable() {
			_gameEvent.UnregisterLister(this);
		}

		internal void OnEventRaised(Component sender, object data) {
			_callback?.Invoke(sender, data);
		}

	}
}