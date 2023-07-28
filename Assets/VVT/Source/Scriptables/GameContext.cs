using UnityEngine.Events;
using UnityEngine;

namespace VVT {
    
    [CreateAssetMenu(menuName = "VVT/GameContext")]
    /// <summary> All possible contexts where the player could be while playing the game </summary>
    public sealed class GameContext : ScriptableObject {
        
        [field:SerializeField] public string Name { get; private set; }
        [SerializeField, TextArea(2, 5)] private string _description;



    }
}