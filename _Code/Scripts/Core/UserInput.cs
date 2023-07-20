using UnityEngine.InputSystem;
using VamVam.Resources.Input;
using VamVam.Source.Core;
using UnityEngine;
using System;

namespace VamVam.Scripts.Core {

    public sealed class UserInput : MonoBehaviour {

        [SerializeField] private bool _startWithInputEnabled;

        private static event Action<InputActionMap> OnActionMapChanged;
        private static PlayerInputActions _playerInput;

        public static float LastInteraction { get; private set; } = 0.0f;
        public static bool ActionPressed { get; private set; } = false;
        public static bool JumpPressed  { get; private set; } = false;
        public static bool JumpReleased { get; private set; } = false;
        public static Vector2 MovementDirection  { get; private set; } = Vector2.zero;
        public static Vector2 MouseDelta     { get; private set; } = Vector2.zero;
        public static Vector2 MousePosition  { get; private set; } = Vector2.zero;

        private InputAction _inputPause;
        private InputAction _inputInteraction;
        private InputAction _inputEscape;
        private InputAction _inputMovement;
        private InputAction _inputMouseDelta;
        private InputAction _inputSpace;

        private void Awake() {
            _playerInput = new PlayerInputActions();
            ToggleActionMap(_playerInput.Gameplay);
        }


        private void Start() {
            if (_startWithInputEnabled)
                GameContextData.PlayerHasControl = true;
        }

        private void Update() {
            if (!GameContextData.PlayerHasControl) return;

            ActionPressed = _inputInteraction.WasPerformedThisFrame();
            JumpReleased = _inputSpace.WasReleasedThisFrame();
            JumpPressed = _inputSpace.IsPressed();
            MovementDirection = _inputMovement.ReadValue<Vector2>();
            MousePosition = Mouse.current.position.ReadValue();
            MouseDelta = _inputMouseDelta.ReadValue<Vector2>();
        }


        // Event dispatch
        private void DispatchPauseInput(InputAction.CallbackContext context) {
            if (GameContextData.CanToggleGamePause)
                EventDispatcher.OnPauseInput.Invoke();
        }

        private void DispatchInteractionInput(InputAction.CallbackContext context) {
            LastInteraction = Time.time;
            EventDispatcher.OnInteractionInput?.Invoke();
        }

        private void DispatchEscapeInput(InputAction.CallbackContext context) {
            EventDispatcher.OnEscapeInput?.Invoke();
        }

        // Event subscription
        private void OnEnable() {
            _inputPause       = _playerInput.Gameplay.TogglePause;
            _inputInteraction = _playerInput.Gameplay.Interact;
            _inputEscape      = _playerInput.Gameplay.Escape;
            _inputMovement    = _playerInput.Gameplay.Movement;
            _inputMouseDelta  = _playerInput.Gameplay.MouseLook;
            _inputSpace       = _playerInput.Gameplay.Jump;

            _inputPause         .Enable();
            _inputInteraction   .Enable();
            _inputEscape        .Enable();
            _inputMovement      .Enable();
            _inputMouseDelta    .Enable();
            _inputSpace       .Enable();

            _inputPause.performed           += DispatchPauseInput;
            _inputInteraction.performed     += DispatchInteractionInput;
            _inputEscape.performed          += DispatchEscapeInput;
        }

        private void OnDisable() {
            _inputPause         .Disable();
            _inputInteraction   .Disable();
            _inputEscape        .Disable();
            _inputMovement      .Disable();
            _inputMouseDelta    .Disable();
            _inputSpace       .Disable();

            _inputPause.performed           -= DispatchPauseInput;
            _inputInteraction.performed     -= DispatchInteractionInput;
            _inputEscape.performed          -= DispatchEscapeInput;
        }

        private static void ToggleActionMap(InputActionMap actionMap) {
            if (actionMap.enabled) return;

            _playerInput.Disable();
            OnActionMapChanged?.Invoke(actionMap);
            actionMap.Enable();
        }

    }
}
