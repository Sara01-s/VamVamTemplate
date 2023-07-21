using System.Collections.Generic;
using VamVam.Source.Utils;
using VamVam.Source.Data;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

namespace VamVam.Scripts.Core {
    
    /// <summary> Provides a FMOD audio system facade </summary>
    public sealed partial class FMODAudioController : MonoBehaviour, ISettingsDataPersistant, IAudioService {

        private const float MIN_VOLUME_VALUE = 0.00001F;
        private const string PREFIX = "FMOD Audio : ";

        [Range(MIN_VOLUME_VALUE, 1f)] private float MasterVolume   = 0.5f;
        [Range(MIN_VOLUME_VALUE, 1f)] private float MusicVolume    = 0.5f;
        [Range(MIN_VOLUME_VALUE, 1f)] private float AmbienceVolume = 0.5f;
        [Range(MIN_VOLUME_VALUE, 1f)] private float GameSfxVolume  = 0.5f;
        [Range(MIN_VOLUME_VALUE, 1f)] private float UISfxVolume    = 0.5f;

        private Bus MasterBus, MusicBus, AmbienceBus, GameSfxBus, UISfxBus;

#if UNITY_WEBGL || !UNITY_EDITOR
        [FMODUnity.BankRef]
        [SerializeField] List<string> _fmodBanks = new List<string>();
#endif

        private void Awake() {
            ServiceLocator.Instance.RegisterService<IAudioService>(this);

#if UNITY_WEBGL || !UNITY_EDITOR
            foreach(var bank in _fmodBanks) {
                FMODUnity.RuntimeManager.LoadBank(bank, true);
            }
#else // UNITY_WEBGL || UNITY_EDITOR
            MasterBus   = RuntimeManager.GetBus("bus:/");                   // bus:/ is the Master bus
            MusicBus    = RuntimeManager.GetBus("bus:/Music");
            AmbienceBus = RuntimeManager.GetBus("bus:/Ambience");
            GameSfxBus  = RuntimeManager.GetBus("bus:/GameSFX");
            UISfxBus    = RuntimeManager.GetBus("bus:/UISFX");
#endif
        }

        private void OnDisable() {
            ServiceLocator.Instance.UnRegisterService<IAudioService>();
        }

        public void LoadData(SettingsData settingsData) {
            ChangeBusVolume(MasterBus   , settingsData.AudioMasterVolume);
            ChangeBusVolume(MusicBus    , settingsData.AudioMusicVolume);
            ChangeBusVolume(AmbienceBus , settingsData.AudioAmbienceVolume);
            ChangeBusVolume(GameSfxBus  , settingsData.AudioGameSfxVolume);
            ChangeBusVolume(UISfxBus    , settingsData.AudioUISfxVolume);
        }

        public void SaveData(SettingsData settingsData) {
            settingsData.AudioMasterVolume   = MasterVolume;
            settingsData.AudioMusicVolume    = MusicVolume;
            settingsData.AudioAmbienceVolume = AmbienceVolume;
            settingsData.AudioGameSfxVolume  = GameSfxVolume;
            settingsData.AudioUISfxVolume    = UISfxVolume;
        }

        public void ChangeBusVolume(Bus bus, float newVolume) {
            bus.setVolume(newVolume);
        }

        public float GetBusVolume(Bus bus) {
            var result = bus.getVolume(out var volume);

            if (result == FMOD.RESULT.ERR_DSP_NOTFOUND) {
                LogUtils.SystemLogWarning($"Failed to get {bus.ToString()} volume. BUS NOT FOUND");
                return 0.0f;
            }
            else return volume;
        }

                                            // FMOD AUDIO FACADE //
        private EventInstance _music;
        private EventInstance _ambience;
        private EventInstance _sfx;
        
        public void PlaySfx(EventReference sound) {
            _sfx = RuntimeManager.CreateInstance(sound);
            _sfx.start();
            _sfx.release();
        }

        public void PlaySfx3D(EventReference sound, Vector3 sourcePos) {
            RuntimeManager.PlayOneShot(sound, sourcePos);
        }


        public void PlayAmbience(EventReference ambience) {
            _ambience = RuntimeManager.CreateInstance(ambience);
            _ambience.start();
            _ambience.release();
        }

        public void PlayAmbience3D(EventReference ambience, Vector3 sourcePos) {
            RuntimeManager.PlayOneShot(ambience, sourcePos);
        }


        public void PlayMusic(EventReference music) {
            _music = RuntimeManager.CreateInstance(music);

            _music.start();
            _music.release();
        }

        public void PlayMusic3D(EventReference music, Vector3 sourcePos) {
            RuntimeManager.PlayOneShot(music, sourcePos);
        }


        public void StopMusic    () => _music    .stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        public void StopAmbience () => _ambience .stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        public void StopSfx      () => _sfx      .stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        public void StopMasterChannel   () => MasterBus   .stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        public void StopMusicChannel    () => MusicBus    .stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        public void StopAmbienceChannel () => AmbienceBus .stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        public void StopGameSfxChannel  () => GameSfxBus  .stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        public void StopUISfxChannel    () => UISfxBus    .stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

    }
}