namespace VVT {
    
    public interface IContextService {

        /// <summary> 
        /// Called when a new context is set.
        /// First context is the previous context.
        /// Second context is the new context.
        /// </summary>
        event System.Action<Context, Context> OnContextChanged;

        event System.Action<bool> OnPause;

        /// <summary> Get relevant data from the context. </summary>
        ContextInfo ContextsInfo { get; }

        /// <summary> Updates the current context. </summary>
        void UpdateGameContext(Context newContext);

        /// <summary> Toggles the game pause state </summary>
        void ToggleGamePause();

        /// <summary> Closes the application </summary>
        void QuitApplication();


    }
}