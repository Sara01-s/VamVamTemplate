using VamVam.Source.Utils;
using UnityEngine;
using System;

namespace VamVam.Source.Core {  

    [ExecuteAlways]
    public sealed class GameContextUpdater {

        public static Action<GameContext, GameContext> OnGameContextChanged;

        public static void UpdateGameContext(GameContext previousContext, GameContext newContext) {
            GameContextData.CanToggleGamePause = false;
            GameContextData.Context = newContext;
            GameContextData.PreviousContext = previousContext;

            switch (GameContextData.Context) {
                case GameContext.OnMainTitle:   HandleMainTitle();   break;
                case GameContext.OnSaveSlots:   HandleSaveSlots();   break;
                case GameContext.OnPlayingGame: HandlePlaying();     break;
                case GameContext.OnGamePaused:  HandlePause();       break;
                case GameContext.OnGameOver:    HandleFail();        break;
                case GameContext.OnGameEnd:     HandleGameEnd();     break;
                case GameContext.OnLoading:     HandleLoading();     break;
                case GameContext.OnSettings:    HandleConfiguring(); break;
                case GameContext.OnTestingGame: HandleTestMode();    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newContext), newContext, "Invalid game context");
            }

            LogUtils.SystemLog($"{LogUtils.Bold("Game context")} : {newContext}");

            // This is the same as OnGameStateChanged(); but checking if the delegate is null
            OnGameContextChanged?.Invoke(previousContext, newContext);
        }

        private static void HandleMainTitle() { }

        private static void HandleSaveSlots() { }

        private static void HandlePlaying() {
            GameContextData.CanToggleGamePause = true;
            GameContextData.GamePaused = false;

            Time.timeScale = 1;

            EventDispatcher.OnGamePauseToggled?.Invoke(GameContextData.GamePaused);
        }

        private static void HandlePause() {
            GameContextData.CanToggleGamePause = true;
            GameContextData.GamePaused = true;

            Time.timeScale = 0;
            
            EventDispatcher.OnGamePauseToggled?.Invoke(GameContextData.GamePaused);
        }

        private static void HandleFail() {
            EventDispatcher.OnGameOver?.Invoke();
            GameContextData.PlayerHasControl = false;
        }

        private static void HandleGameEnd() { }

        private static void HandleConfiguring() { }

        private static void HandleLoading() { }
        
        private static void HandleTestMode() { }

    }
}