using System.Collections;

namespace VamVam.Source.Utils {

    public interface ISceneService {
        
        void LoadSceneInstantWithSave(int index);
        void LoadNewScene(int index);
        void ReloadLevel();
        IEnumerator CO_LoadScene(int index);
    }
}