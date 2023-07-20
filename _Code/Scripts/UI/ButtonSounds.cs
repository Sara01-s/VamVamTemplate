using UnityEngine.EventSystems;
using VamVam.Scripts.Core;
using VamVam.Source.Utils;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace VamVam.Scripts.UI {

    [RequireComponent(typeof(Button))]
    internal sealed class ButtonSounds : MonoBehaviour, IPointerEnterHandler {
        
        private IAudioService _audioService;
        private UnityAction _onClickAction;
        private Button _thisButton;

        private void Awake() => _thisButton = GetComponent<Button>();
        private void Start() => _audioService = ServiceLocator.Instance.GetService<IAudioService>();
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