using static Unity.Mathematics.math;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

namespace VVT {
    
	/// <summary>
    /// Provides a set of mathematical and geometric functions and constants.
    /// Functions are lowercase to provide a sweet shader-like syntax ;).
    /// </summary>
    public static class VVTMath {

        private const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

		/// <summary>
        /// Represents the golden ratio, which is approximately 1.618033988749.
        /// </summary>
        public const float PHI = 1.618033988749F;

        public const float INFINITY = float.PositiveInfinity;
        public const float NEG_INFINITY = float.NegativeInfinity;
        public static readonly float EPSILON = UnityEngineInternal.MathfInternal.IsFlushToZeroEnabled ? UnityEngineInternal.MathfInternal.FloatMinNormal : UnityEngineInternal.MathfInternal.FloatMinDenormal;

		/// <summary>
        /// Calculates the golden ratio on runtime.
        /// </summary>
        /// <returns>The golden ratio.</returns>
        [MethodImpl(INLINE)]
        public static float phi() {
            return 1.0f + sqrt(5) / 2.0f;
        }

		/// <summary>
        /// Converts a volume from decibels to a linear scale.
        /// </summary>
        /// <param name="volumeInDb">The volume in decibels.</param>
        /// <returns>The volume on a linear scale.</returns>
        [MethodImpl(INLINE)]
        public static float dbToLinear(float volumeInDb) {
            return pow(10.0f, volumeInDb / 20.0f);
        }

		/// <summary>
        /// Converts a volume from a linear scale to decibels.
        /// </summary>
        /// <param name="volumeLinear">The volume on a linear scale.</param>
        /// <returns>The volume in decibels.</returns>
        [MethodImpl(INLINE)]
        public static float linearToDb(float volumeLinear) {
            return 20.0f * log10(volumeLinear);
        }

        public static ulong binomialCoef(uint n, uint k) {
            // ref: https://github.com/FreyaHolmer/Mathfs/blob/master/Runtime/Mathfs.cs
            // source: https://blog.plover.com/math/choose.html
			ulong r = 1UL;

			if (k > n) return 0UL;

			for (ulong d = 1; d <= k; d++) {
				r *= n--;
				r /= d;
			}

			return r;
			// mathematically clean but extremely prone to overflow
            // return fact(n) / fact(k) * fact(n-k)
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
        [MethodImpl(INLINE)]
        public static float remap(float value, float from1, float to1, float from2, float to2) {
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
        [MethodImpl(INLINE)]
        public static float fastRemap(float value, float from1, float to1, float from2, float to2) {
            return Unity.Mathematics.math.remap(value, from1, to1, from2, to2);
        }

        /// <summary>
        /// Returns the direction vector from an angle in degrees.
        /// </summary>
        /// <param name="angleInDegrees">The angle in degrees.</param>
        /// <returns>The direction vector.</returns>
        [MethodImpl(INLINE)]
        public static Vector3 angleToDirection(float angleInDegrees) {
            
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
        [MethodImpl(INLINE)]
        public static Vector2 angleToDirection2D(float angleInDegrees) {
            
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
        [MethodImpl(INLINE)]
        public static Vector3 angleToLocalDirection(Transform transform, float angleInDegrees) {
            
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
        [MethodImpl(INLINE)]
        public static Vector2 vectorFromRotation(float angle) {
            var sinT = Mathf.Sin(angle);
            var cosT = Mathf.Cos(angle);

            var result = new Vector2();

            var rX = (result.x * cosT) - (result.y * sinT);
            var rY = (result.x * sinT) + (result.y * cosT);

            return new Vector2(rX, rY);
        }

        [MethodImpl(INLINE)]
        public static bool aprox(float a, float b) {
            return abs(b - a) < max(1E-06f * max(abs(a), abs(b)), Unity.Mathematics.math.EPSILON * 8.0f);
        }

        private static readonly long[] factorialLongLUT = {
			/*0*/  1,
			/*1*/  1,
			/*2*/  2,
			/*3*/  6,
			/*4*/  24,
			/*5*/  120,
			/*6*/  720,
			/*7*/  5040,
			/*8*/  40320,
			/*9*/  362880,
			/*10*/ 3628800,
			/*11*/ 39916800,
			/*12*/ 479001600,
			/*13*/ 6227020800,
			/*14*/ 87178291200,
			/*15*/ 1307674368000,
			/*16*/ 20922789888000,
			/*17*/ 355687428096000,
			/*18*/ 6402373705728000,
			/*19*/ 121645100408832000,
			/*20*/ 2432902008176640000
		};

		private static readonly int[] factorialIntLUT = {
			/*0*/  1,
			/*1*/  1,
			/*2*/  2,
			/*3*/  6,
			/*4*/  24,
			/*5*/  120,
			/*6*/  720,
			/*7*/  5040,
			/*8*/  40320,
			/*9*/  362880,
			/*10*/ 3628800,
			/*11*/ 39916800,
			/*12*/ 479001600
		};

        [MethodImpl( INLINE )]
        public static int fact(uint value) {
			if (value <= 12) {
				return factorialIntLUT[value];
            }

			if (value <= 20) {
				throw new OverflowException($"The Factorial of {value} is too big for integer representation, please use {nameof(factLong)} instead");
            }

			throw new OverflowException( $"The Factorial of {value} is too big for integer representation" );
		}

        [MethodImpl(INLINE)]
        public static long factLong(long value) {
			if (value <= 12L) {
				return factorialLongLUT[value];
            }

			throw new OverflowException( $"The Factorial of {value} is too big for long representation" );
		}

        [MethodImpl(INLINE)]
        public static Vector3 cubicCasteljau(Vector3[] points, float t) {
            if (points.Length != 4) {
                Debug.LogError("You need to specify at least 4 points to calculate casteljau's bezier curve");
                return Vector3.zero;
            }

            return cubicCasteljau(points[0], points[1], points[2], points[3], t);
        }

        [MethodImpl(INLINE)]
        public static Vector3 cubicCasteljau(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
            var A = lerp(p0, p1, t);
            var B = lerp(p1, p2, t);
            var C = lerp(p2, p3, t);

            var D = lerp(A, B, t);
            var E = lerp(B, C, t);

            var P = lerp(D, E, t);

            return P;
        }

        [MethodImpl(INLINE)]
        public static float eerp(float a, float b, float t) {
            return t switch {
                0.0f => a,
                1.0f => b,
                _ => pow(a, 1.0f - t) * pow(b, t),
            };
        }

        [MethodImpl(INLINE)]
        public static float invEerp(float a, float b, float value) {
            return log(a / value) / log (a / b);
        }

        // source: https://github.com/FreyaHolmer/Mathfs/blob/master/Runtime/Mathfs.cs
        /// <summary>Gradually changes a value towards a desired goal over time.
		/// The value is smoothed by some spring-damper like function, which will never overshoot.
		/// The function can be used to smooth any kind of value, positions, colors, scalars</summary>
		/// <param name="current">The current position</param>
		/// <param name="target">The position we are trying to reach</param>
		/// <param name="currentVelocity">The current velocity, this value is modified by the function every time you call it</param>
		/// <param name="smoothTime">Approximately the time it will take to reach the target. A smaller value will reach the target faster</param>
		/// <param name="maxSpeed">	Optionally allows you to clamp the maximum speed</param>
		public static float SmoothDamp( float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed = INFINITY ) {
			float deltaTime = Time.deltaTime;
			return SmoothDamp( current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime );
		}

		/// <summary>Gradually changes a value towards a desired goal over time.
		/// The value is smoothed by some spring-damper like function, which will never overshoot.
		/// The function can be used to smooth any kind of value, positions, colors, scalars</summary>
		/// <param name="current">The current position</param>
		/// <param name="target">The position we are trying to reach</param>
		/// <param name="currentVelocity">The current velocity, this value is modified by the function every time you call it</param>
		/// <param name="smoothTime">Approximately the time it will take to reach the target. A smaller value will reach the target faster</param>
		/// <param name="maxSpeed">	Optionally allows you to clamp the maximum speed</param>
		/// <param name="deltaTime">The time since the last call to this function. By default Time.deltaTime</param>
		public static float SmoothDamp( float current, float target, ref float currentVelocity, float smoothTime, [UnityEngine.Internal.DefaultValue( "Mathf.Infinity" )] float maxSpeed, [UnityEngine.Internal.DefaultValue( "Time.deltaTime" )] float deltaTime ) {
			// Based on Game Programming Gems 4 Chapter 1.10
			smoothTime = max( 0.0001F, smoothTime );
			float omega = 2F / smoothTime;

			float x = omega * deltaTime;
			float exp = 1F / ( 1F + x + 0.48F * x * x + 0.235F * x * x * x );
			float change = current - target;
			float originalTo = target;

			// Clamp maximum speed
			float maxChange = maxSpeed * smoothTime;
			change = clamp( change, -maxChange, maxChange );
			target = current - change;

			float temp = ( currentVelocity + omega * change ) * deltaTime;
			currentVelocity = ( currentVelocity - omega * temp ) * exp;
			float output = target + ( change + temp ) * exp;

			// Prevent overshooting
			if( originalTo - current > 0.0F == output > originalTo ) {
				output = originalTo;
				currentVelocity = ( output - originalTo ) / deltaTime;
			}

			return output;
		}

    }
}