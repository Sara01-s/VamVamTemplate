using UnityEngine.Events;
using NaughtyAttributes;
using UnityEngine;

namespace VVT {

	[RequireComponent(typeof(Collider2D))]
	internal sealed class VVTTrigger2DEvent : MonoBehaviour {
		
		[SerializeField] private bool _useTag = true;
		[SerializeField, ShowIf(nameof(_useTag))] private string _tagToDetect;
		[SerializeField, HideIf(nameof(_useTag))] private LayerMask _layerToDetect;

		[SerializeField] private UnityEvent<Collider2D> _onTriggerEnter;
		[SerializeField] private UnityEvent<Collider2D> _onTriggerStay;
		[SerializeField] private UnityEvent<Collider2D> _onTriggerExit;

		private void Awake() {
			GetComponent<Collider2D>().isTrigger = true;
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (_useTag) {
				if (other.CompareTag(_tagToDetect)) {
					_onTriggerEnter?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onTriggerEnter?.Invoke(other);
			}
		}

		private void OnTriggerStay2D(Collider2D other) {
			if (_useTag) {
				if (other.CompareTag(_tagToDetect)) {
					_onTriggerStay?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onTriggerStay?.Invoke(other);
			}
		}

		private void OnTriggerExit2D(Collider2D other) {
			if (_useTag) {
				if (other.CompareTag(_tagToDetect)) {
					_onTriggerExit?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onTriggerExit?.Invoke(other);
			}
		}

	}
}