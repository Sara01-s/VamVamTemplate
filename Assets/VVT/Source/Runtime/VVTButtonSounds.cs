using UnityEngine.EventSystems;
using UnityEngine.Events;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine;

namespace VVT.Runtime {

    [RequireComponent(typeof(Button))]
    internal sealed class VVTButtonSounds : MonoBehaviour, IPointerEnterHandler {
        
		[SerializeField] private bool _useSoundName;

		[ShowIf(nameof(_useSoundName))]
		[SerializeField] private string _clickSoundName;
		[ShowIf(nameof(_useSoundName))]
		[SerializeField] private string _hoverSoundName;

		[HideIf(nameof(_useSoundName))]
		[SerializeField] private AudioClip _clickSound;
		[HideIf(nameof(_useSoundName))]
		[SerializeField] private AudioClip _hoverSound;

        private IAudioService _audioService;
        private UnityAction _onClick;
        private Button _button;

        private void Awake() => _button = GetComponent<Button>();
        private void Start() => _audioService = Services.Instance.GetService<IAudioService>();

        private void OnEnable() {
            _onClick += PlayClickSound;
            _button.onClick.AddListener(_onClick);
        }

        private void OnDisable() {
            _button.onClick.RemoveListener(_onClick);
            _onClick -= PlayClickSound;
        }

        private void PlayClickSound() {
			var clickSound = _useSoundName 
						     ? _audioService.NameToAudioClip(_clickSoundName)
						     : _clickSound;

			_audioService.PlaySound(clickSound);
		}

        public void OnPointerEnter(PointerEventData eventData) {
			var hoverSound = _useSoundName 
						     ? _audioService.NameToAudioClip(_hoverSoundName)
						     : _hoverSound;

            _audioService.PlaySound(hoverSound);
        }

    }
}