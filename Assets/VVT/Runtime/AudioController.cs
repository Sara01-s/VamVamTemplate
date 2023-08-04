using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using VVT.Data;
using static Unity.Mathematics.math;

namespace VVT.Runtime {

    // If you're using this, make sure you have unity audio enabled in Edit -> Project Settings -> Audio
    /// <summary> Provides an Unity Audio system facade </summary>
    public sealed partial class AudioController : MonoBehaviour, ISettingsDataPersistant, IAudioService
        
        
        
        
        
        
        {

        private const float MIN_VOLUME_VALUE = 0.00001F;
        private const string PREFIX = "Audio System :";

        [System.Serializable]
        internal class MixerData {
            [field:SerializeField] internal int BanksAmount { get; set; }
            [field:SerializeField] internal string BankName { get; set; }
            [field:SerializeField] internal string ExposedValue { get; set; }
            [field:SerializeField] internal Mixer MixerTarget { get; set; }
            [field:SerializeField] internal List<AudioSource> AudioBanks { get; set; }
            internal AudioMixerGroup MixerGroup { get; set; }
        }

        [Header("Audio Controller Settings")]
        [SerializeField] private GameObject _bankPrefab;
        [SerializeField] private List<MixerData> _mixersData;
        [SerializeField] private AudioMixer _masterMixer;
        [SerializeField] private string _exposedMasterVolume;

        private Dictionary<Mixer, MixerData> _mixersDict = new();

        private void Awake() {
            Services.Instance.RegisterService<IAudioService>(this);

            foreach (var mixerData in _mixersData) {
                if (!_mixersDict.TryAdd(mixerData.MixerTarget, mixerData)) {
                    Debug.LogError($"{PREFIX} Failed to register {mixerData.BankName}");
                    continue;
                }
                else
                    Logs.SystemLog($"{PREFIX} {mixerData.BankName} data registered");
            }

            foreach (var mixerData in _mixersData) {
                GenerateAllAudioBanksFor(mixerData.MixerTarget);
            }

            Logs.SystemLog(PREFIX + " Audio Banks Generated");
        }

        private void OnDisable() {
            Services.Instance.UnRegisterService<IAudioService>();
        }

        public void LoadData(SettingsData settingsData) {
            if (!_masterMixer.SetFloat(_exposedMasterVolume, settingsData.AudioMasterVolume)) {
                Debug.LogError($"{PREFIX} Failed to load volume for {_exposedMasterVolume}");
            }

            ChangeMixerVolume(Mixer.Music, settingsData.AudioMusicVolume);
            ChangeMixerVolume(Mixer.Ambience, settingsData.AudioAmbienceVolume);
            ChangeMixerVolume(Mixer.SFX, settingsData.AudioSFXVolume);
        }

        public void SaveData(SettingsData settingsData) {

            // TODO : Refactor this, that means refactoring how SettingsData works.
            if (_masterMixer.GetFloat(_exposedMasterVolume, out var masterVolume))
                settingsData.AudioMasterVolume = masterVolume;
            else
                Debug.LogError($"{PREFIX} Failed to get {_exposedMasterVolume} volume");

            if (_masterMixer.GetFloat(_mixersDict[Mixer.Music].ExposedValue, out var musicVolume))
                settingsData.AudioMusicVolume = musicVolume;
            else
                Debug.LogError($"{PREFIX} Failed to get {_mixersDict[Mixer.Music].ExposedValue} volume");

            if (_masterMixer.GetFloat(_mixersDict[Mixer.Ambience].ExposedValue, out var ambienceVolume))
                settingsData.AudioAmbienceVolume = ambienceVolume;
            else
                Debug.LogError($"{PREFIX} Failed to get {_mixersDict[Mixer.Ambience].ExposedValue} volume");

            if (_masterMixer.GetFloat(_mixersDict[Mixer.SFX].ExposedValue, out var sfxVolume))
                settingsData.AudioSFXVolume = sfxVolume;
            else
                Debug.LogError($"{PREFIX} Failed to get {_mixersDict[Mixer.SFX].ExposedValue} volume");
        }

        public void ChangeMixerVolume(Mixer mixer, float newVolume) {
            var volumeInDBs = clamp(newVolume, MIN_VOLUME_VALUE, 20 * log10(1 / newVolume));

            // If master volume is target
            if (mixer == Mixer.Master) {
                if (_masterMixer.SetFloat(_exposedMasterVolume, volumeInDBs))
                    return;
                else
                    Debug.LogError($"{PREFIX} Failed to get {mixer} mixer data.");
            }

            if (!_mixersDict.TryGetValue(mixer, out var mixerData)) {
                Debug.LogError($"{PREFIX} Failed to get {mixer} mixer data.");
                return;
            }

            if (!_masterMixer.SetFloat(mixerData.ExposedValue, volumeInDBs)) {
                Debug.LogError($"{PREFIX} Failed to set volume for {mixerData.ExposedValue}");
            }
        }

        public float GetMixerVolume(Mixer mixer) {
            // If master volume is requested
            if (mixer == Mixer.Master) {
                if (!_masterMixer.GetFloat(_exposedMasterVolume, out var masterVolume)) {
                    Debug.LogError($"{PREFIX} Failed to get {mixer} mixer data.");
                    return 0.0f;
                }
                else
                    return masterVolume;
            }

            // Everything else
            if (!_mixersDict.TryGetValue(mixer, out var mixerData)) {
                Debug.LogError($"{PREFIX} Failed to get {mixer} mixer data.");
                return 0.0f;
            }

            if (!_masterMixer.GetFloat(mixerData.ExposedValue, out var volume)) {
                Debug.LogError($"{PREFIX} Failed to get from {mixerData.ExposedValue}");
                return 0.0f;
            }
            else
                return volume;
        }

        private void GenerateAllAudioBanksFor(Mixer mixer) {
            var mixerData = _mixersDict[mixer];

            for (int i = 0; i < mixerData.BanksAmount; i++) {
                var bankObj = Instantiate(_bankPrefab, transform);
                var bankAudio = bankObj.GetComponent<AudioSource>();

                bankAudio.outputAudioMixerGroup = mixerData.MixerGroup;
                bankAudio.Stop(); // For some reason, initialized audio sources have isPlaying set to true by default. :/
                mixerData.AudioBanks.Add(bankAudio);
                bankObj.name = mixerData.BankName;
            }
        }

        private AudioSource GetAvailableSource(List<AudioSource> sources) {
            var candidate = sources.FirstOrDefault(source => !source.isPlaying && source.clip == null);

            if (candidate == null) {
                candidate = sources.FirstOrDefault(source => !source.isPlaying);

                if (candidate != null)
                    candidate.clip = null;
            }

            if (candidate == null) {
                Debug.LogError(PREFIX + " No banks left to play the requested sound");
                return null;
            }

            return candidate;
        }

        // IAudioService implementation //
        public void PlaySound(string sound, Mixer mixer = Mixer.SFX, bool is3D = false) {
            var sources = _mixersDict[mixer].AudioBanks;
            var source = GetAvailableSource(sources);

            source.clip = ParseSound(sound);
            source.spatialize = is3D;
            source.Play();
        }

    }
}