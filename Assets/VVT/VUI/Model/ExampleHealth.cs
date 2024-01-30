using static Unity.Mathematics.math;
using UnityEngine;

namespace VVT.UI {

	internal sealed class Health {

		internal int CurrentHealth { get; private set; }

		private int _startingHealth;

		internal Health(int startingHealth) {
			_startingHealth = startingHealth;
			Reset();
		}

		private void Reset() {
			CurrentHealth = _startingHealth;
		}

		internal void Decrease(int amount) {
			CurrentHealth -= abs(amount);
		}

		internal void Increase(int amount) {
			CurrentHealth += abs(amount);
		}

	}
}