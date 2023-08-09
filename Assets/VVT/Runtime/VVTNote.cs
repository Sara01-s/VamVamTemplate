using NaughtyAttributes;
using UnityEngine;

namespace VVT.Runtime {
    /// <summary> Attach this component to a game object to give it a note o description </summary>
    internal sealed class VVTNote : MonoBehaviour {

        [ResizableTextArea]
        [SerializeField] private string _note;

    }
}