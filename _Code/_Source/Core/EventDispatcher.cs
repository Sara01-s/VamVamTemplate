using System;

namespace VamVam.Source.Core {

    public sealed class EventDispatcher {
        
        // Input Events //
        public static Action OnPauseInput;
        public static Action OnInteractionInput;
        public static Action OnEscapeInput;

        // Game Context //
        public static Action<bool> OnGamePauseToggled;

        // Gameplay //
        public static Action OnGameStart;
        public static Action OnGameOver;
        public static Action OnGameEnd;

    }
}
