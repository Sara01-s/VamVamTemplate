using UnityEngine.SceneManagement;
using UnityEngine;

namespace VVT.Runtime {

    /// <summary> Main UI using Mediator Pattern </summary>
    internal sealed class UIMediator : MonoBehaviour {

        [SerializeField] private Context _pauseContext;
        [SerializeField] private Context _settingsContext;
        [SerializeField] private Context _mainTitleContext;
        [SerializeField] private Context _saveSlotsContext;

        private ISceneService _sceneService;
        private IContextService _contextService;

        private void Awake() {
            _sceneService = Services.Instance.GetService<ISceneService>();
            _contextService  = Services.Instance.GetService<IContextService>();
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
            _sceneService.ReloadScene();
        }

        public void GoToContext(Context newContext) {
            _contextService.UpdateGameContext(newContext);
        }

        public void GoToPreviousContext() {
            _contextService.UpdateGameContext(_contextService.ContextsInfo.PreviousContext);
        }

        public void QuitGame() {
            // TODO - Some effect and/or confirmation before quitting
            _contextService.QuitApplication();
        }

    }
}