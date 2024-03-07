using UnityEngine.Events;
using NaughtyAttributes;
using UnityEngine;

namespace VVT {

	[RequireComponent(typeof(Collider2D))]
	internal sealed class VVTTrigger2DEvent : MonoBehaviour {
		
		[SerializeField] private bool _useTag = true;
		[SerializeField, ShowIf(nameof(_useTag))] private string _tagToDetect;
		[SerializeField, HideIf(nameof(_useTag))] private LayerMask _layerToDetect;

		[Foldout("On Trigger2D Enter")]
		[SerializeField] private UnityEvent<Collider2D> _onTriggerEnter2D;
		[Foldout("On Trigger2D Stay")]
		[SerializeField] private UnityEvent<Collider2D> _onTriggerStay2D;
		[Foldout("On Trigger2D Exit")]
		[SerializeField] private UnityEvent<Collider2D> _onTriggerExit2D;

		private void Awake() {
			GetComponent<Collider2D>().isTrigger = true;
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (_useTag) {
				if (other.CompareTag(_tagToDetect)) {
					_onTriggerEnter2D?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onTriggerEnter2D?.Invoke(other);
			}
		}

		private void OnTriggerStay2D(Collider2D other) {
			if (_useTag) {
				if (other.CompareTag(_tagToDetect)) {
					_onTriggerStay2D?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onTriggerStay2D?.Invoke(other);
			}
		}

		private void OnTriggerExit2D(Collider2D other) {
			if (_useTag) {
				if (other.CompareTag(_tagToDetect)) {
					_onTriggerExit2D?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onTriggerExit2D?.Invoke(other);
			}
		}

	}
}