using VamVam.Source.Data;
using UnityEngine.UI;
using UnityEngine;

namespace VamVam.Scripts.Data {

    internal sealed class SaveSlot : MonoBehaviour {
        
        [Header("Profile")]
        [SerializeField] private string _profileID = "";
        public string ProfileName = "";

        [Header("Content")]
        [SerializeField] private Button _buttonEmpty;
        [SerializeField] private Button _buttonContinue;
        [SerializeField] private Button _clearButton;

        internal string ProfileID { get => _profileID; private set => value = _profileID; }

        internal void SetInteractable(bool interactable) {
            _buttonEmpty.interactable = interactable;
        }

        internal bool HasEmptyData() {
            return _buttonEmpty.IsActive();             // FIXME - find a better way to do this lol
        }

        public void SetData(GameData data) {
            _buttonContinue.gameObject.SetActive(data == null);

            _buttonContinue.gameObject.SetActive(data != null);
            _clearButton.gameObject.SetActive(data != null);
        }

    }
}