using UnityEngine.SceneManagement;
using UnityEngine;

namespace VVT.Runtime {

    /// <summary> Main UI using Mediator Pattern </summary>
    internal sealed class UIMediator : MonoBehaviour {

        [SerializeField] private GameContext _pauseContext;
        [SerializeField] private GameContext _settingsContext;
        [SerializeField] private GameContext _mainTitleContext;
        [SerializeField] private GameContext _saveSlotsContext;

        private ISceneService _sceneService;
        private IContextService _gameContext;

        private void Awake() {
            _sceneService = Services.Instance.GetService<ISceneService>();
            _gameContext  = Services.Instance.GetService<IContextService>();
        }

                                // Button functions //
        public void GoToPreviousScene() {
            int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            _sceneService.LoadScene(previousSceneIndex);
        }

        public void GoToNextScene() {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            _sceneService.LoadScene(nextSceneIndex);
        }

        public void RestartScene() {
            if (_gameContext.Info.GamePaused) EventDispatcher.OnPauseInput?.Invoke();
            _sceneService.ReloadLevel();
        }

        public void UpdateContextTo(GameContext newContext) {
            _gameContext.UpdateGameContext(newContext);
        }

        public void QuitGame() {
            // TODO - Some effect and/or confirmation before quitting
            _gameContext.QuitApplication();
        }

    }
}