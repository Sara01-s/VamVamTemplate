using UnityEngine;
using UniRx;

namespace VVT.Rx {

	[CreateAssetMenu(menuName = "Reactive Types/New Reactive Color", fileName = "New Reactive Color")]
	internal sealed class ReactiveColor : ScriptableObject {
		
		public Color Value {
			get => _reactiveProperty.Value;
			set => _reactiveProperty.Value = value;
		}

		private readonly ColorReactiveProperty _reactiveProperty = new();

		public void Suscribe(System.Action<Color> action) {
			_reactiveProperty.Subscribe(action);
		}

		public void Unsuscribe() {
			_reactiveProperty.Dispose();
		}

	}
}