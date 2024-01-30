using static Unity.Mathematics.math;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace VVT {

    public static class AudioSourceExtensions {

 		/// <summary>
        /// Fades in the audio source over time according to the provided curve.
        /// </summary>
        /// <param name="source">The audio source to fade in.</param>
        /// <param name="fadeinCurve">The curve that defines the fade in.</param>
		public static void FadeIn(this AudioSource source, AnimationCurve fadeinCurve) {

			Timing.RunCoroutine(CO_FadeIn());

			IEnumerator<float> CO_FadeIn() {

				if (!source.isPlaying) {
					source.Play();
				}

				float startTime = Time.time;

				while (Time.time - startTime < fadeinCurve.length) {
					float i = (Time.time - startTime) / fadeinCurve.length;
					float t = fadeinCurve.Evaluate(i);

					source.volume = lerp(0.0f, fadeinCurve.Evaluate(fadeinCurve.length), t);

					yield return Timing.WaitForOneFrame;
				}
			}
			
		}
		
		/// <summary>
        /// Fades out the audio source over time according to the provided curve.
        /// </summary>
        /// <param name="source">The audio source to fade out.</param>
        /// <param name="fadeOutCurve">The curve that defines the fade out.</param>
		public static void FadeOut(this AudioSource source, AnimationCurve fadeOutCurve) {

			Timing.RunCoroutine(CO_FadeOut());

			IEnumerator<float> CO_FadeOut() {
				
				float startTime = Time.time;

				while (Time.time - startTime < fadeOutCurve.length) {
					float i = (Time.time - startTime) / fadeOutCurve.length;
					float t = fadeOutCurve.Evaluate(i);

					source.volume = lerp(0.0f, fadeOutCurve.Evaluate(fadeOutCurve.length), t);

					if (source.volume <= 0.01f) {
						source.Stop();
					}

					yield return Timing.WaitForOneFrame;
				}
			}

		}

    }
}
