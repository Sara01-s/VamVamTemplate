using static Unity.Mathematics.math;
using UnityEngine;

namespace VVT {
    
	/// <summary>
    /// Provides a set of mathematical and geometric functions and constants.
    /// </summary>
    public static class VVTMath {

		/// <summary>
        /// Represents the golden ratio, which is approximately 1.618033988749.
        /// </summary>
        public const float PHI = 1.618033988749F;

		/// <summary>
        /// Calculates the golden ratio.
        /// </summary>
        /// <returns>The golden ratio.</returns>
        public static float GetPHI() {
            return 1.0f + sqrt(5) / 2.0f;
        }

		/// <summary>
        /// Converts a volume from decibels to a linear scale.
        /// </summary>
        /// <param name="volumeInDb">The volume in decibels.</param>
        /// <returns>The volume on a linear scale.</returns>
        public static float DbToLinear(float volumeInDb) {
            return pow(10.0f, volumeInDb / 20.0f);
        }

		/// <summary>
        /// Converts a volume from a linear scale to decibels.
        /// </summary>
        /// <param name="volumeLinear">The volume on a linear scale.</param>
        /// <returns>The volume in decibels.</returns>
        public static float LinearToDb(float volumeLinear) {
            return 20.0f * log10(volumeLinear);
        }

        /// <summary>
        /// Remaps a value from one range to another.
        /// </summary>
        /// <param name="value">The value to remap.</param>
        /// <param name="from1">The lower bound of the original range.</param>
        /// <param name="to1">The upper bound of the original range.</param>
        /// <param name="from2">The lower bound of the new range.</param>
        /// <param name="to2">The upper bound of the new range.</param>
        /// <returns>The remapped value.</returns>
        public static float Remap(float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    
		/// <summary>
        /// Uses Unity's mathematics API to remap a value from one range to another.
        /// </summary>
        /// <param name="value">The value to remap.</param>
        /// <param name="from1">The lower bound of the original range.</param>
        /// <param name="to1">The upper bound of the original range.</param>
        /// <param name="from2">The lower bound of the new range.</param>
        /// <param name="to2">The upper bound of the new range.</param>
        /// <returns>The remapped value.</returns>
        public static float FastRemap(float value, float from1, float to1, float from2, float to2) {
            return remap(value, from1, to1, from2, to2);
        }

        /// <summary>
        /// Returns the direction vector from an angle in degrees.
        /// </summary>
        /// <param name="angleInDegrees">The angle in degrees.</param>
        /// <returns>The direction vector.</returns>
        public static Vector3 DirectionFromAngle(float angleInDegrees) {
            
            float angleRads = angleInDegrees * PI / 180.0f;
            float dirX = cos(angleRads);
            float dirY = sin(angleRads);

            return new Vector3(dirX, dirY);
        }

        /// <summary>
        /// Returns the 2D direction vector from an angle in degrees.
        /// </summary>
        /// <param name="angleInDegrees">The angle in degrees.</param>
        /// <returns>The 2D direction vector.</returns>
        public static Vector2 DirectionFromAngle2D(float angleInDegrees) {
            
            float angleRads = angleInDegrees * PI / 180.0f;
            float dirX = cos(angleRads);
            float dirY = sin(angleRads);

            return new Vector2(dirX, dirY);
        }

		/// <summary>
        /// Returns the local direction vector from an angle in degrees.
        /// </summary>
        /// <param name="transform">The transform of the local space.</param>
        /// <param name="angleInDegrees">The angle in degrees.</param>
        /// <returns>The local direction vector.</returns>
        public static Vector3 DirectionFromAngleLocal(Transform transform, float angleInDegrees) {
            
            angleInDegrees -= transform.eulerAngles.z;

            float angleRads = angleInDegrees * PI / 180.0f;
            float dirX = cos(angleRads);
            float dirY = sin(angleRads);

            return new Vector3(dirX, dirY);
        }

        /// <summary>
        /// Returns a Vector2 from a rotation angle.
        /// </summary>
        /// <param name="angle">The rotation angle.</param>
        /// <returns>The Vector2 from the rotation angle.</returns>
        public static Vector2 VectorFromRotation(float angle) {
            var sinT = Mathf.Sin(angle);
            var cosT = Mathf.Cos(angle);

            var result = new Vector2();

            var rX = (result.x * cosT) - (result.y * sinT);
            var rY = (result.x * sinT) + (result.y * cosT);

            return new Vector2(rX, rY);
        }

    }
}