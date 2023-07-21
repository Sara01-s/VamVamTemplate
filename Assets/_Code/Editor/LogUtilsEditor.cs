using VamVam.Source.Utils;
using UnityEditor;

namespace VamVam.Editor {
    
    internal sealed class LogsEditor {
        [MenuItem("VamVam/System/Toggle System Logs")]
        public static void ToggleSystemLogs() {
            Logs.SystemLogs = !Logs.SystemLogs;
        }

        [MenuItem("VamVam/System/Toggle Game Logs")]
        public static void ToggleGameLogs() {
            Logs.GameLogs = !Logs.GameLogs;
        }
    }
}