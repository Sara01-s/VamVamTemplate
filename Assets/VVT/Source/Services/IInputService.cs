using UnityEngine.InputSystem;
using UnityEngine;

namespace VVT {

    public interface IInputService {

        // ¿Que ofrecerá este servicio?

        /// <summary> Changes the current Input Action Map </summary>
        void ToggleActionMap(InputActionMap actionMap);
        
    }
}