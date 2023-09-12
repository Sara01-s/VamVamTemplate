using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System.Linq;
using VVT.Data;

namespace VVT.Runtime {

    // If you're using this, make sure you have unity audio enabled in Edit -> Project Settings -> Audio
    /// <summary> Provides an Unity Audio system facade </summary>
    public sealed partial class AudioController : VVTMonoSystem, ISettingsDataPersistant, IAudioService {

        protected override string Prefix { get; set; } = "Audio Service : ";
        
        [System.Serializable]
        internal class MixerData {
            [field:SerializeField] internal int BanksAmount     { get; set; }
            [field:SerializeField] internal string BankName     { get; set; }
            [field:SerializeField] internal string ExposedValue { get; set; }
            [field:SerializeField] internal Mixer MixerTarget   { get; set; }
            [field:SerializeField] internal AudioMixerGroup MixerGroup { get; set; }
            internal List<AudioSource> AudioBanks { get; set; } = new();
        }

        [Header("Audio Controller Settings")]
        [SerializeField] private GameObject _bankPrefab;
        [SerializeField] private List<MixerData> _mixersData;
        [SerializeField] private AudioMixer _masterMixer;
        [SerializeField] private string _exposedMasterVolume;

        private readonly Dictionary<Mixer, MixerData> _mixersDict = new();

        private void Awake() {
            Services.Instance.RegisterService<IAudioService>(this);

            foreach (var mixerData in _mixersData) {
                if (!_mixersDict.TryAdd(mixerData.MixerTarget, mixerData)) {
                    Debug.LogError($"{Prefix} Failed to register {mixerData.BankName}");
                    continue;
                }
                else 
                    Logs.SystemLog($"{Prefix} {mixerData.BankName} data registered");
            }

            foreach (var mixerData in _mixersData) {
                GenerateAllAudioBanksFor(mixerData.MixerTarget);
            }

            Logs.SystemLog(Prefix + " Audio Banks Generated");
        }

        private void OnDisable() {
            Services.Instance.UnRegisterService<IAudioService>();
        }

        public void LoadData(SettingsData settingsData) {
            if (!_masterMixer.SetFloat(_exposedMasterVolume, settingsData.AudioMasterVolume)) {
                Debug.LogError($"{Prefix} Failed to load volume for {_exposedMasterVolume}");
            }

            ChangeMixerVolume(Mixer.Music    , VVTMath.DbToLinear(settingsData.AudioMusicVolume));
            ChangeMixerVolume(Mixer.Ambience , VVTMath.DbToLinear(settingsData.AudioAmbienceVolume));
            ChangeMixerVolume(Mixer.SFX      , VVTMath.DbToLinear(settingsData.AudioSFXVolume));
        }

        public void SaveData(SettingsData settingsData) {

            // TODO : Refactor this, that means refactoring how SettingsData works.
            if (_masterMixer.GetFloat(_exposedMasterVolume, out var masterVolume))
                settingsData.AudioMasterVolume = masterVolume;
            else 
                Debug.LogError($"{Prefix} Failed to get {_exposedMasterVolume} volume");

            if (_masterMixer.GetFloat(_mixersDict[Mixer.Music].ExposedValue, out var musicVolume))
                settingsData.AudioMusicVolume = musicVolume;
            else
                Debug.LogError($"{Prefix} Failed to get {_mixersDict[Mixer.Music].ExposedValue} volume");

            if (_masterMixer.GetFloat(_mixersDict[Mixer.Ambience].ExposedValue, out var ambienceVolume))
                settingsData.AudioAmbienceVolume = ambienceVolume;
            else
                Debug.LogError($"{Prefix} Failed to get {_mixersDict[Mixer.Ambience].ExposedValue} volume");

            if (_masterMixer.GetFloat(_mixersDict[Mixer.SFX].ExposedValue, out var sfxVolume))
                settingsData.AudioSFXVolume = sfxVolume;
            else
                Debug.LogError($"{Prefix} Failed to get {_mixersDict[Mixer.SFX].ExposedValue} volume");
        }

        public void ChangeMixerVolume(Mixer mixer, float newVolume) {
            var volumeInDBs = VVTMath.LinearToDb(newVolume);
            
            // If master volume is target
            if (mixer == Mixer.Master) {
                if (_masterMixer.SetFloat(_exposedMasterVolume, volumeInDBs))
                    return;
                else
                    Debug.LogError($"{Prefix} Failed to get {mixer} mixer data.");
            }

            if (!_mixersDict.TryGetValue(mixer, out var mixerData)) {
                Debug.LogError($"{Prefix} Failed to get {mixer} mixer data.");
                return;
            }

            if (!_masterMixer.SetFloat(mixerData.ExposedValue, volumeInDBs)) {
                Debug.LogError($"{Prefix} Failed to set volume for {mixerData.ExposedValue}");
                return;
            }
        }

        public float GetMixerVolume(Mixer mixer) {
            // If master volume is requested
            if (mixer == Mixer.Master) { 
                if (!_masterMixer.GetFloat(_exposedMasterVolume, out var masterVolume)) {
                    Debug.LogError($"{Prefix} Failed to get {mixer} mixer data.");
                    return 0.0f;
                }
                else return VVTMath.DbToLinear(masterVolume);
            }

            // Everything else
            if (!_mixersDict.TryGetValue(mixer, out var mixerData)) {
                Debug.LogError($"{Prefix} Failed to get {mixer} mixer data.");
                return 0.0f;
            }

            if (!_masterMixer.GetFloat(mixerData.ExposedValue, out var volume)) {
                Debug.LogError($"{Prefix} Failed to get from {mixerData.ExposedValue}");
                return 0.0f;
            }
            else return VVTMath.DbToLinear(volume);
        }

        private void GenerateAllAudioBanksFor(Mixer mixer) {
            var mixerData = _mixersDict[mixer];

            for (int i = 0; i < mixerData.BanksAmount; i++) {
                var bankObj   = Instantiate(_bankPrefab, transform);
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
                Debug.LogError(Prefix + " No banks left to play the requested sound");
                return null;
            }

            return candidate;
        }

        // IAudioService implementation //
        public void PlaySound(string soundExactName, Mixer mixer = Mixer.SFX, float volume = 1, float pitch = 1, bool is3D = false, float spatialBlend = 1) {
            PlaySound(ParseSound(soundExactName), mixer, volume, pitch, is3D, spatialBlend);
        }

        public void PlaySound(AudioClip clip, Mixer mixer = Mixer.SFX, float volume = 1, float pitch = 1, bool is3D = false, float spatialBlend = 1) {
            if (clip == null) {
                Logs.LogError(Prefix + "Failed to play sound, audio clip is null");
                return;
            }

            var sources = _mixersDict[mixer].AudioBanks;
            var source = GetAvailableSource(sources);

            source.spatialize = is3D;
            source.spatialBlend = spatialBlend;
            source.pitch = pitch;
            source.volume = volume;
            source.clip = clip;
            source.Play();
        }
    }
}
