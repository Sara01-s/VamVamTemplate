using UnityEngine;

namespace VamVam.Source.Utils {

    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour {

        public static T Instance { get; private set; }

        protected StaticInstance() { }

        protected virtual void Awake() {
            Instance = this as T;
        }

        protected virtual void OnAppQuit() {
            Instance = null;
            Destroy(gameObject);
            Debug.LogWarning("Static instance destroyed");
        }
    }

    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour {

        protected override void Awake() {
            if (Instance != null) {
                Debug.LogWarning("A singleton duplicate has been destroyed");
                Destroy(gameObject);
            }
            base.Awake(); // Instance = this as T;
        }
    }

    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour {

        protected override void Awake() {
            base.Awake(); // Instance = this;
            DontDestroyChildOnLoad(gameObject);
        }

        internal static void DontDestroyChildOnLoad(GameObject child) {
            Transform parentTransform = child.transform;

            // If this transform doesn't have a parent, keep searching
            while (parentTransform.parent != null) {
                parentTransform = parentTransform.parent;
            }

            GameObject.DontDestroyOnLoad(parentTransform.gameObject);
        }
    }
}