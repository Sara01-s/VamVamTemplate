using UnityEngine.SceneManagement;
using System.Collections;
using NaughtyAttributes;
using EasyTransition;
using UnityEngine;
using VVT.Data;

namespace VVT.Runtime {
    
    internal sealed class SceneController : MonoBehaviour, ISceneService {

		[field:SerializeField] public bool UseTransitions { get; set; }

		[ShowIf(nameof(UseTransitions))]
		[SerializeField] private TransitionSettings _transition;

		[Space(20.0f)]
        [SerializeField] private Context _loadingContext;

        private ISettingsDataService _settingsDataService;
        private IGameDataService _gameDataService;
        private IContextService _contextService;
        
        private void Awake() {
            Services.Instance.RegisterService<ISceneService>(this);
            
            _gameDataService = Services.Instance.GetService<IGameDataService>();
            _settingsDataService = Services.Instance.GetService<ISettingsDataService>();

			if (UseTransitions) {
				UnityEngine.Assertions.Assert.IsNotNull(_transition, "Please assign a transition in inspector.");
			}
        }

		private void Start() {
            _contextService = Services.Instance.GetService<IContextService>();
		}

        private void OnDisable() {
            Services.Instance.UnRegisterService<ISceneService>();
        }

        public void LoadScene(int sceneBuildIndex) {

			int scenesCount = SceneManager.sceneCountInBuildSettings;
            
			if (!sceneBuildIndex.between(-1, scenesCount)) {
                Logs.LogError($"Scene Controller : Can't load scene with build index {sceneBuildIndex}, There are {scenesCount} scenes in Build Settings.", ErrorCode.BadArgument);
                return;
            }

			if (UseTransitions) {
				TransitionManager.Instance().Transition(sceneBuildIndex, _transition, 0.0f);
				return;
			}

            StopAllCoroutines();
            StartCoroutine(CO_LoadScene(sceneBuildIndex));
        }

		
        public void LoadSceneImmediate(int sceneBuildIndex) {
            SceneManager.LoadScene(sceneBuildIndex);
        }

		public void LoadPreviousSceneImmediate() {
			int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            SceneManager.LoadScene(previousSceneIndex);
        }

		public void LoadNextSceneImmediate() {
			int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }

		public void LoadPreviousScene() {
			int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
			LoadScene(previousSceneIndex);
		}

		public void LoadNextScene() {
			int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
			LoadScene(nextSceneIndex);
		}

        public void ReloadScene() {
            if (_contextService.ContextsInfo.GamePaused) _contextService.ToggleGamePause();
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

		public void ReloadSceneImmediate() {
            if (_contextService.ContextsInfo.GamePaused) _contextService.ToggleGamePause();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

		private IEnumerator CO_LoadScene(int sceneBuildIndex) {
            Time.timeScale = 1.0f;

            _gameDataService.SaveData();
            _settingsDataService.SaveData();
            
            var scene = SceneManager.LoadSceneAsync(sceneBuildIndex);

            scene.allowSceneActivation = false;
            _contextService.UpdateGameContext(_loadingContext);

            yield return new WaitForSeconds(1.0f);                // ! FIXME - Delete this in an actual game

            while (scene.progress < 0.9f)
                yield return null;                              // Add delay to the scene load while is not ready
                
            scene.allowSceneActivation = true;
            Logs.SystemLog($"Scene Controller : Scene \"{SceneManager.GetSceneByBuildIndex(sceneBuildIndex).name}\" loaded, make sure the scene has a SceneContextSetter");
        }
        
    }

}