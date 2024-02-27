using static Unity.Mathematics.math;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using MEC;

namespace VVT {

	/// <summary>
	/// RealTimer is used to make timers with persistent state.
	/// Designed to be used along a server clock or custom save systems.
	/// </summary>
	public sealed class RealTimer {
		
		/// <summary>
		/// Event triggered when the timer ends.
		/// </summary>
		public event Action OnTimerEnd;

		/// <summary>
		/// Gets the name of the timer.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Is the timer currently active?.
		/// </summary>
		public bool IsTicking { get; private set; }

		/// <summary>
		/// Timer ends when this value is equal to 0.
		/// </summary>
		public double RemainingSeconds { get; private set; }

		/// <summary>
		/// Total duration time of the timer.
		/// </summary>
		public TimeSpan Duration { get; private set; }

		/// <summary>
		/// When was the timer started.
		/// </summary>
		public DateTime StartTime { get; private set; }

		/// <summary>
		/// When will this timer finish.
		/// </summary>
		public DateTime FinishTime { get; private set; }


		/// <summary>
		/// Creates a new Real-time Timer.
		/// </summary>
		/// <param name="name">The name of the timer. It can be used as an identifier.</param>
		/// <param name="start">When should this timer start.</param>
		/// <param name="duration">When should this timer end.</param>
		public RealTimer(string name, DateTime start, TimeSpan duration) {
			Name = name;
			Duration = duration;

			StartTime = start;
			FinishTime = start.Add(duration);
		}

		/// <summary>
		/// Starts decreasing <c>RemainingSeconds</c> over the game frames.
		/// </summary>
		public void StartTicking() {
			Timing.RunCoroutine(_StartTicking());

			IEnumerator<float> _StartTicking() {
				RemainingSeconds = Duration.TotalSeconds;
				IsTicking = true;

				while (IsTicking) {
					RemainingSeconds -= Time.deltaTime;

					CheckForTimerEnd();
					yield return Timing.WaitForOneFrame;
				}
			}
		}

		/// <summary>
		/// Checks if the timer has reached the <c>FinishTime</c>.
		/// Raises the event <c>OnTimerEnd</c> if <c>RemainingSeconds = 0</c> 
		/// </summary>
		public void CheckForTimerEnd() {
			if (RemainingSeconds <= 0.1d) {
				RemainingSeconds = 0.0d;
				FinishTime = DateTime.Now;
				IsTicking = false;

				OnTimerEnd?.Invoke();
			}
		}

		/// <summary>
		/// Gets the time left on the timer in <c> 00h00min00sec </c> format.
		/// </summary>
		/// <returns>A string representing the time left on the timer.</returns>
		public string GetFormattedTime() {
			var formattedTime = new StringBuilder();
			var timeLeft = TimeSpan.FromSeconds(RemainingSeconds);

			if (timeLeft.Days > 0) {
				formattedTime.AppendLine($"{timeLeft.Days}d ");
			}

			if (timeLeft.Hours > 0) {
				formattedTime.AppendLine($"{timeLeft.Hours}h ");
			}

			if (timeLeft.Minutes > 0) {
				formattedTime.AppendLine($"{timeLeft.Minutes}min ");
			}

			if (timeLeft.Seconds > 0 || RemainingSeconds > 0.0d) {
				formattedTime.AppendLine($"{(float) floor(RemainingSeconds)}sec ");
			}

			if (timeLeft.Seconds <= 0) {
				formattedTime.Clear();
				return "Finished";
			}

			return formattedTime.ToString();
		}

		/// <summary>
		/// Forces the timer to reach it's end.
		/// Calls <c>CheckForTimerEnd</c> internally.
		/// </summary>
		public void SkipTimer() {
			RemainingSeconds = 0.0d;
			CheckForTimerEnd();
		}
		
	}
}
