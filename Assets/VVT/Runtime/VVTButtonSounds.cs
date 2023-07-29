using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace VVT.Runtime {

    [RequireComponent(typeof(Button))]
    internal sealed class VVTButtonSounds : MonoBehaviour, IPointerEnterHandler {
        
        private IAudioService _audioService;
        private UnityAction _onClickAction;
        private Button _thisButton;

        private void Awake() => _thisButton = GetComponent<Button>();
        private void Start() => _audioService = Services.Instance.GetService<IAudioService>();
        private void PlayClickSound() => _audioService.PlaySound("sfx_ding");

        private void OnEnable() {
            _onClickAction += PlayClickSound;
            _thisButton.onClick.AddListener(_onClickAction);
        }

        private void OnDisable() {
            _thisButton.onClick.RemoveListener(_onClickAction);
            _onClickAction -= PlayClickSound;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
            _audioService.PlaySound("UI_Button_Hover_02");
        }

    }
}