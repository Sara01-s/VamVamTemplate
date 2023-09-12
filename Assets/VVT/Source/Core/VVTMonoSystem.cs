using UnityEngine;

namespace VVT.Runtime {

    [DefaultExecutionOrder(-50)]
    public abstract class VVTMonoSystem : MonoBehaviour {
        /// <summary>
        /// System prefix to be shown in console, initialize with the system's name.
        /// (e.g. Prefix { get; set; } = "MySystem : ")
        /// </summary>
        protected abstract string Prefix { get; set; }
    }
}
