using UnityEngine.SceneManagement;
using VamVam.Source.Utils;
using VamVam.Source.Data;
using VamVam.Source.Core;
using System.Collections;
using UnityEngine;

namespace VamVam.Scripts.Core {
    
    internal sealed class SceneLoader : MonoBehaviour, ISceneService {

        private GameContext _previousContext = GameContext.OnMainTitle;
        private IGameDataService _gameDataService;
        private ISettingsDataService _settingsDataService;
        
        private void Awake() {
            ServiceLocator.Instance.RegisterService<ISceneService>(this);
            _gameDataService = ServiceLocator.Instance.GetService<IGameDataService>();
            _settingsDataService = ServiceLocator.Instance.GetService<ISettingsDataService>();
        }
        private void OnDisable() {
            ServiceLocator.Instance.UnRegisterService<ISceneService>();
        }

        public void LoadSceneInstantWithSave(int index) {
            _gameDataService.SaveGameData();
            _settingsDataService.SaveSettingsData();

            SceneManager.LoadScene(index);
        }

        public void LoadNewScene(int index) {
            StopAllCoroutines();
            StartCoroutine(CO_LoadScene(index));
        }

        public IEnumerator CO_LoadScene(int index) {

            Time.timeScale = 1;

            _gameDataService.SaveGameData();
            _settingsDataService.SaveSettingsData();
            
            var scene = SceneManager.LoadSceneAsync(index);

            scene.allowSceneActivation = false;
            _previousContext = GameContextData.Context;
            GameContextUpdater.UpdateGameContext(_previousContext, GameContext.OnLoading);

            yield return new WaitForSeconds(1f);                // ! FIXME - quitar luego XD

            while (scene.progress < 0.9f)
                yield return null;                              // Add delay to the scene load while is not ready
                
            scene.allowSceneActivation = true;
            GameContextData.PlayerHasControl = true;

            Debug.Log($"Level Loader : Scene \"{SceneManager.GetSceneByBuildIndex(index).name}\" loaded, make sure the scene has a SceneContextSetter");
        }

        public void ReloadLevel() {
            StartCoroutine(CO_LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
        
    }
}