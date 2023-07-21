using UnityEngine;

namespace VVT.Runtime {

    internal sealed class LoadingScreen : MonoBehaviour {
        public void DisableLoadingScreen() => gameObject.SetActive(false);
    }
}
