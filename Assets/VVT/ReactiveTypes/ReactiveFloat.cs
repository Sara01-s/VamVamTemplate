using UnityEngine;
using UniRx;

namespace VVT.Rx {

	[CreateAssetMenu(menuName = "Reactive Types/New Reactive Float", fileName = "New Reactive Float")]
	internal sealed class ReactiveFloat : ScriptableObject {
		
		public float Value {
			get => _reactiveProperty.Value;
			set => _reactiveProperty.Value = value;
		}

		private readonly FloatReactiveProperty _reactiveProperty = new();

		public void Suscribe(System.Action<float> action) {
			_reactiveProperty.Subscribe(action);
		}

		public void Unsuscribe() {
			_reactiveProperty.Dispose();
		}

	}
}