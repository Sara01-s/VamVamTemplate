namespace VVT {

    /// <summary>
    /// Get this service using Service Locator to access VVT Audio system!
    /// </summary>
    public interface IAudioService {

        /// <summary>
        /// Parses the exact sound name and plays it through the given mixergroup output.
        /// </summary>
        /// <param name="soundExactName">The exact name of the sound in the project</param>
        /// <param name="mixer">The sound's output channel </param>
        /// <param name="is3D">is the sound 3D/spatialized?</param>
        void PlaySound(string soundExactName, Mixer mixer = Mixer.SFX, bool is3D = false);

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

    }
}