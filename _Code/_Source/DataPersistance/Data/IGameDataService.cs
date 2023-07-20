using System.Collections.Generic;

namespace VamVam.Source.Data {
    
    public interface IGameDataService {
        void LoadGameData();
        void CreateNewGameData();
        void SaveGameData();
        void ChangeSelectedProfileID(string newProfileID);
        void DeleteProfileData(string profileID);
        Dictionary<string, GameData> GetAllProfilesData();
    }
}