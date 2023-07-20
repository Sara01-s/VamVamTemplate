using VamVam.Source.Utils;
using VamVam.Source.Core;
using UnityEngine;

namespace VamVam.Scripts.Utils {

    internal sealed class VVTSceneContextSetter : MonoBehaviour {
        
        [SerializeField] private GameContext _sceneInitContext;
        [SerializeField] private Optional<float> _delay;

        private void Start() {
            if (_delay.Enabled) {
                _delay.Value = (_delay.Value <= 0f) ? 0.1f : _delay.Value;
                Invoke(nameof(UpdateContext), _delay.Value);
            }
            else
                UpdateContext();
        }

        private void UpdateContext() {
            GameContextUpdater.UpdateGameContext(GameContextData.PreviousContext, _sceneInitContext);
        }


    }
}