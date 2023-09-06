using UnityEngine;

namespace VVT {

    [CreateAssetMenu(menuName = "VVT/Game Context Info")]
    public sealed class GameContextInfo : ScriptableObject {

        [Tooltip("Current game context handled by Vam Vam Template")]
        [field:SerializeField] public GameContext CurrentContext { get; set; }
        [Tooltip("Previous game context before context update")]
        [field:SerializeField] public GameContext PreviousContext { get; set; }
        [Tooltip("Can game pause be toggled in the current context?")]
        [field:SerializeField] public bool CanToggleGamePause { get; set; } = false;
        [Tooltip("Is player input read?")]
        [field:SerializeField] public bool PlayerHasControl { get; set; } = false;
        [Tooltip("Is game currently paused?")]
        [field:SerializeField] public bool GamePaused { get; set; } = false;
        
    }
}