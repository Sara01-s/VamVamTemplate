using static Unity.Mathematics.math;
using UnityEngine;

namespace VVT {

    public static class VVTMath {

        public const float PHI = 1.618033988749F;

        public static float GetPHI() {
            return 1.0f + sqrt(5) / 2.0f;
        }

        /// <summary> Returns a remaped version of the given range. </summary>
        public static float Remap(float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    
        /// <summary> Uses Unity's mathematics API to get a remaped version of the given range. </summary>
        public static float FastRemap(float value, float from1, float to1, float from2, float to2) {
            return remap(value, from1, to1, from2, to2);
        }

        // Returns the direction which angleInDebrees points
        public static Vector3 DirectionFromAngle(float angleInDegrees) {
            
            float angleRads = angleInDegrees * PI / 180.0f;
            float dirX = cos(angleRads);
            float dirY = sin(angleRads);

            return new Vector3(dirX, dirY);
        }

        // Returns the direction which angleInDebrees points
        public static Vector2 DirectionFromAngle2D(float angleInDegrees) {
            
            float angleRads = angleInDegrees * PI / 180.0f;
            float dirX = cos(angleRads);
            float dirY = sin(angleRads);

            return new Vector2(dirX, dirY);
        }

        // Returns local direction which angleInDebrees points
        public static Vector3 DirectionFromAngleLocal(Transform transform, float angleInDegrees) {
            
            angleInDegrees -= transform.eulerAngles.z;

            float angleRads = angleInDegrees * PI / 180.0f;
            float dirX = cos(angleRads);
            float dirY = sin(angleRads);

            return new Vector3(dirX, dirY);
        }

        /// <summary> Returns a Vector2 from a rotation angle. </summary>
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