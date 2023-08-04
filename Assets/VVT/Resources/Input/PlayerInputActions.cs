//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/VVT/Resources/Input/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace VVT.Input
{
    public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""684c2d9b-23cc-4f44-bd9c-b64335a46e80"",
            ""actions"": [
                {
                    ""name"": ""TogglePause"",
                    ""type"": ""Button"",
                    ""id"": ""d573ad94-df07-4a8b-9f80-5a6e9b74f4a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""161bc779-4f28-4391-b481-07de70ec1d75"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""072c194e-bf88-476d-a6d5-a2b366fd3fef"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ddb6c76b-0ad9-4a33-890f-86600faac9d5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""1429ba97-5a7c-4fef-bc2f-f04d3222cb8a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""66019a4a-ec91-45c8-8cc9-bdaa22f0c663"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8d1bb4d4-13dd-4384-8842-91f143bd6f77"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61b0a1a1-fc4b-4048-9fd1-efbd6b4182c9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b3e616f8-cafe-4299-ae51-10bb03379870"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""76375f7f-1225-4cef-b89a-2377119cb59f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5e29bf4d-b1a0-4b4f-a4cb-6fb829b5f447"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0caca8a8-5bc9-417a-a5ba-dd30c9f7d289"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""793b8f12-998b-4638-b7aa-583a2d4d8631"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""86c02b56-109c-44c9-bc1e-bd466d47f052"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47fe59e3-7395-4f0e-a323-8fa466480396"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ecd7568-243c-4ecd-91bf-db86c29d5b95"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_TogglePause = m_Gameplay.FindAction("TogglePause", throwIfNotFound: true);
            m_Gameplay_Escape = m_Gameplay.FindAction("Escape", throwIfNotFound: true);
            m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
            m_Gameplay_MouseLook = m_Gameplay.FindAction("MouseLook", throwIfNotFound: true);
            m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
            m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_TogglePause;
        private readonly InputAction m_Gameplay_Escape;
        private readonly InputAction m_Gameplay_Movement;
        private readonly InputAction m_Gameplay_MouseLook;
        private readonly InputAction m_Gameplay_Interact;
        private readonly InputAction m_Gameplay_Jump;
        public struct GameplayActions
        {
            private @PlayerInputActions m_Wrapper;
            public GameplayActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @TogglePause => m_Wrapper.m_Gameplay_TogglePause;
            public InputAction @Escape => m_Wrapper.m_Gameplay_Escape;
            public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
            public InputAction @MouseLook => m_Wrapper.m_Gameplay_MouseLook;
            public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
            public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @TogglePause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTogglePause;
                    @TogglePause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTogglePause;
                    @TogglePause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTogglePause;
                    @Escape.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Escape.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                    @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @MouseLook.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
                    @MouseLook.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
                    @MouseLook.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
                    @Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                    @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @TogglePause.started += instance.OnTogglePause;
                    @TogglePause.performed += instance.OnTogglePause;
                    @TogglePause.canceled += instance.OnTogglePause;
                    @Escape.started += instance.OnEscape;
                    @Escape.performed += instance.OnEscape;
                    @Escape.canceled += instance.OnEscape;
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @MouseLook.started += instance.OnMouseLook;
                    @MouseLook.performed += instance.OnMouseLook;
                    @MouseLook.canceled += instance.OnMouseLook;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        public interface IGameplayActions
        {
            void OnTogglePause(InputAction.CallbackContext context);
            void OnEscape(InputAction.CallbackContext context);
            void OnMovement(InputAction.CallbackContext context);
            void OnMouseLook(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
        }
    }
}
