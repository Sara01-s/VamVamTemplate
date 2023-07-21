using UnityEngine.SceneManagement;
using System.Collections.Generic;
using VamVam.Source.Utils;
using VamVam.Source.Data;
using UnityEngine;
using System.Linq;

namespace VamVam.Scripts.Data {

    public sealed class GameDataPersistance : MonoBehaviour, IGameDataService {

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
            ServiceLocator.Instance.RegisterService<IGameDataService>(this);

            // Application persistentDataPath is the default data path in a Unity application
            _fileDataHandler = new FileDataHandler<GameData> (
                Application.persistentDataPath
                , _dataFileFullName
                , _useEncryption
            );
        }

        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        private void OnDisable() {
            ServiceLocator.Instance.UnRegisterService<IGameDataService>();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        } 

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            LoadGameData();
        }



        public void CreateNewGameData() {
            _gameData = new GameData();
            LogUtils.SystemLog(PREFIX + LogUtils.Colorize("New game created", LogColor.Lime));
        }

        public void LoadGameData() {
            _gameData = _fileDataHandler.LoadFromFile(_selectedProfileID);
            _dataPersistantObjects = Object.FindObjectsOfType<MonoBehaviour>(true).OfType<IGameDataPersistant>().ToList();

            // Debugging purposes only
            if (_gameData == null && _initializeDataIfNull) {
                LogUtils.SystemLogWarning(PREFIX + "Data created for development/debug");
                CreateNewGameData();
            }

            if (_gameData == null) {
                LogUtils.SystemLog(PREFIX + "No data was found, a New Game needs to be created. Maybe use Debug mode?");
                return;
            }

            if (_dataPersistantObjects.Count == 0) {
                LogUtils.SystemLog(PREFIX + "No data persistant objects were found in this scene.");
                return;
            }

            foreach (var dataPersistantObj in _dataPersistantObjects) {
                if (dataPersistantObj != null)
                    dataPersistantObj.LoadData(_gameData);
            }

            LogUtils.SystemLog(PREFIX + LogUtils.Colorize($"Loading game data from profile: {_selectedProfileID}", LogColor.Aqua));
        }

        private void OnApplicationQuit() => SaveGameData();
        public void SaveGameData() {
            if (_gameData is null) {
                LogUtils.SystemLogWarning(PREFIX + "No data was found, a New Game needs to be started before data can be saved.");
                return;
            }
        

            foreach (var dataObj in _dataPersistantObjects) {
                if (!dataObj.Equals(null)) {                // Using Object.Equals because null check for destroyed objects doesn't work :/
                    dataObj.SaveData(_gameData);
                }
                else LogUtils.SystemLogWarning(PREFIX + "One or more data objects are null");
            }

            _fileDataHandler.SaveToFile(_gameData, _selectedProfileID);


            LogUtils.SystemLog(PREFIX + LogUtils.Colorize($"Saving game data for profile: {_selectedProfileID}", LogColor.Orange));
        }

        public void DeleteProfileData(string profileID) {
            _fileDataHandler.DeleteData(profileID);
            LoadGameData();
        }



        public void ChangeSelectedProfileID(string newProfileID) {
            _selectedProfileID = newProfileID;
        }

        public Dictionary<string, GameData> GetAllProfilesData() {
            return _fileDataHandler.LoadAllProfiles();
        }

    }
}