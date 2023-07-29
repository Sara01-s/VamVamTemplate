using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using System.Linq;
using System.Text;

namespace VVT.Runtime {

    public sealed partial class AudioController {

        [Space(15)]
        [Header("Sound Settings")]
        [Label("Pre-load Clips")]
        [SerializeField] bool _preLoad = false;

        [SerializeField, ShowIf(nameof(_preLoad)), BoxGroup("Sounds Paths"), Label("Music : Resources/")]
        private string _musicPath    = "Audio/Clips/Music/";
        [SerializeField, ShowIf(nameof(_preLoad)), BoxGroup("Sounds Paths"), Label("Ambience : Resources/")] 
        private string _ambiencePath = "Audio/Clips/Ambience/";
        [SerializeField, ShowIf(nameof(_preLoad)), BoxGroup("Sounds Paths"), Label("SFX : Resources/")] 
        private string _sfxPath      = "Audio/Clips/SFX/";

        [SerializeField, HideIf(nameof(_preLoad)), BoxGroup("Sounds references")] 
        private List<AudioClip> _musicClips     = new();
        [SerializeField, HideIf(nameof(_preLoad)), BoxGroup("Sounds references")] 
        private List<AudioClip> _ambienceClips  = new();
        [SerializeField, HideIf(nameof(_preLoad)), BoxGroup("Sounds references")] 
        private List<AudioClip> _sfxClips       = new();

        [Tooltip("This sound will play if a play sound request fails")]
        [SerializeField] private AudioClip _errorSound;

        private readonly Dictionary<string, AudioClip> _registeredSounds = new();
        private readonly List<string> _validPrefixes = new() { "music_", "ambience_", "sfx_" };

        private void Start() {
            if (_preLoad) {
                _musicClips    = Resources.LoadAll<AudioClip>(_musicPath).ToList();
                _ambienceClips = Resources.LoadAll<AudioClip>(_ambiencePath).ToList();
                _sfxClips      = Resources.LoadAll<AudioClip>(_sfxPath).ToList();
            }

            _musicClips    .ForEach(clip => RegisterSound(clip));
            _ambienceClips .ForEach(clip => RegisterSound(clip));
            _sfxClips      .ForEach(clip => RegisterSound(clip));
        }

        private void RegisterSound(AudioClip sound) {
            var validPrefix = _validPrefixes.Any(prefix => sound.name.StartsWith(prefix));

            if (!validPrefix) {
                var validPrefixes = new StringBuilder();

                _validPrefixes.ForEach(pfx => {
                    var connector = (pfx != _validPrefixes.Last()) ? ", " : ".";
                    validPrefixes.Append($"{Logs.Bold(pfx)}" + connector);
                });

                Debug.LogWarning($"{PREFIX} {sound.name} doesn't use any sound's prefix convention. " +
                                 $"valid prefixes are: {validPrefixes}");
                return;
            }

            // Valid sound prefix
            if (!_registeredSounds.TryAdd(sound.name, sound))
                Debug.LogError(PREFIX + " Failed to register sound: " + sound.name);
            else 
                Logs.SystemLog(PREFIX + " Sound " + sound.name + " registered");
        }

        private AudioClip ParseSound(string soundName) {
            if (!_registeredSounds.TryGetValue(soundName, out var sound)) {
                Debug.LogWarning($"{PREFIX} Failed to get sound: \"{soundName}\", default error sound played instead");
                return _errorSound == null ? null : _errorSound;
            }
            else return sound;
        }

    }
}