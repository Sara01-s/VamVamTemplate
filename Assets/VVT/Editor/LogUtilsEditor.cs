using UnityEngine;
using UnityEditor;

namespace VVT.Editor {
    
    internal sealed class LogsEditor {
        [MenuItem("VamVam/System/Toggle System Logs")]
        public static void ToggleSystemLogs() {
            Logs.SystemLogs = !Logs.SystemLogs;

			var status = Logs.SystemLogs ? "enabled" : "disabled";
			Debug.Log($"Logs : System Logs {status}");
        }

        [MenuItem("VamVam/System/Toggle Game Logs")]
        public static void ToggleGameLogs() {
            Logs.GameLogs = !Logs.GameLogs;

			var status = Logs.SystemLogs ? "enabled" : "disabled";
			Debug.Log($"Logs : Game Logs {status}");
        }
    }
}