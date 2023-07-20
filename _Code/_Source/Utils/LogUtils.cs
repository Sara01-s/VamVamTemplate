using System.Collections.Generic;
using System;

namespace VamVam.Source.Utils {

    /// <summary> Color reference : https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html#:~:text=of%20the%20texture.-,Supported%20colors,-The%20following%20table </summary>
    public enum LogColor {
        Aqua, Black, Blue, Brown, Cyan, DarkBlue, Fuchsia, Green, Grey, LightBLue, Lime, 
        Magenta, Maroon, Navy, Olive, Orange, Purple, Red, Silver, Teal, White, Yellow,
    }


    /// <summary> Unity's Debug class facade with extra methods </summary>
    public static class LogUtils {

        private static Dictionary<LogColor, string> ColorsMap = new Dictionary<LogColor, string> {
            { LogColor.Aqua,      "aqua"      },
            { LogColor.Black,     "black"     },
            { LogColor.Blue,      "blue"      },
            { LogColor.Brown,     "brown"     },
            { LogColor.Cyan,      "cyan"      },
            { LogColor.DarkBlue,  "darkBlue"  },
            { LogColor.Fuchsia,   "fuchsia"   },
            { LogColor.Green,     "green"     },
            { LogColor.Grey,      "grey"      },
            { LogColor.LightBLue, "lightblue" },
            { LogColor.Lime,      "lime"      },
            { LogColor.Magenta,   "magenta"   },
            { LogColor.Maroon,    "maroon"    },
            { LogColor.Navy,      "navy"      },
            { LogColor.Olive,     "olive"     },
            { LogColor.Orange,    "orange"    },
            { LogColor.Purple,    "purple"    },
            { LogColor.Red,       "red"       },
            { LogColor.Silver,    "silver"    },
            { LogColor.Teal,      "teal"      },
            { LogColor.White,     "white"     },
            { LogColor.Yellow,    "yellow"    },
        };

        public static bool SystemLogs;
        public static bool GameLogs = true;

        public static void Log(object message) {
            if (!GameLogs) return;
            UnityEngine.Debug.Log(message);
        }

        public static void Log(object message, bool conditioner) {
            if (!conditioner) return;
            UnityEngine.Debug.Log(message);
        }

        public static void Print(object message) {
            if (!GameLogs) return;
            UnityEngine.Debug.Log(message);
        }

        public static void Print(object message, bool conditioner) {
            if (!conditioner) return;
            UnityEngine.Debug.Log(message);
        }

        public static void LogWarning(object message) {
            if (!GameLogs) return;
            UnityEngine.Debug.LogWarning(message);
        }
        
        public static void LogError(object message) {
            if (!GameLogs) return;
            UnityEngine.Debug.LogError(message);
        }

        public static void SystemLog(object message) {
            if (!SystemLogs) return;
            UnityEngine.Debug.Log(message);
        }

        public static void SystemLogWarning(object message) {
            if (!SystemLogs) return;
            UnityEngine.Debug.LogWarning(message);
        }
        
        public static void SystemLogError(object message) {
            if (!SystemLogs) return;
            UnityEngine.Debug.LogError(message);
        }

        public static string ErrorMessage(object message, Exception e) {
            return $"{message.ToString()} \n {e.Message} \n {e.StackTrace} \n {e}";
        }

        public static string Colorize(object message, LogColor logColor) {
            return $"<color={logColor}>{message.ToString()}</color>";
        }

        public static string Colorize(object message, string hexColor) {
            return $"<color={hexColor}>{message.ToString()}</color>";
        }

        public static string Bold(object message) {
            return $"<b>{message.ToString()}</b>";
        }

        public static string Italic(string message) {
            return $"<i>{message.ToString()}</i>";
        }

        public static string Size(string message, int size) {
            return $"<size={size}>{message.ToString()}</size>";
        }
    }
}