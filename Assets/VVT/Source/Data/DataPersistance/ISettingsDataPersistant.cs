namespace VVT.Data {

    public interface ISettingsDataPersistant {

        void LoadData(SettingsData settingsData);
        void SaveData(SettingsData settingsData);
    }
}