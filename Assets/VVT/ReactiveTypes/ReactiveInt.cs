using UnityEngine;
using UniRx;

namespace VVT.Rx {

	[CreateAssetMenu(menuName = "Reactive Types/New Reactive Int", fileName = "New Reactive Int")]
	internal sealed class ReactiveInt : ScriptableObject {
		
		public int Value {
			get => _reactiveProperty.Value;
			set => _reactiveProperty.Value = value;
		}

		private readonly IntReactiveProperty _reactiveProperty = new();

		public void Suscribe(System.Action<int> action) {
			_reactiveProperty.Subscribe(action);
		}

		public void Unsuscribe() {
			_reactiveProperty.Dispose();
		}

	}
}
