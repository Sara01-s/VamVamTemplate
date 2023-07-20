namespace VamVam.Source.Data {
    
    public interface IGameDataPersistant {

        void LoadData(GameData gameData);
        void SaveData(GameData gameData);
        
    }
}