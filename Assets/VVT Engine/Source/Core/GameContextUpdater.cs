using UnityEngine;
using System;

namespace VVT {

    public sealed class GameContextUpdater : MonoBehaviour, IGameContextService {

        [field:SerializeField] public GameContextData Data { get; private set; }
        
        [SerializeField] private GameContext _playingContext;
        [SerializeField] private GameContext _pauseContext;

        public Action<GameContext, GameContext> OnContextChanged { get; set; }

        private void Awake     () => Services.Instance.RegisterService<IGameContextService>(this);
        private void OnDisable () => Services.Instance.UnRegisterService<IGameContextService>();

        public void UpdateGameContext(GameContext previousContext, GameContext newContext) {
            Data.CanToggleGamePause = false;
            Data.CurrentContext = newContext;
            Data.PreviousContext = previousContext;

            // ! Please... Change this.
            if (newContext == _playingContext)
                HandlePlaying();
            else if (newContext == _pauseContext)
                HandlePause();
            // ! Please... Change this.

            Logs.SystemLog($"{Logs.Bold("Game context")} : {newContext}");

            OnContextChanged?.Invoke(previousContext, newContext);
        }

        private void HandlePlaying() {
            Data.CanToggleGamePause = true;
            Data.GamePaused = false;

            Time.timeScale = 1;

            EventDispatcher.OnGamePauseToggled?.Invoke(Data.GamePaused);
        }

        private void HandlePause() {
            Data.CanToggleGamePause = true;
            Data.GamePaused = true;

            Time.timeScale = 0;
            
            EventDispatcher.OnGamePauseToggled?.Invoke(Data.GamePaused);
        }

    }
}