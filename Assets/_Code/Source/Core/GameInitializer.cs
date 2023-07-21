using UnityEngine;

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

    }
}
