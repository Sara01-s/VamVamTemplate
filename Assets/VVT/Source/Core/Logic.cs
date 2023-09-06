using System.Collections;
using UnityEngine;
using System;

namespace VVT {

    /// <summary> Common utilities </summary>
    public static class Common {

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

        /// <summary> Returns true if the layer is equal to the given LayerMask. </summary>
        public static bool CompareLayerAndMask(int layer, LayerMask layerMask) {
            return (layerMask & 1 << layer) == 1 << layer;
        }

        /// <summary> 
        /// Performs a brief pause in the game using time scale.
        /// Example for doing a one second pause: StartCoroutine(nameof(LogicUtils.CO_ShortPause), 1.0f);
        /// </summary>
        public static IEnumerator CO_ShortPause(float duration) {
            Time.timeScale = 0.0f;
            yield return new WaitForSecondsRealtime(duration);
            Time.timeScale = 1.0f;
        }

    }
}