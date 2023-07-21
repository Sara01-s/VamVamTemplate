using UnityEngine.SceneManagement;
using UnityEngine;
using VVT;

namespace VVT.Runtime {

    /// <summary> Main UI Controller using Mediator Pattern </summary>
    internal sealed class UIController : MonoBehaviour {

        [SerializeField] private GameContext _pauseContext;
        [SerializeField] private GameContext _settingsContext;
        [SerializeField] private GameContext _mainTitleContext;
        [SerializeField] private GameContext _saveSlotsContext;

        private ISceneService _sceneService;
        private IGameContextService _gameContext;

        private void Awake() {
            _sceneService = Services.Instance.GetService<ISceneService>();
            _gameContext = Services.Instance.GetService<IGameContextService>();
        }

        private void OnEnable() {
            EventDispatcher.OnPauseInput  += ToggleGamePause;
            EventDispatcher.OnEscapeInput += GoToMainTitleFromSettings;
        }

        private void OnDisable() {
            EventDispatcher.OnPauseInput  -= ToggleGamePause;
            EventDispatcher.OnEscapeInput -= GoToMainTitleFromSettings;
        }

        private GameContext _previousPauseContext;
        public void ToggleGamePause() {
            if (_gameContext.Data.GamePaused)
                _gameContext.UpdateGameContext(_pauseContext, _previousPauseContext);
            else {
                _previousPauseContext = _gameContext.Data.CurrentContext;
                _gameContext.UpdateGameContext(_previousPauseContext, _pauseContext);
            }
        }



                                // Button functions //
        private GameContext _previousSettingsContext;
        public void GoToSettingsMenu() {
            _previousSettingsContext = _gameContext.Data.CurrentContext;
            _gameContext.UpdateGameContext(_previousSettingsContext, _settingsContext);
        }

        public void GoToMainTitleFromSettings() {
            if (_gameContext.Data.CurrentContext == _settingsContext)
                _gameContext.UpdateGameContext(_settingsContext, _previousSettingsContext);
        }

        private GameContext _previousBackToTitleContext;
        public void GoToMainTitle() {
            _previousBackToTitleContext = _gameContext.Data.CurrentContext;
            if (SceneManager.GetActiveScene().buildIndex != GameCore.SceneMainTitle) {
                LoadSceneWithLoadingScreen(GameCore.SceneMainTitle);
                return;
            }
            
            _gameContext.UpdateGameContext(_previousBackToTitleContext, _mainTitleContext);
        }

        public void GoToGameScene() {
            LoadSceneWithLoadingScreen(GameCore.SceneGameplay);
        }

        public void RestartScene() {
            if (_gameContext.Data.GamePaused) EventDispatcher.OnPauseInput?.Invoke();
            _sceneService.ReloadLevel();
        }

        public void QuitGame() {
            // TODO - Some effect and/or confirmation before quitting
            Application.Quit();
        }

        public void GoToSaveSlots() {
            _gameContext.UpdateGameContext(_mainTitleContext, _saveSlotsContext);
        }
        

        public void LoadSceneWithSave(int sceneIndex) {
            if (_gameContext.Data.GamePaused) EventDispatcher.OnPauseInput?.Invoke();         // * - Resume game if paused for any reason

            LoadSceneWithLoadingScreen(sceneIndex);
        }

        private void LoadSceneWithLoadingScreen(int sceneIndex) {
            _sceneService.LoadNewScene(sceneIndex);
        }

    }
}