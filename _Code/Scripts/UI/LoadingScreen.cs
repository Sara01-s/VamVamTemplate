using UnityEngine;

namespace VamVam.Scripts.UI {

    internal sealed class LoadingScreen : MonoBehaviour {
        public void DisableLoadingScreen() => gameObject.SetActive(false);
    }
}
