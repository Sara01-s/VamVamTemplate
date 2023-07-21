namespace VVT {
    
    public interface IGameContextService {

        /// <summary> Called when a new context is set </summary>
        System.Action<GameContext, GameContext> OnContextChanged { get; set; }

        /// <summary> Get relevant data from the context. </summary>
        GameContextData Data { get; }

        /// <summary> Update the current game context. </summary>
        void UpdateGameContext(GameContext previous, GameContext newContext);
    }
}