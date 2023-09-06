using UnityEngine;

namespace VVT {
    
    [CreateAssetMenu(menuName = "VVT/GameContext")]
    /// <summary> All possible contexts where the player could be while playing the game </summary>
    public sealed class GameContext : ScriptableObject {
        
        [field:Header("Context Settings")]
        [field:Tooltip("Game context name")]
        [field:SerializeField] public string Name { get; private set; }
        [field:Tooltip("Can the player toggle game pause?")]
        [field:SerializeField] public bool   AllowPause { get; private set; }
        [field:Tooltip("Can the player issue game inputs?")]
        [field:SerializeField] public bool   AllowControl { get; private set; }

        [Tooltip("How would you describe the purpose of this game context?")]
        [SerializeField, TextArea(2, 5)] private string _description;

    }
}