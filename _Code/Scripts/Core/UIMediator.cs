using UnityEngine.SceneManagement;
using VamVam.Source.Utils;
using VamVam.Source.Core;
using UnityEngine;

namespace VamVam.Scripts.Core {

    internal sealed class UIMediator : MonoBehaviour {

        private ISceneService _sceneService;

        private void Awake() {
            _sceneService = ServiceLocator.Instance.GetService<ISceneService>();
        }

        private void OnEnable() {
            EventDispatcher.OnPauseInput  += ToggleGamePause;
            EventDispatcher.OnEscapeInput += SettingsBack;
        }

        private void OnDisable() {
            EventDispatcher.OnPauseInput  -= ToggleGamePause;
            EventDispatcher.OnEscapeInput -= SettingsBack;
        }

        private GameContext _previousPauseContext;
        public void ToggleGamePause() {
            if (GameContextData.GamePaused)
                GameContextUpdater.UpdateGameContext(GameContext.OnGamePaused, _previousPauseContext);
            else {
                _previousPauseContext = GameContextData.Context;
                GameContextUpdater.UpdateGameContext(_previousPauseContext, GameContext.OnGamePaused);
            }
        }



                                // Button functions //
        private GameContext _previousSettingsContext;
        public void OpenSettingsMenu() {
            _previousSettingsContext = GameContextData.Context;
            GameContextUpdater.UpdateGameContext(_previousSettingsContext, GameContext.OnSettings);
        }

        public void SettingsBack() {
            if (GameContextData.Context == GameContext.OnSettings)
                GameContextUpdater.UpdateGameContext(GameContext.OnSettings, _previousSettingsContext);
        }

        private GameContext _previousBackToTitleContext;
        public void BackToMainTitle() {
            _previousBackToTitleContext = GameContextData.Context;
            if (SceneManager.GetActiveScene().buildIndex != GameCore.SceneMainTitle) {
                LoadSceneWithLoadingScreen(GameCore.SceneMainTitle);
                return;
            }
            
            GameContextUpdater.UpdateGameContext(_previousBackToTitleContext, GameContext.OnMainTitle);
        }

        public void LoadGameScene() {
            LoadSceneWithLoadingScreen(GameCore.SceneGameplay);
        }

        public void RestartScene() {
            if (GameContextData.GamePaused) EventDispatcher.OnPauseInput?.Invoke();
            _sceneService.ReloadLevel();
        }

        public void ExitGame() {
            // TODO - Some effect and/or confirmation before quitting
            Application.Quit();
        }

        public void GoToSaveSlots() {
            GameContextUpdater.UpdateGameContext(GameContext.OnMainTitle, GameContext.OnSaveSlots);
        }
        

        public void LoadSceneWithSave(int sceneIndex) {
            if (GameContextData.GamePaused) EventDispatcher.OnPauseInput?.Invoke();         // * - Resume game if paused for any reason

            LoadSceneWithLoadingScreen(sceneIndex);
        }

        private void LoadSceneWithLoadingScreen(int sceneIndex) {
            _sceneService.LoadNewScene(sceneIndex);
        }

    }
}