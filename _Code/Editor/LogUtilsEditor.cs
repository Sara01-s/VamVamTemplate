using VamVam.Source.Utils;
using UnityEditor;

namespace VamVam.Editor {
    
    internal sealed class LogUtilsEditor {
        [MenuItem("VamVam/System/Toggle System Logs")]
        public static void ToggleSystemLogs() {
            LogUtils.SystemLogs = !LogUtils.SystemLogs;
        }

        [MenuItem("VamVam/System/Toggle Game Logs")]
        public static void ToggleGameLogs() {
            LogUtils.GameLogs = !LogUtils.GameLogs;
        }
    }
}