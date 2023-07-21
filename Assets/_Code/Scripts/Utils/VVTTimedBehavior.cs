using VamVam.Source.Utils;
using UnityEngine.Events;
using UnityEngine;

namespace VamVam.Scripts.Utils {

    internal sealed class VVTTimedBehavior : MonoBehaviour {

        [SerializeField] private float _timeBeforeInvoke = 1f;
        [SerializeField] private UnityEvent _onTimerEnd = null;
        [SerializeField] private bool _destroyObjectAfter = false;

        private Timer _timer;

        private void Start() {
            _timer = new Timer(_timeBeforeInvoke);

            _timer.OnTimerEnd += InvokeEvent;
        }

        private void InvokeEvent() {
            _onTimerEnd.Invoke();

            if (_destroyObjectAfter)
                Destroy(gameObject);
            else
                Destroy(this);                  // Destroys this component, not the gameObject

        }

        private void Update() => _timer.Tick(Time.deltaTime);
    }
}