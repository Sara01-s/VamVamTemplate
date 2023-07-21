using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using VVT;

namespace VVT.Data {

    public sealed class GameDataPersistance : MonoBehaviour, IProfileDataService<GameData> {

        [Header("File Storage Configuration")]
        [SerializeField] private string _dataFileFullName = "data.vvg";

        private string _selectedProfileID = "";

        [Header("Debugging")]
        [SerializeField] private bool _useEncryption = false;
        [SerializeField] private bool _initializeDataIfNull = false;

        private GameData _gameData;
        private FileDataHandler<GameData> _fileDataHandler;
        private List<IGameDataPersistant> _dataPersistantObjects = new List<IGameDataPersistant>();
        private const string PREFIX = "Game Data : ";


        private void Awake() {
            Services.Instance.RegisterService<IProfileDataService<GameData>>(this);

            // Application persistentDataPath is the default data path in a Unity application
            _fileDataHandler = new FileDataHandler<GameData> (
                Application.persistentDataPath
                , _dataFileFullName
                , _useEncryption
            );
        }

        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        private void OnDisable() {
            Services.Instance.UnRegisterService<IProfileDataService<GameData>>();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        } 

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            LoadData();
        }



        public void CreateNewData() {
            _gameData = new GameData();
            Logs.SystemLog(PREFIX + Logs.Colorize("New game created", LogColor.Lime));
        }

        public void LoadData() {
            _gameData = _fileDataHandler.LoadFromFile(_selectedProfileID);
            _dataPersistantObjects = Object.FindObjectsOfType<MonoBehaviour>(true).OfType<IGameDataPersistant>().ToList();

            // Debugging purposes only
            if (_gameData == null && _initializeDataIfNull) {
                Logs.SystemLogWarning(PREFIX + "Data created for development/debug");
                CreateNewData();
            }

            if (_gameData == null) {
                Logs.SystemLog(PREFIX + "No data was found, a New Game needs to be created. Maybe use Debug mode?");
                return;
            }

            if (_dataPersistantObjects.Count == 0) {
                Logs.SystemLog(PREFIX + "No data persistant objects were found in this scene.");
                return;
            }

            foreach (var dataPersistantObj in _dataPersistantObjects) {
                if (dataPersistantObj != null)
                    dataPersistantObj.LoadData(_gameData);
            }

            Logs.SystemLog(PREFIX + Logs.Colorize($"Loading game data from profile: {_selectedProfileID}", LogColor.Aqua));
        }

        private void OnApplicationQuit() => SaveData();
        public void SaveData() {
            if (_gameData is null) {
                Logs.SystemLogWarning(PREFIX + "No data was found, a New Game needs to be started before data can be saved.");
                return;
            }
        

            foreach (var dataObj in _dataPersistantObjects) {
                if (!dataObj.Equals(null)) {                // Using Object.Equals because null check for destroyed objects doesn't work :/
                    dataObj.SaveData(_gameData);
                }
                else Logs.SystemLogWarning(PREFIX + "One or more data objects are null");
            }

            _fileDataHandler.SaveToFile(_gameData, _selectedProfileID);


            Logs.SystemLog(PREFIX + Logs.Colorize($"Saving game data for profile: {_selectedProfileID}", LogColor.Orange));
        }

        public void DeleteProfileData(string profileID) {
            _fileDataHandler.DeleteData(profileID);
            LoadData();
        }



        public void ChangeSelectedProfileID(string newProfileID) {
            _selectedProfileID = newProfileID;
        }

        public Dictionary<string, GameData> GetAllProfilesData() {
            return _fileDataHandler.LoadAllProfiles();
        }

    }
}