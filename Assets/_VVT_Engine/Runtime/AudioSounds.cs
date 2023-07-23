using System.Collections.Generic;
using UnityEngine;

namespace VVT.Runtime {

    public sealed partial class AudioController {

        [Space(20), Header("Game Sounds references")]
        [SerializeField] private List<AudioClip> _musicClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> _ambienceClips = new List<AudioClip>();
        [SerializeField] private List<AudioClip> _sfxClips = new List<AudioClip>();

        [Tooltip("This sound will play if a play sound request fails")]
        [SerializeField] private AudioClip _errorSound;

        private readonly Dictionary<string, AudioClip> _registeredSounds = new Dictionary<string, AudioClip>();
        private readonly HashSet<string> _validPrefixes = new HashSet<string>() { 
            "music_", "ambience_", "sfx_"
        };

        private void Start() {
            _musicClips    .ForEach(clip => RegisterSound(clip));
            _ambienceClips .ForEach(clip => RegisterSound(clip));
            _sfxClips      .ForEach(clip => RegisterSound(clip));
        }

        private void RegisterSound(AudioClip sound) {
            if (!_validPrefixes.Contains(sound.name))
                Debug.LogWarning($"{PREFIX} {sound.name} doesn't use any sound's prefix convention.");

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