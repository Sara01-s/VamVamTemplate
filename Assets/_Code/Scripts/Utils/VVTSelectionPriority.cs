using UnityEngine;

namespace VamVam.Scripts.Utils {
    /// <summary> Gives editor scene view selection priority to the object who has this component.</summary>
    [SelectionBase]
    internal sealed class VVTSelectionPriority : MonoBehaviour { 
        [Tooltip("This component gives scene view selection priority to this object.")]
        [SerializeField, Range(0.0f, 0.0f)] private float _hoverMe;
    }
}