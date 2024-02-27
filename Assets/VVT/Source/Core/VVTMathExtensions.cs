using static Unity.Mathematics.math;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VVT {
    /// <summary>
    /// Functions are lowercase to provide a sweet shader-like syntax ;).
    /// </summary>
    public static class VVTMathExtensions {

        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

        #region float and int
        [MethodImpl(INLINE)]
        public static float sqr(this float x) {
            return pow(x, 2.0f);
        }

        [MethodImpl(INLINE)]
        public static int sqr(this int x) {
            return x * x;
        }

        [MethodImpl(INLINE)]
        public static float cube(this float x) {
            return pow(x, 2.0f);
        }

        [MethodImpl(INLINE)]
        public static float degToRad(this float angleDegrees) {
            return radians(angleDegrees);
        }

        [MethodImpl(INLINE)]
        public static float radToDeg(this float angleRadians) {
            return degrees(angleRadians);
        }

        [MethodImpl(INLINE)]
        public static bool within(this float x, float min, float max) {
            return x >= min && x <= max;
        }

        
        [MethodImpl(INLINE)]
        public static bool within(this int x, int min, int max) {
            return x >= min && x <= max;
        }

        [MethodImpl(INLINE)]
        public static bool between(this float x, float min, float max) {
            return x > min && x < max;
        }
        
        [MethodImpl(INLINE)]
        public static bool between(this int x, int min, int max) {
            return x > min && x < max;
        }

        [MethodImpl(INLINE)]
        public static float atLeast(this float x, float min) {
            return x < min
                   ? min
                   : x;
        }

        [MethodImpl(INLINE)]
        public static int atLeast(this int x, int min) {
            return x < min
                   ? min
                   : x;
        }

        [MethodImpl(INLINE)]
        public static float atMost(this float x, float max) {
            return x > max
                   ? max
                   : x;
        }

        [MethodImpl(INLINE)]
        public static int atMost(this int x, int max) {
            return x > max
                   ? max
                   : x;
        }

        [MethodImpl(INLINE)]
        public static bool isAprox(this float x, float value) {
            return abs(value - x) < max(1E-06f * max(abs(x), abs(value)), VVTMath.EPSILON * 8.0f);
        }
        #endregion

        #region Vectors
        [MethodImpl(INLINE)]
        public static Vector2 rotate(this Vector2 v, float angleRadians) {
            float c = cos(angleRadians);
            float s = sin(angleRadians);
            return new Vector2(c * v.x - s * v.y, s * v.x + c * v.y);
        }

        [MethodImpl(INLINE)]
        public static float angle(this Vector2 v) {
            return atan2(v.y, v.x);
        }

        [MethodImpl(INLINE)]
        public static Vector2 rotate90CW(this Vector2 v) {
            return new Vector2(v.y, -v.x);
        }

        [MethodImpl(INLINE)]
        public static Vector2 rotate90CCW(this Vector2 v) {
            return new Vector2(-v.y, v.x);
        }

        [MethodImpl(INLINE)]
        public static Vector2 rotateAround(this Vector2 v, Vector2 pivot, float angleRadians) {
            return rotate(v - pivot, angleRadians) + pivot;
        }

        [MethodImpl(INLINE)]
        public static Vector2 to(this Vector2 v, Vector2 target) {
            return target - v;
        }

        [MethodImpl(INLINE)]
        public static Vector3 to(this Vector3 v, Vector3 target) {
            return target - v;
        }

        [MethodImpl(INLINE)]
        public static Vector2 dirTo(this Vector2 v, Vector2 target) {
            return (target- v).normalized;
        }

        [MethodImpl(INLINE)]
        public static Vector3 dirTo(this Vector3 v, Vector3 target) {
            return (target - v).normalized;
        }
        #endregion

        #region Swizzling
        [MethodImpl(INLINE)]
        public static Vector2 xy(this Vector2 v) {
            return new Vector2(v.x, v.y);
        }
        
        [MethodImpl(INLINE)]
        public static Vector2 yx(this Vector2 v) {
            return new Vector2(v.y, v.x);
        }

        [MethodImpl(INLINE)]
        public static Vector2 xz(this Vector3 v) {
            return new Vector2(v.x, v.z);
        }

        [MethodImpl(INLINE)]
        public static Vector2 flattenX(this Vector2 v) {
            return new Vector2(0.0f, v.y);
        }

        [MethodImpl(INLINE)]
        public static Vector2 flattenY(this Vector2 v) {
            return new Vector2(v.x, 0.0f);
        }

        [MethodImpl(INLINE)]
        public static Vector3 flattenX(this Vector3 v) {
            return new Vector3(0.0f, v.y, v.z);
        }

        [MethodImpl(INLINE)]
        public static Vector3 flattenY(this Vector3 v) {
            return new Vector3(v.x, 0.0f, v.z);
        }

        [MethodImpl(INLINE)]
        public static Vector3 flattenZ(this Vector3 v) {
            return new Vector3(v.x, v.y, 0.0f);
        }
        #endregion

        #region Colors
        [MethodImpl(INLINE)]
        public static Color withAlpha(this Color c, float a) {
            return new Color(c.r, c.g, c.b, a);
        }

        [MethodImpl(INLINE)]
        public static string toHexString(this Color c) {
            return ColorUtility.ToHtmlStringRGBA(c);
        }
        #endregion

    }
}