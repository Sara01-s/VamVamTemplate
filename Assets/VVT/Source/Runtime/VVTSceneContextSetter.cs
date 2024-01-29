using UnityEngine;

namespace VVT.Runtime {

    internal sealed class VVTSceneContextSetter : MonoBehaviour {
        
        [SerializeField] private Context _sceneInitContext;
        [SerializeField] private Optional<float> _delay;

        private IContextService _contextService;

        private void Awake() {
            _contextService = Services.Instance.GetService<IContextService>();
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
            _contextService.UpdateGameContext(_sceneInitContext);
        }


    }
}