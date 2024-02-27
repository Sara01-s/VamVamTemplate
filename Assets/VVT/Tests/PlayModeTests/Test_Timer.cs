using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

namespace VVT.RuntimeTests {

    public class Test_Timer {
		
		private const float TIMER_DURATION = 10.0F;

        private Timer _timer;
		private System.Action _onTimerEndCallback;

        [SetUp]
        public void Setup() {
            _timer = new Timer(TIMER_DURATION);
			_onTimerEndCallback = Substitute.For<System.Action>();

			_timer.OnTimerEnd += _onTimerEndCallback;
        }

        [UnityTest]
        public IEnumerator Timer_InitializesCorrectly() {
            Assert.AreEqual(TIMER_DURATION, _timer.DurationSeconds);
            Assert.AreEqual(TIMER_DURATION, _timer.RemainingSeconds);
            Assert.IsFalse(_timer.IsTicking);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Timer_StartsTicking() {
            _timer.StartTicking();
			
            yield return null;

            Assert.IsTrue(_timer.IsTicking);
        }

        [UnityTest]
        public IEnumerator Timer_Pauses() {
            _timer.StartTicking();
            _timer.Pause();

            yield return null;

            Assert.IsFalse(_timer.IsTicking);
        }

        [UnityTest]
        public IEnumerator Timer_Resumes() {
            _timer.StartTicking();
            _timer.Pause();
            _timer.Resume();

            yield return null;

            Assert.IsTrue(_timer.IsTicking);
        }

        [UnityTest]
        public IEnumerator Timer_SkipsToEnd() {
            _timer.StartTicking();
            _timer.Skip();

            yield return null;

            Assert.IsFalse(_timer.IsTicking);
            Assert.AreEqual(0.0f, _timer.RemainingSeconds);
        }

		[UnityTest]
        public IEnumerator Timer_RaiseCallbacks() {
            _timer.StartTicking();
            _timer.Skip();

            yield return null;

			_onTimerEndCallback.Received().Invoke();
        }

        [UnityTest]
        public IEnumerator Timer_Resets() {
            _timer.StartTicking();
            _timer.Reset();

            yield return null;

            Assert.IsFalse(_timer.IsTicking);
            Assert.AreEqual(TIMER_DURATION, _timer.RemainingSeconds);
        }
    }
}
