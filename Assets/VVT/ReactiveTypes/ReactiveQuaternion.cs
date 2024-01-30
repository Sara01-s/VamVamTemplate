using UnityEngine;
using UniRx;

namespace VVT.Rx {

	[CreateAssetMenu(menuName = "Reactive Types/New Reactive Quaternion", fileName = "New Reactive Quaternion")]
	internal sealed class ReactiveQuaternion : ScriptableObject {
		
		public Quaternion Value {
			get => _reactiveProperty.Value;
			set => _reactiveProperty.Value = value;
		}

		private readonly QuaternionReactiveProperty _reactiveProperty = new();

		public void Suscribe(System.Action<Quaternion> action) {
			_reactiveProperty.Subscribe(action);
		}

		public void Unsuscribe() {
			_reactiveProperty.Dispose();
		}

	}
}