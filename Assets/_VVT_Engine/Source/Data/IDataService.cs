using System.Collections.Generic;

namespace VVT.Data {
    /// <summary> All Data objects in the game need to implement this interface </summary>
    public interface IData {}
    
    public interface ISettingsDataService {
        void LoadData();
        void CreateNewData();
        void SaveData();
    }

    public interface IGameDataService {

        void LoadData();
        void CreateNewData();
        void SaveData();
        
        void ChangeSelectedProfileID(string newProfileID);
        void DeleteProfileData(string profileID);
        Dictionary<string, GameData> GetAllProfilesData();
    }
}