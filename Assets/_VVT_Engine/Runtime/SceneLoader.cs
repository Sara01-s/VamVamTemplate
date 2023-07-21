using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using VVT.Data;

namespace VVT.Runtime {
    
    internal sealed class SceneLoader : MonoBehaviour, ISceneService {

        [SerializeField] private GameContext _onSceneLoadChangeTo;

        private GameContext _previousContext;
        private ISettingsDataService _settingsDataService;
        private IGameDataService _gameDataService;
        private IGameContextService _gameContext;
        
        private void Awake() {
            Services.Instance.RegisterService<ISceneService>(this);
            
            _gameContext = Services.Instance.GetService<IGameContextService>();
            _gameDataService = Services.Instance.GetService<IGameDataService>();
            _settingsDataService = Services.Instance.GetService<ISettingsDataService>();
        }
        
        private void OnDisable() {
            Services.Instance.UnRegisterService<ISceneService>();
        }

        public void LoadSceneInstantWithSave(int index) {
            _gameDataService.SaveData();
            _settingsDataService.SaveData();

            SceneManager.LoadScene(index);
        }

        public void LoadNewScene(int index) {
            StopAllCoroutines();
            StartCoroutine(CO_LoadScene(index));
        }

        public IEnumerator CO_LoadScene(int index) {

            Time.timeScale = 1;

            _gameDataService.SaveData();
            _settingsDataService.SaveData();
            
            var scene = SceneManager.LoadSceneAsync(index);

            scene.allowSceneActivation = false;
            _previousContext = _gameContext.Data.CurrentContext;
            _gameContext.UpdateGameContext(_previousContext, _onSceneLoadChangeTo);

            yield return new WaitForSeconds(1f);                // ! FIXME - Delete this in an actual game

            while (scene.progress < 0.9f)
                yield return null;                              // Add delay to the scene load while is not ready
                
            scene.allowSceneActivation = true;
            _gameContext.Data.PlayerHasControl = true;

            Logs.SystemLog($"Level Loader : Scene \"{SceneManager.GetSceneByBuildIndex(index).name}\" loaded, make sure the scene has a SceneContextSetter");
        }

        public void ReloadLevel() {
            StartCoroutine(CO_LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
        
    }
}