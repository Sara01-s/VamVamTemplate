namespace VVT {
    
    public interface IContextService {

        /// <summary> Called when a new context is set </summary>
        event System.Action<GameContext, GameContext> OnContextChanged;

        /// <summary> Get relevant data from the context. </summary>
        GameContextInfo Info { get; }

        /// <summary> Update the current game context. </summary>
        void UpdateGameContext(GameContext previous, GameContext newContext);
        /// <summary> Update the current game context. </summary>
        void UpdateGameContext(GameContext newContext);

        /// <summary> Toggles the game pause state </summary>
        void ToggleGamePause();

        /// <summary> Closes the application </summary>
        void QuitApplication();


    }
}