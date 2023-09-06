using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using VVT.Data;

namespace VVT.Runtime {
    
    internal sealed class SceneLoader : MonoBehaviour, ISceneService {

        [SerializeField] private GameContext _loadingContext;

        private GameContext _previousContext;
        private ISettingsDataService _settingsDataService;
        private IGameDataService _gameDataService;
        private IContextService _gameContext;
        
        private void Awake() {
            Services.Instance.RegisterService<ISceneService>(this);
            
            _gameContext = Services.Instance.GetService<IContextService>();
            _gameDataService = Services.Instance.GetService<IGameDataService>();
            _settingsDataService = Services.Instance.GetService<ISettingsDataService>();
        }
        
        private void OnDisable() {
            Services.Instance.UnRegisterService<ISceneService>();
        }

        public void LoadSceneImmediate(int index) {
            SceneManager.LoadScene(index);
        }

        public void LoadScene(int index) {
            
            if (index < SceneManager.sceneCount || index > SceneManager.sceneCount) {
                var sceneName = SceneManager.GetSceneByBuildIndex(index).name;
                Logs.LogError($"Can't load scene {sceneName} (with build index {index}), There are {SceneManager.sceneCount} scenes in Build Settings");
                return;
            }

            StopAllCoroutines();
            StartCoroutine(CO_LoadScene(index));
        }

        private IEnumerator CO_LoadScene(int index) {

            Time.timeScale = 1;

            _gameDataService.SaveData();
            _settingsDataService.SaveData();
            
            var scene = SceneManager.LoadSceneAsync(index);

            scene.allowSceneActivation = false;
            _previousContext = _gameContext.Info.CurrentContext;
            _gameContext.UpdateGameContext(_previousContext, _loadingContext);

            yield return new WaitForSeconds(1f);                // ! FIXME - Delete this in an actual game

            while (scene.progress < 0.9f)
                yield return null;                              // Add delay to the scene load while is not ready
                
            scene.allowSceneActivation = true;

            Logs.SystemLog($"Level Loader : Scene \"{SceneManager.GetSceneByBuildIndex(index).name}\" loaded, make sure the scene has a SceneContextSetter");
        }

        public void ReloadLevel() {
            StartCoroutine(CO_LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
        
    }
}