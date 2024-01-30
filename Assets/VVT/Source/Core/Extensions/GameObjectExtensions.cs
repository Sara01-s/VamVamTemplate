using UnityEngine;

namespace VVT {

    public static class GameObjectExtensions {

		/// <summary>
        /// Returns the parent GameObject of the provided GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject whose parent is to be returned.</param>
        /// <returns>The parent GameObject of the provided GameObject.</returns>
		public static GameObject GetParentObject(this GameObject gameObject) {
			return gameObject.transform.parent.gameObject;
		}

    }
}
