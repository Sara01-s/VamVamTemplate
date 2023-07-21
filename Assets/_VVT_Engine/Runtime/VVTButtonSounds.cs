using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace VVT.Runtime {

    [RequireComponent(typeof(Button))]
    internal sealed class VVTButtonSounds : MonoBehaviour, IPointerEnterHandler {
        
        private IFMODAudioService _audioService;
        private UnityAction _onClickAction;
        private Button _thisButton;

        private void Awake() => _thisButton = GetComponent<Button>();
        private void Start() => _audioService = Services.Instance.GetService<IFMODAudioService>();
        private void PlayClickSound() => _audioService.PlaySfx(_audioService.GetSound("UI_Button_Hover_01"));

        private void OnEnable() {
            _onClickAction += PlayClickSound;
            _thisButton.onClick.AddListener(_onClickAction);
        }

        private void OnDisable() {
            _thisButton.onClick.RemoveListener(_onClickAction);
            _onClickAction -= PlayClickSound;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
            _audioService.PlaySfx(_audioService.GetSound("UI_Button_Hover_02"));
        }

    }
}