using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VVT.Runtime {

    public sealed partial class AudioController {

        [Space(20), Header("Game Sounds references")]
        [SerializeField] private List<AudioClip> _musicClips = new();
        [SerializeField] private List<AudioClip> _ambienceClips = new();
        [SerializeField] private List<AudioClip> _sfxClips = new();

        [Tooltip("This sound will play if a play sound request fails")]
        [SerializeField] private AudioClip _errorSound;

        private readonly Dictionary<string, AudioClip> _registeredSounds = new();
        private readonly List<string> _validPrefixes = new() { "music_", "ambience_", "sfx_" };

        private void Start() {
            _musicClips    .ForEach(clip => RegisterSound(clip));
            _ambienceClips .ForEach(clip => RegisterSound(clip));
            _sfxClips      .ForEach(clip => RegisterSound(clip));
        }

        private void RegisterSound(AudioClip sound) {
            var validPrefix = _validPrefixes.Any(prefix => sound.name.StartsWith(prefix));
            if (!validPrefix)
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