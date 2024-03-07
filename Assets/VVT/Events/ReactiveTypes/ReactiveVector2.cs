using UnityEngine;
using UniRx;

namespace VVT.Rx {

	[CreateAssetMenu(menuName = "Reactive Types/New Reactive Vector2", fileName = "New Reactive Vector2")]
	internal sealed class ReactiveVector2 : ScriptableObject {
		
		public Vector2 Value {
			get => _reactiveProperty.Value;
			set => _reactiveProperty.Value = value;
		}

		private readonly Vector2ReactiveProperty _reactiveProperty = new();

		public void Suscribe(System.Action<Vector2> action) {
			_reactiveProperty.Subscribe(action);
		}

		public void Unsuscribe() {
			_reactiveProperty.Dispose();
		}

	}
}