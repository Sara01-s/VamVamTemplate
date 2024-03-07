using UnityEngine;

namespace VVT.Runtime {

    internal sealed class VVTAutoDestroy : MonoBehaviour {
		
		[field:SerializeField, Min(0.0f)] internal float LifeTimeSeconds;

		private void Awake() {
			Destroy(gameObject, LifeTimeSeconds);
		}

    }
}
