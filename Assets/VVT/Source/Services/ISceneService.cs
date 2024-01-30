namespace VVT {

    /// <summary>
    /// Get this service using Service Locator to access VVT Scene Management system!
    /// </summary>
    public interface ISceneService {

		/// <summary>
        /// Set this value to true to activate scene transitions.
        /// </summary>
		bool UseTransitions { get; set; }
        
        /// <summary>
        /// Asynchronously loads a scene by its scene build index.
        /// </summary>
        /// <param name="sceneBuildIndex">The build index of the scene to load.</param>
        void LoadScene(int sceneBuildIndex);

        /// <summary>
        /// Asynchronously loads the next scene based on the current scene's build index.
        /// </summary>
        void LoadNextScene();

        /// <summary>
		/// Asynchronously loads the previous scene based on the current scene's build index.
        /// </summary>
        void LoadPreviousScene();

        /// <summary>
        /// Loads a scene immediately (synchronously) by its scene build index.
        /// </summary>
        /// <param name="sceneBuildIndex">The build index of the scene to load immediately.</param>
        void LoadSceneImmediate(int sceneBuildIndex);

        /// <summary>
		/// Immediately loads the next scene based on the current scene's build index
        /// </summary>
        void LoadNextSceneImmediate();

        /// <summary>
		/// Immediately loads the previous scene based on the current scene's build index
        /// </summary>
        void LoadPreviousSceneImmediate();

        /// <summary>
        /// Reloads the current scene, resetting its state.
        /// </summary>
        void ReloadScene();

        /// <summary>
        /// Reloads the current scene immediately (synchronously), resetting its state.
        /// </summary>
        void ReloadSceneImmediate();

    }
}
