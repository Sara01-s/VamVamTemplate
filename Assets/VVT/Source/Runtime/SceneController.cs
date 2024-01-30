using UnityEngine.SceneManagement;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using VVT.Data;

namespace VVT.Runtime {
    
    internal sealed class SceneController : MonoBehaviour, ISceneService {

		[field:SerializeField] public bool UseTransitions { get; set; }

		[ShowIf(nameof(UseTransitions))]
		[SerializeField] private UnityEngine.UI.Image _transitionImage;
		[ShowIf(nameof(UseTransitions))]
		[SerializeField] private AnimationCurve _fadeInCurve;
		[ShowIf(nameof(UseTransitions))]
		[SerializeField] private float _fadeInDuration;
		[ShowIf(nameof(UseTransitions))]
		[SerializeField] private float _fadeOutDuration;
		[ShowIf(nameof(UseTransitions))]
		[SerializeField] private AnimationCurve _fadeOutCurve;

		[Space(20.0f)]
        [SerializeField] private Context _loadingContext;

        private ISettingsDataService _settingsDataService;
		private SceneTransitioner _sceneTransitioner;
        private IGameDataService _gameDataService;
        private IContextService _contextService;
        
        private void Awake() {
            Services.Instance.RegisterService<ISceneService>(this);
            
            _gameDataService = Services.Instance.GetService<IGameDataService>();
            _settingsDataService = Services.Instance.GetService<ISettingsDataService>();

			if (UseTransitions) {
				_sceneTransitioner = new(_transitionImage, _fadeInDuration, _fadeOutDuration, _fadeInCurve, _fadeOutCurve);
			}
        }

		private void Start() {
            _contextService = Services.Instance.GetService<IContextService>();
		}

        private void OnDisable() {
            Services.Instance.UnRegisterService<ISceneService>();
        }

        public void LoadScene(int sceneBuildIndex) {

			int scenes = SceneManager.sceneCountInBuildSettings;
            
            if (!Comparator.IsBetween(sceneBuildIndex, -1, scenes)) {
                Logs.LogError($"Scene Controller : Can't load scene with build index {sceneBuildIndex}, There are {scenes} scenes in Build Settings.", ErrorCode.BadArgument);
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

			if (UseTransitions) {
				StartCoroutine(_sceneTransitioner.CO_FadeOut());
				yield return new WaitUntil(() => _sceneTransitioner.IsTransitionDone);
			}

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

			if (UseTransitions) {
				StartCoroutine(_sceneTransitioner.CO_FadeIn());
				yield return new WaitUntil(() => _sceneTransitioner.IsTransitionDone);
			}

            Logs.SystemLog($"Scene Controller : Scene \"{SceneManager.GetSceneByBuildIndex(sceneBuildIndex).name}\" loaded, make sure the scene has a SceneContextSetter");
        }
        
    }

}