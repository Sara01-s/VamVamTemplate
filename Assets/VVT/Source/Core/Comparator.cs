using UnityEngine;
using System;

namespace VVT {

    /// <summary>
    /// Provides a set of comparison utilities.
    /// </summary>
    public static class Comparator {

        /// <summary>
        /// Determines whether a value is between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>True if the value is between the minimum and maximum values, false otherwise.</returns>
        public static bool IsBetween(float value, float minValue, float maxValue) {
            return value > minValue && value < maxValue;
        }

		/// <summary>
        /// Determines whether a value is between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minValue">The minimum value of the range.</param>
        /// <param name="maxValue">The maximum value of the range.</param>
        /// <returns>True if the value is between the minimum and maximum values, false otherwise.</returns>
        public static bool IsBetween(int value, int minValue, int maxValue) {
            return value > minValue && value < maxValue;
        }

        /// <summary>
        /// Determines whether a value is within a specified range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="range">The range to check against.</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public static bool IsBetween(float value, Range range) {
            return value > range.End.Value && value < range.Start.Value;
        }

		/// <summary>
        /// Determines whether a value is within a specified range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="range">The range to check against.</param>
        /// <returns>True if the value is within the range, false otherwise.</returns>
        public static bool IsBetween(int value, Range range) {
            return value > range.End.Value && value < range.Start.Value;
        }

		/// <summary>
        /// Compares a layer to a LayerMask.
        /// </summary>
        /// <param name="layer">The layer to compare.</param>
        /// <param name="layerMask">The LayerMask to compare against.</param>
        /// <returns>True if the layer is equal to the LayerMask, false otherwise.</returns>
        public static bool CompareLayerAndMask(int layer, LayerMask layerMask) {
            return (layerMask & 1 << layer) == 1 << layer;
        }

    }
}