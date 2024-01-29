using UnityEngine;

namespace VVT {

    /// <summary>
    /// Get this service using Service Locator to access VVT Audio system!
    /// </summary>
    public interface IAudioService {
		
		float MinVolume { get; }
		float MaxVolume { get; }

        /// <summary>
        /// Parses the exact sound name and plays it through the given mixergroup output.
        /// </summary>
        /// <param name="soundFileName">The exact name of the sound in the project</param>
        /// <param name="mixer">The sound's output channel</param>
        /// <param name="loop">does the sound loop?</param>
        /// <param name="spatialBlend">set the 3D intensity of the sound, 0 = full 2D, 1 = full 3D</param>
        /// <param name="volume">overrides output volumek, 0 = silence, 1 = max volume</param>
        /// <param name="pitch">overrides output pitch/tone, -3 = lowest pitch, 0 = NO SOUND, 3 = highest pitch </param>
		/// <param name="priority">overrides output priority, 0 = highest priority, 255 = lowest priority </param>
		/// <param name="fadeIn">Indicates the easing curve used to play the sound at start</param>
		/// <returns> Sound's audio source</returns>
        AudioSource PlaySound(string soundFileName, Mixer mixer = Mixer.SFX, float volume = 1.0f, float pitch = 1.0f
                     , bool loop = false, float spatialBlend = 0.0f, byte priority = 128);

        /// <summary>
        /// Parses the exact sound name and plays it through the given mixergroup output.
        /// </summary>
        /// <param name="clip">Audio clip asset to play</param>
        /// <param name="mixer">The sound's output channel</param>
        /// <param name="loop">does the sound loop?</param>
        /// <param name="spatialBlend">set the 3D intensity of the sound, 0 = full 2D, 1 = full 3D</param>
        /// <param name="volume">overrides output volumek, 0 = silence, 1 = max volume</param>
        /// <param name="pitch">overrides output pitch/tone, -3 = lowest pitch, 3 = highest pitch </param>
		/// <param name="priority">overrides output priority, 0 = highest priority, 255 = lowest priority </param>
		/// <param name="fadeIn">Indicates the easing curve used to play the sound at start</param>
		/// <returns> Sound's audio source</returns>
        AudioSource PlaySound(AudioClip clip, Mixer mixer = Mixer.SFX, float volume = 1.0f, float pitch = 1.0f
                     , bool loop = false, float spatialBlend = 0.0f, byte priority = 128);

        /// <summary>
        /// Changes the current volume of a mixer group output.
        /// </summary>
        /// <param name="mixer">The mixer you want to change volume</param>
        /// <param name="newVolume">The new mixer's volume</param>
        void ChangeMixerVolume(Mixer mixer, float newVolume);

        /// <summary>
        /// Returns current volume of selected mixer
        /// </summary>
        /// <param name="mixer">Retrieved mixer's volume</param>
        /// <returns> Selected mixer's volume</returns>
        float GetMixerVolume(Mixer mixer);

		/// <summary>
        /// Returns audio clip file from sound name
        /// </summary>
        /// <param name="soundName">Resources audio file name
        /// <returns> Audio clip assosiated with sound's name</returns>
		AudioClip NameToAudioClip(string soundName);

    }
}