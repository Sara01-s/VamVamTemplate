using UnityEngine;
using UniRx;

namespace VVT.Rx {

	[CreateAssetMenu(menuName = "Reactive Types/New Reactive Vector3", fileName = "New Reactive Vector3")]
	internal sealed class ReactiveVector3 : ScriptableObject {
		
		public Vector3 Value {
			get => _reactiveProperty.Value;
			set => _reactiveProperty.Value = value;
		}

		private readonly Vector3ReactiveProperty _reactiveProperty = new();

		public void Suscribe(System.Action<Vector3> action) {
			_reactiveProperty.Subscribe(action);
		}

		public void Unsuscribe() {
			_reactiveProperty.Dispose();
		}

	}
}