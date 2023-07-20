using VamVam.Source.Utils;

/// <summary> Base namespace for all Vam Vam Studios Video games. </summary>
namespace VamVam.Scripts.Core {
    /// <summary>
    /// This class will make every parented GameObject a persistent object 
    /// </summary>
    internal sealed class GameCore : PersistentSingleton<GameCore> { 
        
        /// <summary> Main title scene build index                  </summary>
        internal const int SceneMainTitle = 0;
        /// <summary> Tutorial scene build index. -1 if no tutorial </summary>
        internal const int SceneTutorial  = -1;
        /// <summary> Gameplay scene build index                    </summary>
        internal const int SceneGameplay  = 1;

    }
}