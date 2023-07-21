using System.Collections.Generic;
using VamVam.Source.Utils;
using VamVam.Source.Data;
using UnityEngine;
using System.Linq; 

namespace VamVam.Scripts.Data {

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
            SaveSettingsData();
        }

        private void Awake() {
            ServiceLocator.Instance.RegisterService<ISettingsDataService>(this);

            // Application persistentDataPath is the default data path in a Unity application
            _fileDataHandler = new FileDataHandler<SettingsData> (
                Application.persistentDataPath
                , _dataFileFullName
                , _useEncryption
            );
        }

        private void OnDisable() => ServiceLocator.Instance.UnRegisterService<ISettingsDataService>();

        private void Start() {
            LoadSettingsData();
        } 

        public void CreateNewSettingsData() {
            _settingsData = new SettingsData();
            LogUtils.SystemLog(_prefix + LogUtils.Colorize("New settings created", LogColor.Lime));
        }


        public void LoadSettingsData() {
            _settingsData = _fileDataHandler.LoadFromFile();
            _dataPersistantObjects = Object.FindObjectsOfType<MonoBehaviour>(true).OfType<ISettingsDataPersistant>().ToList();

            if (_settingsData is null) {
                LogUtils.SystemLog(_prefix + "Settings data is null, creating new settings data...");
                CreateNewSettingsData();
            }

            if (_dataPersistantObjects.Count == 0) {
                LogUtils.SystemLog(_prefix + "No settings persistant objects were found in this scene.");
                return;
            }

            foreach (var dataPersistantObj in _dataPersistantObjects)
                if (dataPersistantObj != null)
                    dataPersistantObj.LoadData(_settingsData);

            LogUtils.SystemLog(_prefix + LogUtils.Colorize("Loading settings data...", LogColor.Aqua));
        }

        public void SaveSettingsData() {
            if (_settingsData == null) {
                Debug.LogWarning(_prefix + "No settings data was found, a new Settings Data needs to be created before data can be saved.");
                return;
            }
        
            LogUtils.SystemLog(_prefix + LogUtils.Colorize("Saving settings data...", LogColor.Orange));

            foreach (var dataPersistantObj in _dataPersistantObjects)
                dataPersistantObj.SaveData(_settingsData);

            _fileDataHandler.SaveToFile(_settingsData);
        }

    }
}