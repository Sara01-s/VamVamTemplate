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
        /// Occurs when the timer ends.
        /// </summary>
        public event Action OnTimerEnd;

		/// <summary>
        /// Creates a new timer with the specified duration.
        /// </summary>
        /// <param name="durationSeconds">The duration of the timer in seconds.</param>
        public Timer(float durationSeconds) {
            RemainingSeconds = durationSeconds;
        }

		/// <summary>
        /// Decreases the remaining seconds on the timer by the specified amount.
        /// </summary>
        /// <param name="deltaTime">The amount of time to decrease from the timer.</param>
        public void Tick(float deltaTime) {
            if (RemainingSeconds <= 0) return;

            RemainingSeconds -= deltaTime;

            CheckForTimerEnd();
        }

        private void CheckForTimerEnd() {
            if (RemainingSeconds > 0f) return;

            RemainingSeconds = 0f;
            OnTimerEnd?.Invoke();
        }

    }
}