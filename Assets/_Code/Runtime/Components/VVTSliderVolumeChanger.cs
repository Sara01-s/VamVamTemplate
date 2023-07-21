using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using FMODUnity;

namespace VVT.Runtime {
    
    [RequireComponent(typeof(Slider))]
    internal class VVTSliderVolumeChanger : MonoBehaviour {

        [SerializeField, BankRef] private string _targetBus;
        [SerializeField] private Slider _thisSlider; 

        private UnityAction<float> _changeTargetVolumeAction;
        private IFMODAudioService _audioService;
        private FMOD.Studio.Bus _bus;

        private void Awake() {
            _audioService = Services.Instance.GetService<IFMODAudioService>();
            _changeTargetVolumeAction = new UnityAction<float>(ChangeTargetVolume);
            _bus = RuntimeManager.GetBus(_targetBus);
        }

        private void OnEnable()  {
            _thisSlider.value = _audioService.GetBusVolume(_bus);
            _thisSlider.onValueChanged.AddListener(_changeTargetVolumeAction);
        }
        private void OnDisable() => _thisSlider.onValueChanged.RemoveListener(_changeTargetVolumeAction);

        private void ChangeTargetVolume(float sliderValue) {
            _audioService.ChangeBusVolume(_bus, sliderValue);
        }

    }
}