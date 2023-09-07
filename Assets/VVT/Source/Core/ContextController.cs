using UnityEngine;
using System;

namespace VVT {

    [DefaultExecutionOrder(-50)]
    public sealed class ContextController : MonoBehaviour, IContextService {

        [field:SerializeField] public GameContextInfo Info { get; private set; }
        public event Action<GameContext, GameContext> OnContextChanged;

        private void Awake     () => Services.Instance.RegisterService<IContextService>(this);
        private void OnDisable () => Services.Instance.UnRegisterService<IContextService>();

        public void UpdateGameContext(GameContext newContext, GameContext previousContext) {
            Info.CanToggleGamePause = newContext.AllowPause;
            Info.PreviousContext = previousContext;
            Info.CurrentContext  = newContext;

            Logs.SystemLog($"{Logs.Bold("Game context")} : {newContext}");

            OnContextChanged?.Invoke(previousContext, newContext);
        }

        public void UpdateGameContext(GameContext newContext) {
            Info.CanToggleGamePause = newContext.AllowPause;
            Info.PreviousContext = Info.PreviousContext;
            Info.CurrentContext  = newContext;

            Logs.SystemLog($"{Logs.Bold("Game context")} : {newContext}");

            OnContextChanged?.Invoke(newContext, Info.PreviousContext);
        }

        public void ToggleGamePause() {
            Info.GamePaused = !Info.GamePaused;
            Time.timeScale = Info.GamePaused ? 0.0f : 1.0f;
            Logs.SystemLog($"Game pause toggled to " + Info.GamePaused, LogColor.Green);
        }

        public void QuitApplication() {
            Application.Quit();
        }

        

    }
}