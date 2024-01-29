using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System.Linq;
using System.Text;

namespace VVT.Runtime {

    public sealed partial class AudioController {

        [Space(15), Header("Sound Settings"), Label("Pre-load Clips")]
        [SerializeField] bool _preLoad = false;

        [SerializeField, ShowIf(nameof(_preLoad)), BoxGroup("Sounds Paths"), Label("Music : Resources/")]
        private string _musicPath    = "Audio/Clips/Music/";
        [SerializeField, ShowIf(nameof(_preLoad)), BoxGroup("Sounds Paths"), Label("Ambience : Resources/")] 
        private string _ambiencePath = "Audio/Clips/Ambience/";
        [SerializeField, ShowIf(nameof(_preLoad)), BoxGroup("Sounds Paths"), Label("SFX : Resources/")] 
        private string _sfxPath      = "Audio/SFX/";

        private const string VVT_MUSIC_PATH    = "VVTAudio/Clips/Music/";
        private const string VVT_AMBIENCE_PATH = "VVTAudio/Clips/Ambience/";
        private const string VVT_SFX_PATH      = "VVTAudio/Clips/SFX/";
		private List<AudioClip> _vvtMusicClips = new();
		private List<AudioClip> _vvtAmbienceClips = new();
		private List<AudioClip> _vvtSFXClips = new();

        [SerializeField, HideIf(nameof(_preLoad)), BoxGroup("Sounds references")] 
        private List<AudioClip> _musicClips = new();
        [SerializeField, HideIf(nameof(_preLoad)), BoxGroup("Sounds references")] 
        private List<AudioClip> _ambienceClips = new();
        [SerializeField, HideIf(nameof(_preLoad)), BoxGroup("Sounds references")] 
        private List<AudioClip> _sfxClips = new();

        [Tooltip("This sound will play if a play sound request fails")]
        [SerializeField] private AudioClip _errorSound;

        private readonly Dictionary<string, AudioClip> _registeredSounds = new();
        private readonly List<string> _validPrefixes = new() { "music_", "ambience_", "sfx_" };

        private void OnEnable() {
            if (_preLoad) {
				_vvtMusicClips 	  = Resources.LoadAll<AudioClip>(VVT_MUSIC_PATH).ToList();
				_vvtAmbienceClips = Resources.LoadAll<AudioClip>(VVT_AMBIENCE_PATH).ToList();
				_vvtSFXClips	  = Resources.LoadAll<AudioClip>(VVT_SFX_PATH).ToList();

                _musicClips    = Resources.LoadAll<AudioClip>(_musicPath).ToList();
                _ambienceClips = Resources.LoadAll<AudioClip>(_ambiencePath).ToList();
                _sfxClips      = Resources.LoadAll<AudioClip>(_sfxPath).ToList();
            }

			_vvtMusicClips	  .ForEach(clip => RegisterSound(clip));
			_vvtAmbienceClips .ForEach(clip => RegisterSound(clip));
			_vvtSFXClips	  .ForEach(clip => RegisterSound(clip));

            _musicClips    .ForEach(clip => RegisterSound(clip));
            _ambienceClips .ForEach(clip => RegisterSound(clip));
            _sfxClips      .ForEach(clip => RegisterSound(clip));
        }

        private void RegisterSound(AudioClip sound) {
            var validPrefix = _validPrefixes.Any(prefix => sound.name.StartsWith(prefix));

            if (!validPrefix) {
                var validPrefixes = new StringBuilder();

                _validPrefixes.ForEach(prefix => {
                    var connector = (prefix != _validPrefixes.Last()) ? ", " : ".";
                    validPrefixes.Append($"{Logs.Bold(prefix)}" + connector);
                });

                Debug.LogWarning($"{Prefix} {sound.name} doesn't use any sound's prefix convention. " +
                                 $"valid prefixes are: {validPrefixes}");
                return;
            }

            // Valid sound prefix
            if (!_registeredSounds.TryAdd(sound.name, sound))
                Debug.LogError($"{Prefix} Failed to register sound: {sound.name}");
            else 
                Logs.SystemLog($"{Prefix} Sound {sound.name} registered");
        }

        public AudioClip NameToAudioClip(string soundName) {
			
            if (!_registeredSounds.TryGetValue(soundName, out var sound)) {
                Debug.LogWarning($"{Prefix} Failed to get sound: \"{soundName}\", default error sound played instead");
                return _errorSound == null
					   ? null 
					   : _errorSound;
            }
			
            return sound;
        }

		// Temporal, to give warns if needed
		private void CheckSoundMixerOutput(string soundName, Mixer mixer) {
			if (soundName.StartsWith("_music") && mixer != Mixer.Music)
				Debug.LogWarning($"{Prefix} Sound starting with \'_music\' not using Music mixer, is it correct?");
			if (soundName.StartsWith("_ambience") && mixer != Mixer.Music)
				Debug.LogWarning($"{Prefix} Sound starting with \'_ambience\' not using Music mixer, is it correct?");
			if (soundName.StartsWith("_sfx") && mixer != Mixer.Music)
				Debug.LogWarning($"{Prefix} Sound starting with \'_sfx\' not using Music mixer, is it correct?");
		}

    }
}