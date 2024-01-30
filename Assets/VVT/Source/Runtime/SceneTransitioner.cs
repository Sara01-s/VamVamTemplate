using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace VVT.Runtime {

	internal sealed class SceneTransitioner {

		internal bool IsTransitionDone { get; private set; }

		private readonly Image _image;

		private readonly int _transitionProgress = Shader.PropertyToID("_Progress");
		private readonly int _isTransitionInverted = Shader.PropertyToID("_Invert");

		private readonly AnimationCurve _fadeInCurve;
		private readonly AnimationCurve _fadeOutCurve;
		private readonly float _fadeInDuration;
		private readonly float _fadeOutDuration;

		internal SceneTransitioner(Image image, float fadeInDuration, float fadeOutDuration, AnimationCurve fadeInCurve, AnimationCurve fadeOutCurve) {
			_image = image;
			_fadeInDuration = fadeInDuration;
			_fadeOutDuration = fadeOutDuration;
			_fadeInCurve = fadeInCurve;
			_fadeOutCurve = fadeOutCurve;

			_image.enabled = false;
		}

		internal IEnumerator CO_FadeIn() {
			_image.enabled = true;
			_image.material.SetFloat(_isTransitionInverted, 1.0f);

			float elapsedTime = 0.0f;

			while (elapsedTime < _fadeInDuration) {

				float t = elapsedTime / _fadeInDuration;

				_image.material.SetFloat(_transitionProgress, _fadeInCurve.Evaluate(t));

				elapsedTime += Time.deltaTime;
				yield return null;
			}

			_image.enabled = false;
		}
		
		internal IEnumerator CO_FadeOut() {
			_image.enabled = true;
			IsTransitionDone = false;

			_image.material.SetFloat(_isTransitionInverted, 0.0f);

			float elapsedTime = 0.0f;

			while (elapsedTime < _fadeOutDuration) {

				float t = elapsedTime / _fadeOutDuration;

				_image.material.SetFloat(_transitionProgress, _fadeOutCurve.Evaluate(t));

				elapsedTime += Time.deltaTime;
				yield return null;
			}

			IsTransitionDone = true;
		}

	}
}