using UnityEngine;

namespace VVT.Runtime {

	internal sealed class VVTSceneLoader : MonoBehaviour {

		private ISceneService _sceneService;

		private void Awake() {
			_sceneService = Services.Instance.GetService<ISceneService>();
		}

        public void LoadScene(int sceneBuildIndex) {
			_sceneService.LoadScene(sceneBuildIndex);
		}

        public void LoadNextScene() {
			_sceneService.LoadNextScene();
		}

        public void LoadPreviousScene() {
			_sceneService.LoadPreviousScene();
		}

        public void LoadSceneImmediate(int sceneBuildIndex) {
			_sceneService.LoadSceneImmediate(sceneBuildIndex);
		}

        public void LoadNextSceneImmediate() {
			_sceneService.LoadNextSceneImmediate();
		}

        public void LoadPreviousSceneImmediate() {
			_sceneService.LoadPreviousSceneImmediate();
		}

        public void ReloadScene() {
			_sceneService.ReloadScene();
		}

        public void ReloadSceneImmediate() {
			_sceneService.ReloadSceneImmediate();
		}

	}
}