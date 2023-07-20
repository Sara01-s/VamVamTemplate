using UnityEngine;

namespace VamVam.Source.Data {

    [System.Serializable]
    /// <summary> Serializable class containing all gameplay related persistent data. </summary>
    public sealed class GameData : IData {
        
        public string ProfileID;
        public Vector3 PlayerPosition;
        
        // When a new GameData is created, the game values are set to defaults.
        public GameData() {

        }

        internal void PrintData() {
            Debug.Log("Data Debug : " + PlayerPosition);
        }

    }
}