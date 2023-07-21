using System.Collections;
using UnityEngine;
using System;

namespace VamVam.Source.Utils {

    /// <summary> Math and Logic common utilities </summary>
    public static class LogicUtils {
        
        /// <summary> Return true if the given value is between the min and max values. </summary>
        public static bool IsBetween(float value, float minValue, float maxValue) {
            if (value > minValue
            &&  value < maxValue) return true;
            else return false;
        }

        /// <summary> Return true if the given value is between the min and max values. </summary>
        public static bool IsBetween(int value, int minValue, int maxValue) {
            if (value > minValue
            &&  value < maxValue) return true;
            else return false;
        }

        /// <summary> Return true if the given value is between the given range. </summary>
        public static bool IsBetween(float value, Range range) {
            if (value > range.End.Value
            &&  value < range.Start.Value) return true;
            else return false;
        }

        /// <summary> Return true if the given value is between the given range. </summary>
        public static bool IsBetween(int value, Range range) {
            if (value > range.End.Value
            &&  value < range.Start.Value) return true;
            else return false;
        }
    

        /// <summary> Returns a Vector2 from a rotation angle. </summary>
        public static Vector2 VectorFromRotation(float angle) {
            var sinT = Mathf.Sin(angle);
            var cosT = Mathf.Cos(angle);

            var result = new Vector2();

            var rX = (result.x * cosT) - (result.y * sinT);
            var rY = (result.x * sinT) + (result.y * cosT);

            return result;
        }

        /// <summary> Returns a remaped version of the given range. </summary>
        public static float Remap(float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        /// <summary> Returns true if the layer is equal to the given LayerMask. </summary>
        public static bool CompareLayerAndMask(int layer, LayerMask layerMask) {
            return (layerMask & 1 << layer) == 1 << layer;
        }

        /// <summary> 
        /// Performs a brief pause in the game using time scale.
        /// Example for doing a one second pause: StartCoroutine(nameof(LogicUtils.CO_ShortPause), 1.0f);
        /// </summary>
        private static IEnumerator CO_ShortPause(float duration) {
            Time.timeScale = 0.0f;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1.0f;
        }

    }
}