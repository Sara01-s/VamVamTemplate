using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace VVT {
    /// <summary>
    /// This class will instantiate the "Game Core",
    /// a prefab that contains all cross-scene persistent Systems,
    /// this prefab always lives in the "Don't destroy on load" Unity scene.
    /// The creation of the object happens before any scene is loaded
    /// </summary>
    internal static class GameCoreInitializer {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeSystems() {
            UnityEngine.Object.DontDestroyOnLoad(
                UnityEngine.Object.Instantiate(Resources.Load("GameCore")) as GameObject);
        }
        
#if UNITY_EDITOR
        [MenuItem("VamVam/Open GameCore")]
        public static void OpenGameCorePrefab() {
            PrefabStageUtility.OpenPrefab("Assets/VVT/Resources/GameCore.prefab");
        }
#endif
    }
}
