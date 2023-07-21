using UnityEngine;
using VVT;

namespace VVT.Data {

    internal sealed class SavedSlotsMenu : MonoBehaviour {

        private SaveSlot[] _saveSlots;
        private IProfileDataService<GameData> _gameDataService;

        private void Awake() {
            _saveSlots = GetComponentsInChildren<SaveSlot>();
            _gameDataService = Services.Instance.GetService<IProfileDataService<GameData>>();
        }
        
        private void OnEnable() => ActivateMenu();


        public void ContinueSlotGame(SaveSlot saveSlot) {
            _gameDataService.ChangeSelectedProfileID(saveSlot.ProfileID);
        }

        public void CreateNewGameOnSlot(SaveSlot saveSlot) {
            _gameDataService.ChangeSelectedProfileID(saveSlot.ProfileID);
            _gameDataService.CreateNewData();
        }

        public void ClearSlotData(SaveSlot saveSlot) {
            _gameDataService.DeleteProfileData(saveSlot.ProfileID);
            ActivateMenu();
        }

        public void ActivateMenu() {
            var profilesGameData = _gameDataService.GetAllProfilesData();

            foreach(var saveSlot in _saveSlots) {
                GameData profileData = null;
                profilesGameData.TryGetValue(saveSlot.ProfileID, out profileData);

                saveSlot.SetData(profileData);
            }
        }

        private void DisableSavedSlotsButtons() {
            foreach(var saveSlot in _saveSlots) {
                if (saveSlot.HasEmptyData())
                    saveSlot.SetInteractable(false);
            }

            // Maybe, back button has to be disable as well, but idk when to active it again...
        }

    }
}
