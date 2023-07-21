using System.Collections;

namespace VVT {

    public interface ISceneService {
        
        void LoadSceneInstantWithSave(int index);
        void LoadNewScene(int index);
        void ReloadLevel();
        IEnumerator CO_LoadScene(int index);
    }
}