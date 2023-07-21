using System.Collections.Generic;

namespace VVT.Data {
    /// <summary> All Data objects in the game need to implement this interface </summary>
    public interface IData {}
    
    public interface IDataService<T> where T : IData {
        void LoadData();
        void CreateNewData();
        void SaveData();
    }

    public interface IProfileDataService<T> : IDataService<T> where T : IData {

        void ChangeSelectedProfileID(string newProfileID);
        void DeleteProfileData(string profileID);
        Dictionary<string, T> GetAllProfilesData();
    }
}