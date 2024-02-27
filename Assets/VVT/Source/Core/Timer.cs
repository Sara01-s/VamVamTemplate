using System;

namespace VVT {

	/// <summary>
    /// Represents a timer that triggers an event when it ends.
    /// </summary>
    public sealed class Timer {

		/// <summary>
        /// Gets the remaining seconds on the timer.
        /// </summary>
        public float RemainingSeconds { get; private set; }

		/// <summary>
        /// Gets the total duration on the timer in seconds.
        /// </summary>
        public float DurationSeconds { get; private set; }

		/// <summary>
        /// Is the timer currently running?
        /// </summary>
        public bool IsTicking { get; private set; }

		/// <summary>
        /// Occurs when the timer ends.
        /// </summary>
        public event Action OnTimerEnd;

		/// <summary>
        /// Creates a new timer with the specified duration.
        /// </summary>
        /// <param name="durationSeconds">The duration of the timer in seconds.</param>
        public Timer(float durationSeconds) {
			DurationSeconds = durationSeconds;
            RemainingSeconds = durationSeconds;
        }

		/// <summary>
        /// Decreases the remaining seconds on the timer by the specified amount.
        /// </summary>
        /// <param name="deltaTime">The amount of time to decrease from the timer.</param>
        public void Tick(float deltaTime) {
            if (RemainingSeconds <= 0) return;

			IsTicking = true;
            RemainingSeconds -= deltaTime;

            CheckForTimerEnd();
        }

		/// <inheritdoc cref="Tick(float)"/>
        public void Tick() {
			Tick(UnityEngine.Time.deltaTime);
        }

		/// <summary>
        /// Starts decreasing <c>RemainingSeconds</c> until the timer finishes.
        /// </summary>
        public void StartTicking() {
			IsTicking = true;
			MEC.Timing.RunCoroutine(_StartTicking());

			System.Collections.Generic.IEnumerator<float> _StartTicking() {
				while (IsTicking) {
            		RemainingSeconds -= UnityEngine.Time.deltaTime;
            		CheckForTimerEnd();

					yield return MEC.Timing.WaitForOneFrame;
				}
			}
        }

		/// <summary>
        /// Pauses the timer ticking.
        /// </summary>
		public void Pause() {
			IsTicking = false;
		}

		/// <summary>
        /// Resumes the timer ticking.
        /// </summary>
		public void Resume() {
			if (!IsTicking) {
				IsTicking = true;
			}
		}

		/// <summary>
        /// Forces the timer to finish.
        /// </summary>
		public void Skip() {
			IsTicking = false;
			RemainingSeconds = 0.0f;

			CheckForTimerEnd();
		}

		/// <summary>
        /// Restores the timer initial state.
        /// </summary>
		public void Reset() {
			IsTicking = false;
			RemainingSeconds = DurationSeconds;
		}

        private void CheckForTimerEnd() {
            if (RemainingSeconds > 0.01f) return;

            RemainingSeconds = 0.0f;
			IsTicking = false;

            OnTimerEnd?.Invoke();
        }

    }
}