using NaughtyAttributes;
using UnityEngine;

namespace VVT.Runtime {

	/// <summary>
    /// Represents an audio player that can play, pause, resume, and stop audio.
    /// </summary>
	public sealed class VVTAudioPlayer : MonoBehaviour {

		[Header("Output Settings")]
		[SerializeField] private Mixer _mixerOutput = Mixer.SFX;
		[SerializeField] private bool _playOnAwake = false;
		[SerializeField] private bool _loop = false;
		[SerializeField, Range(0.0f, 1.0f)] private float _overrideVolume = 1.0f;
		[SerializeField] private bool _fadeIn = false;
		[SerializeField, ShowIf(nameof(_fadeIn))] private AnimationCurve _fadeInCurve;
		[SerializeField] private bool _fadeOut = false;
		[SerializeField, ShowIf(nameof(_fadeOut))] private AnimationCurve _fadeOutCurve;

		[Header("Clip Settings")]
		#pragma warning disable
		[SerializeField] private bool _useMultipleClips = false;
		#pragma warning enable
		[SerializeField, ShowIf(EConditionOperator.And, nameof(_useAudioClip), nameof(_useMultipleClips))]
		private AudioClip[] _audioClips;
		[SerializeField, ShowIf(nameof(_useMultipleClips))]
		private bool _useRandomClip;
		[SerializeField] private bool _useAudioClip = false;
		[SerializeField, HideIf(nameof(_useAudioClip))] 
		private string _audioFileName = "";
		[SerializeField, ShowIf(nameof(_useAudioClip))] 
		private AudioClip _audioClip;

		[Header("Pitch Settings")]
		[SerializeField] private bool _randomizePitch = false;
		[SerializeField, ShowIf(nameof(_randomizePitch)), MinMaxSlider(-3.0f, 3.0f)] private Vector2 _pitchVariationRange;
		[Tooltip("A pitch with value 0 will not sound at all")]
		[SerializeField, HideIf(nameof(_randomizePitch)), Range(-3.0f, 3.0f)] private float _overridePitch = 1.0f;

		[Header("Spatial Settings"), Tooltip("2D = 0.0, 3D = 1.0")]
		[SerializeField, Range(0.0f, 1.0f)] private float _spatialBlend = 0.0f;

		private AudioSource _currentAudioSource = default;
		private IAudioService _audioService;

		private void Awake() {
			_audioService = Services.Instance.GetService<IAudioService>();

			if (_playOnAwake) {
				Play();
			}
		}

		/// <summary>
        /// Plays an audio clip or file with optional pitch randomization and fade-in.
        /// </summary>
		public void Play() {
			// Note: a pitch = 0 will not sound at all, sorry if you ended here because of that...
			float pitch = _randomizePitch 
				? Random.Range(_pitchVariationRange.x, _pitchVariationRange.y) 
				: _overridePitch;

			if (_useAudioClip) {
				if (_audioClips != null) {

					var clip = _useRandomClip
						? _audioClips[Random.Range(0, _audioClips.Length)]
						: _audioClip;

					_currentAudioSource = _audioService.PlaySound(clip, _mixerOutput, _overrideVolume, pitch, _loop, _spatialBlend);
					if (_fadeIn) {
						_currentAudioSource.FadeIn(_fadeInCurve);
					}
					return;
				}

				_currentAudioSource = _audioService.PlaySound(_audioClip, _mixerOutput, _overrideVolume, pitch, _loop, _spatialBlend);
				if (_fadeIn) {
					_currentAudioSource.FadeIn(_fadeInCurve);
				}
			}
			else {
				_currentAudioSource = _audioService.PlaySound(_audioFileName, _mixerOutput, _overrideVolume, pitch, _loop, _spatialBlend);
				if (_fadeOut) {
					_currentAudioSource.FadeIn(_fadeInCurve);
				}
			}
		}

		/// <summary>
        /// Plays the specified audio clip with pitch calculated by the CalculatePitch method.
        /// </summary>
        /// <param name="soundClip">The audio clip to play.</param>
		public void PlaySound(AudioClip soundClip) {
			float pitch = CalculatePitch();
			_audioService.PlaySound(soundClip, _mixerOutput, _overrideVolume, pitch, _loop, _spatialBlend);
		}
		
		/// <summary>
        /// Plays the specified audio file with pitch calculated by the CalculatePitch method.
        /// </summary>
        /// <param name="soundFileName">The name of the audio file to play.</param>
		public void PlaySound(string soundFileName) {
			float pitch = CalculatePitch();
			_audioService.PlaySound(soundFileName, _mixerOutput, _overrideVolume, pitch, _loop, _spatialBlend);
		}

		private float CalculatePitch() {
			return _randomizePitch 
				? Random.Range(_pitchVariationRange.x, _pitchVariationRange.y) 
				: _overridePitch;
		}

		/// <summary>
        /// Pauses the currently playing audio immediately.
        /// </summary>
		public void PauseImmediate() {
			if (_currentAudioSource == null && !_currentAudioSource.isPlaying) {
				Debug.LogError("Failed to pause, audio is not currently playing");
				return;
			}

			_currentAudioSource.Pause();
		}

		/// <summary>
        /// Resumes the currently paused audio immediately.
        /// </summary>
		public void ResumeImmediate() {
			if (_currentAudioSource.isPlaying) {
				Debug.LogError("Failed to resume, audio is not currently paused");
				return;
			}

			_currentAudioSource.Play();
		}

		/// <summary>
        /// Stops the currently playing audio immediately.
        /// </summary>
		public void StopImmediate() {
			if (_currentAudioSource == null && !_currentAudioSource.isPlaying) {
				Debug.LogError("Failed to resume, audio is not currently paused");
				return;
			}

			_currentAudioSource.Stop();
		}
		
		/// <summary>
        /// Pauses the currently playing audio with a smooth fade-out.
        /// </summary>
		public void PauseSmooth() {
			if (_currentAudioSource == null && !_currentAudioSource.isPlaying) {
				Debug.LogError("Failed to pause, audio is not currently playing");
				return;
			}

			if (_fadeOutCurve == null || !_fadeOut) {
				Debug.LogWarning("Failed to pause, no fade out curve is defined, pausing immediately");
				StopImmediate();
				return;
			}

			_currentAudioSource.FadeOut(_fadeOutCurve);
		}

		/// <summary>
        /// Resumes the currently paused audio with a smooth fade-in.
        /// </summary>
		public void ResumeSmooth() {
			if (_currentAudioSource.isPlaying) {
				Debug.LogError("Failed to resume, audio is not currently paused");
				return;
			}

			if (_fadeInCurve == null || !_fadeIn) {
				Debug.LogWarning("Failed to pause, no fade out curve is defined, pausing immediately");
				StopImmediate();
				return;
			}

			_currentAudioSource.FadeIn(_fadeInCurve);
		}

		/// <summary>
        /// Stops the currently playing audio with a smooth fade-out.
        /// </summary>
		public void StopSmooth() {
			if (_currentAudioSource == null && !_currentAudioSource.isPlaying) {
				Debug.LogError("Failed to resume, audio is not currently paused");
				return;
			}
			
			if (_fadeOutCurve == null || !_fadeOut) {
				Debug.LogWarning("Failed to pause, no fade out curve is defined, pausing immediately");
				StopImmediate();
				return;
			}

			_currentAudioSource.FadeOut(_fadeOutCurve);
		}

	}
}