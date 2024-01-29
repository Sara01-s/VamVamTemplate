using System.Collections;
using UnityEngine;
using System;

namespace VVT {

    /// <summary> Comparations utilities </summary>
    public static class Comparator {

        /// <summary> Return true if the given value is between the min and max values. </summary>
        public static bool IsBetween(float value, float minValue, float maxValue) {
            return value > minValue && value < maxValue;
        }

        /// <summary> Return true if the given value is between the min and max values. </summary>
        public static bool IsBetween(int value, int minValue, int maxValue) {
            return value > minValue && value < maxValue;
        }

        /// <summary> Return true if the given value is between the given range. </summary>
        public static bool IsBetween(float value, Range range) {
            return value > range.End.Value && value < range.Start.Value;
        }

        /// <summary> Return true if the given value is between the given range. </summary>
        public static bool IsBetween(int value, Range range) {
            return value > range.End.Value && value < range.Start.Value;
        }

        /// <summary> Returns true if the layer is equal to the given LayerMask. </summary>
        public static bool CompareLayerAndMask(int layer, LayerMask layerMask) {
            return (layerMask & 1 << layer) == 1 << layer;
        }

    }
}