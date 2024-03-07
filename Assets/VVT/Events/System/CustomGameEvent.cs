using System.Collections.Generic;
using UnityEngine;

namespace VVT {

	[CreateAssetMenu(menuName = "Events/New Custom Game Event")]
	internal sealed class CustomGameEvent : ScriptableObject {

		public List<CustomGameEventListener> Listeners = new();
		
		internal void Raise(Component sender, object data) {
			foreach (var listener in Listeners) {
				listener.OnEventRaised(sender, data);
			}
		}

		internal void RegisterListener(CustomGameEventListener listener) {
			if (!Listeners.Contains(listener)) {
				Listeners.Add(listener);
			}
		}

		internal void UnregisterLister(CustomGameEventListener listener) {
			if (Listeners.Contains(listener)) {
				Listeners.Remove(listener);
			}
		}

	}
}