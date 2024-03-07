using System.Collections.Generic;
using UnityEngine;

namespace VVT {

	[CreateAssetMenu(menuName = "Events/New Game Event")]
	internal sealed class GameEvent : ScriptableObject {
		public List<GameEventListener> Listeners = new();
		
		internal void Raise() {
			foreach (var listener in Listeners) {
				listener.OnEventRaised();
			}
		}

		internal void RegisterListener(GameEventListener listener) {
			if (!Listeners.Contains(listener)) {
				Listeners.Add(listener);
			}
		}

		internal void UnregisterLister(GameEventListener listener) {
			if (Listeners.Contains(listener)) {
				Listeners.Remove(listener);
			}
		}

	}
}