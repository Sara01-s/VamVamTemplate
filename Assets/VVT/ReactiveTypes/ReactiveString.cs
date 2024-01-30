using UnityEngine;
using UniRx;

namespace VVT.Rx {

	[CreateAssetMenu(menuName = "Reactive Types/New Reactive String", fileName = "New Reactive String")]
	internal sealed class ReactiveString : ScriptableObject {
		
		public string Value {
			get => _reactiveProperty.Value;
			set => _reactiveProperty.Value = value;
		}

		private readonly StringReactiveProperty _reactiveProperty = new();

		public void Suscribe(System.Action<string> action) {
			_reactiveProperty.Subscribe(action);
		}

		public void Unsuscribe() {
			_reactiveProperty.Dispose();
		}

	}
}