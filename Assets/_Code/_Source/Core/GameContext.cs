namespace VamVam.Source.Core {
    
    /// <summary> All possible contexts where the player could be while playing the game </summary>
    public enum GameContext {
        /// <summary> Player is on the main title screen                        </summary>
        OnMainTitle,
        /// <summary> Player is doing the tutorial                              </summary>
        OnGameTutorial,
        /// <summary> Player is setting up options in the game                  </summary>
        OnSettings,
        /// <summary> Player is selecting a save slot                           </summary>
        OnSaveSlots,

        /// <summary> Player is playing the game                                </summary>
        OnPlayingGame,
        /// <summary> The game is paused                                        </summary>
        OnGamePaused,
        /// <summary> Player died one time                                      </summary>
        OnGameDeath,    
        /// <summary> Player failed the game                                    </summary>
        OnGameOver, 
        /// <summary> Player ended the game                                     </summary>
        OnGameEnd,
        
        /// <summary> A scene is loading                                        </summary>
        OnLoading,
        /// <summary> You're testing the game!                                  </summary>
        OnTestingGame
    }
}
