using UnityEngine;

namespace VVT.Runtime {

    internal sealed class VVTSceneContextSetter : MonoBehaviour {
        
        [SerializeField] private GameContext _sceneInitContext;
        [SerializeField] private Optional<float> _delay;

        private IContextService _gameContext;

        private void Awake() {
            _gameContext = Services.Instance.GetService<IContextService>();
        }

        private void Start() {
            if (_delay.Enabled) {
                _delay.Value = (_delay.Value <= 0f) ? 0.1f : _delay.Value;
                Invoke(nameof(UpdateContext), _delay.Value);
            }
            else
                UpdateContext();
        }

        private void UpdateContext() {
            _gameContext.UpdateGameContext(_gameContext.Info.PreviousContext, _sceneInitContext);
        }


    }
}