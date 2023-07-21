using System.Text.RegularExpressions;
using System.Collections.Generic;
using VamVam.Source.Utils;
using UnityEngine;
using FMODUnity;

namespace VamVam.Scripts.Core {

    public sealed partial class FMODAudioController {
        
        [SerializeField] private EventReference[] _fmodSounds;

        private Dictionary<string, EventReference> _registeredSounds = new Dictionary<string, EventReference>();

        private void OnEnable() {
            foreach (var sound in _fmodSounds) {
                if (!_registeredSounds.TryAdd(GetSoundName(sound.Path), sound)) 
                    Debug.LogError("Failed to load sound: " + sound.Path);
                else 
                    LogUtils.SystemLog(PREFIX + "Sound " + GetSoundName(sound.Path) + " registered");
            }
        }

        private string GetSoundName(string fmodPath) {
            // Example of fmod path: "event:/GameSfx/sfx_button_click_01"
            // This function returns everything after the second '/'
            // In that case, the return value would be "sfx_button_click_01"
            // that string will be used to get the sound you want to play.
            var regex = new Regex(@"(?:[^/]+/){2}(.*)");
            var match = regex.Match(fmodPath);

            if (match.Success) 
                return match.Groups[1].Value;

            Debug.LogError("Failed to parse sound name from path: " + fmodPath);
            return fmodPath;
        }

        public EventReference GetSound(string soundName) {
            if (!_registeredSounds.TryGetValue(soundName, out var sound)) {
                Debug.LogError($"Sound {soundName} not found");
                return default(EventReference); // TODO - Maybe change to nullable type (EventReference?)
            }

            return sound;
        }

    }
}