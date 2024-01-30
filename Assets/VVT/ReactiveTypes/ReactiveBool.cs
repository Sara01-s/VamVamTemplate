using UnityEngine;
using UniRx;

namespace VVT.Rx {

	[CreateAssetMenu(menuName = "Reactive Types/New Reactive Bool", fileName = "New Reactive Bool")]
	internal sealed class ReactiveBool : ScriptableObject {
		
		public bool Value {
			get => _reactiveProperty.Value;
			set => _reactiveProperty.Value = value;
		}

		private readonly BoolReactiveProperty _reactiveProperty = new();

		public void Suscribe(System.Action<bool> action) {
			_reactiveProperty.Subscribe(action);
		}

		public void Unsuscribe() {
			_reactiveProperty.Dispose();
		}

	}
}