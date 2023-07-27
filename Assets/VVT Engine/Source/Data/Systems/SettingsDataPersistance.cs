using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VVT;

namespace VVT.Data {

    public sealed class SettingsDataPersistance : MonoBehaviour, ISettingsDataService {

        [Header("File Storage Configuration")]
        [SerializeField] private string _dataFileFullName = "settings.vvg";
        
        [Header("Debugging")]
        [SerializeField] private bool _useEncryption = false;

        private SettingsData _settingsData;
        private List<ISettingsDataPersistant> _dataPersistantObjects = new List<ISettingsDataPersistant>();
        private FileDataHandler<SettingsData> _fileDataHandler;
        private readonly string _prefix = "Settings Data : ";

        private void OnApplicationQuit() {
            SaveData();
        }

        private void Awake() {
            Services.Instance.RegisterService<ISettingsDataService>(this);

            // Application persistentDataPath is the default data path in a Unity application
            _fileDataHandler = new FileDataHandler<SettingsData> (
                Application.persistentDataPath
                , _dataFileFullName
                , _useEncryption
            );
        }

        private void OnDisable() => Services.Instance.UnRegisterService<ISettingsDataService>();

        private void Start() {
            LoadData();
        } 

        public void CreateNewData() {
            _settingsData = new SettingsData();
            Logs.SystemLog(_prefix + Logs.Colorize("New settings created", LogColor.Lime));
        }


        public void LoadData() {
            _settingsData = _fileDataHandler.LoadFromFile();
            _dataPersistantObjects = Object.FindObjectsOfType<MonoBehaviour>(true).OfType<ISettingsDataPersistant>().ToList();

            if (_settingsData is null) {
                Logs.SystemLog(_prefix + "Settings data is null, creating new settings data...");
                CreateNewData();
            }

            if (_dataPersistantObjects.Count == 0) {
                Logs.SystemLog(_prefix + "No settings persistant objects were found in this scene.");
                return;
            }

            foreach (var dataPersistantObj in _dataPersistantObjects)
                if (dataPersistantObj != null)
                    dataPersistantObj.LoadData(_settingsData);

            Logs.SystemLog(_prefix + Logs.Colorize("Loading settings data...", LogColor.Aqua));
        }

        public void SaveData() {
            if (_settingsData == null) {
                Debug.LogWarning(_prefix + "No settings data was found, a new Settings Data needs to be created before data can be saved.");
                return;
            }
        
            Logs.SystemLog(_prefix + Logs.Colorize("Saving settings data...", LogColor.Orange));

            foreach (var dataPersistantObj in _dataPersistantObjects)
                dataPersistantObj.SaveData(_settingsData);

            _fileDataHandler.SaveToFile(_settingsData);
        }

    }
}