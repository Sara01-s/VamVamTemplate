using System.Collections.Generic;
using System;

namespace VVT {

    /// <summary> Color reference : https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html#:~:text=of%20the%20texture.-,Supported%20colors,-The%20following%20table </summary>
    public enum LogColor {
        Aqua, Black, Blue, Brown, Cyan, DarkBlue, Fuchsia, Green, Grey, LightBLue, Lime, 
        Magenta, Maroon, Navy, Olive, Orange, Purple, Red, Silver, Teal, White, Yellow,
    }


    /// <summary> Unity's Debug class facade with extra utilities </summary>
    public static class Logs {

        private static Dictionary<LogColor, string> ColorsMap = new() {
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

        // TODO - ? Maybe, to make this variables persistant between assembly recompilation, use: https://docs.unity3d.com/ScriptReference/Compilation.CompilationPipeline-compilationStarted.html
        public static bool SystemLogs;
        public static bool GameLogs = true;

        public static void Log(object message) {
            if (!GameLogs) return;
            UnityEngine.Debug.Log(message);
        }

        public static void Log(object message, LogColor textColor = LogColor.Grey, bool bold = false, bool italic = false, int size = 1) {
            if (!GameLogs) return;

            string msg = message.ToString();
            string result;

            result = bold ? Bold(msg) : msg;
            result = italic ? Italic(result) : result;
            result = size != 1 ? Size(result, size) : result; 

            UnityEngine.Debug.Log(Colorize(result.ToString(), textColor));
        }

        public static void Log(object message, bool condition) {
            if (!GameLogs)  return;
            if (!condition) return;
            UnityEngine.Debug.Log(message);
        }

        public static void Log(object message, bool condition, LogColor textColor = LogColor.Grey, bool bold = false, bool italic = false, int size = 1) {
            if (!GameLogs)  return;
            if (!condition) return;

            string msg = message.ToString();
            string result;

            result = bold ? Bold(msg) : msg;
            result = italic ? Italic(result) : result;
            result = size == 1 ? Size(result, size) : result; 

            UnityEngine.Debug.Log(Colorize(result, textColor));
        }

        public static void Print(object message) {
            if (!GameLogs) return;
            Log(message);
        }

        public static void Print(object message, bool condition) {
            if (!condition) return;
            Log(message, condition);
        }

        public static void LogWarning(object message) {
            if (!GameLogs) return;
            UnityEngine.Debug.LogWarning(message);
        }
        
        public static void LogError(object message, ErrorCode errorCode = ErrorCode.GenericError) {
            if (!GameLogs) return;
            UnityEngine.Debug.LogError($"{message}, Error code: [{(int) errorCode}: {errorCode}]");
        }


        // System Logs
        public static void SystemLog(object message) {
            if (!SystemLogs) return;
            Log(message);
        }

        public static void SystemLog(object message, LogColor textColor = LogColor.Grey, bool bold = false, bool italic = false, int size = 1) {
            if (!SystemLogs) return;

            string msg = message.ToString();
            string result;

            result = bold ? Bold(msg) : msg;
            result = italic ? Italic(result) : result;
            result = size == 1 ? Size(result, size) : result; 

            Log(Colorize(result, textColor));
        }


        public static void SystemLogWarning(object message) {
            if (!SystemLogs) return;
            UnityEngine.Debug.LogWarning(message);
        }
        
        public static void SystemLogError(object message, ErrorCode errorCode = ErrorCode.GenericError) {
            if (!SystemLogs) return;
            UnityEngine.Debug.LogError($"{message}, Error code: [{(int)errorCode}: {errorCode}]");
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