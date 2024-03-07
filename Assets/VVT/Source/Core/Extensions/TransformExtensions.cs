using UnityEngine;
using System;

namespace Game {

	public static class TransformExtensions {

		public static void ForAllChilds(this Transform transform, Action<Transform> action) {
			if (transform.childCount < 0) return;

			for (int i = 0; i < transform.childCount; i++) {
				action(transform.GetChild(i));
			}
		}

	}
}