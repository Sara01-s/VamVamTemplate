using UnityEngine.Events;
using NaughtyAttributes;
using UnityEngine;

namespace VVT {

	[RequireComponent(typeof(Collider))]
	internal sealed class VVTCollisionEvent : MonoBehaviour {
		
		[SerializeField] private bool _useTag = true;
		[SerializeField, ShowIf(nameof(_useTag))] private string _tagToDetect;
		[SerializeField, HideIf(nameof(_useTag))] private LayerMask _layerToDetect;

		[SerializeField] private UnityEvent<Collision> _onCollisionEnter;
		[SerializeField] private UnityEvent<Collision> _onCollisionStay;
		[SerializeField] private UnityEvent<Collision> _onCollisionExit;

		private void Awake() {
			GetComponent<Collider>().isTrigger = false;
		}

		private void OnCollisionEnter(Collision other) {
			if (_useTag) {
				if (other.transform.CompareTag(_tagToDetect)) {
					_onCollisionEnter?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onCollisionEnter?.Invoke(other);
			}
		}

		private void OnCollisionStay(Collision other) {
			if (_useTag) {
				if (other.transform.CompareTag(_tagToDetect)) {
					_onCollisionStay?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onCollisionStay?.Invoke(other);
			}
		}

		private void OnCollisionExit(Collision other) {
			if (_useTag) {
				if (other.transform.CompareTag(_tagToDetect)) {
					_onCollisionExit?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onCollisionExit?.Invoke(other);
			}
		}

	}
}