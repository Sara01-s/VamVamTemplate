using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

namespace VVT.Runtime {
    
    [RequireComponent(typeof(Slider))]
    internal class VVTSliderVolumeChanger : MonoBehaviour {

        [SerializeField] private Mixer _targetMixer;
        [SerializeField] private Slider _thisSlider; 

        private UnityAction<float> _changeTargetVolumeAction;
        private IAudioService _audioService;

        private void Awake() {
            _audioService = Services.Instance.GetService<IAudioService>();
            _changeTargetVolumeAction = new UnityAction<float>(ChangeTargetVolume);
        }

        private void OnEnable() {
            _thisSlider.value = _audioService.GetMixerVolume(_targetMixer);
            _thisSlider.onValueChanged.AddListener(_changeTargetVolumeAction);
        }

        private void OnDisable() {
            _thisSlider.value = _audioService.GetMixerVolume(_targetMixer);
            _thisSlider.onValueChanged.RemoveListener(_changeTargetVolumeAction);
        }

        private void ChangeTargetVolume(float sliderValue) {
            _audioService.ChangeMixerVolume(_targetMixer, sliderValue);
        }

    }
}