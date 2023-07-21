using UnityEngine;

namespace VamVam.Source.Data {

    [System.Serializable]
    /// <summary> Serializable class containing all game settings related persistant data. </summary>
    public sealed class SettingsData : IData {
        
        public float AudioMasterVolume;
        public float AudioAmbienceVolume;
        public float AudioMusicVolume;
        public float AudioGameSfxVolume;
        public float AudioUISfxVolume;

        /// <summary> When a new SettingsData is created, the settings values are set to defaults. </summary>
        public SettingsData() {
            AudioMasterVolume   = 1f;
            AudioAmbienceVolume = 0.5f;
            AudioMusicVolume    = 0.5f;
            AudioGameSfxVolume  = 0.5f;
            AudioUISfxVolume    = 0.5f;
        }

        internal void PrintData() {
            Debug.Log("Game Data : AudioMasterVolume: "   + AudioMasterVolume);
            Debug.Log("Game Data : AudioAmbienceVolume: " + AudioAmbienceVolume);
            Debug.Log("Game Data : AudioMusicVolume: "    + AudioMusicVolume);
            Debug.Log("Game Data : AudioGameSfxVolume: "  + AudioGameSfxVolume);
            Debug.Log("Game Data : AudioUISfxVolume: "    + AudioUISfxVolume);
        }

    }
}