using UnityEngine;

namespace VVT.Data {

    [System.Serializable]
    /// <summary> Serializable class containing all game settings related persistant data. </summary>
    public sealed class SettingsData : IData {
        
        public float AudioMasterVolume;
        public float AudioAmbienceVolume;
        public float AudioMusicVolume;
        public float AudioSFXVolume;

		public float MouseSensitivity;

        /// <summary> When a new SettingsData is created, the settings values are set to defaults. </summary>
        public SettingsData() {
            AudioMasterVolume   = 1f;
            AudioAmbienceVolume = 0.5f;
            AudioMusicVolume    = 0.5f;
            AudioSFXVolume  = 0.5f;
			MouseSensitivity = 2.0f;
        }

        public void PrintData() {
            Debug.Log("Settings Data : AudioMasterVolume: "   + AudioMasterVolume);
            Debug.Log("Settings Data : AudioAmbienceVolume: " + AudioAmbienceVolume);
            Debug.Log("Settings Data : AudioMusicVolume: "    + AudioMusicVolume);
            Debug.Log("Settings Data : AudioSFXVolume: "  	  + AudioSFXVolume);
			Debug.Log("Settings Data : Mouse Sensitivity: "   + MouseSensitivity);
        }

    }
}