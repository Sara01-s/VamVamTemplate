namespace VamVam.Source.Core {

    public sealed class GameContextData {

        public static GameContext Context { get; set; }
        public static GameContext PreviousContext { get; set; }

        public static bool CanToggleGamePause { get; set; } = false;
        public static bool PlayerHasControl { get; set; } = false;
        public static bool GamePaused { get; set; } = false;
        
    }
}