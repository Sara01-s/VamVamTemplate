using UnityEngine.SceneManagement;
using UnityEditor.SceneTemplate;
using UnityEngine;

namespace VVT {
        
    internal class MainTitleScene : ISceneTemplatePipeline {

        public virtual bool IsValidTemplateForInstantiation(SceneTemplateAsset sceneTemplateAsset) {
            return true;
        }

        public virtual void BeforeTemplateInstantiation(SceneTemplateAsset sceneTemplateAsset, bool isAdditive, string sceneName) {
            
        }

        public virtual void AfterTemplateInstantiation(SceneTemplateAsset sceneTemplateAsset, Scene scene, bool isAdditive, string sceneName) {
            Debug.Log("hey");
        }

    }
}
