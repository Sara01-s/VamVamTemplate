using UnityEngine.Events;
using NaughtyAttributes;
using UnityEngine;

namespace VVT {

	[RequireComponent(typeof(Collider))]
	internal sealed class VVTCollision2DEvent : MonoBehaviour {
		
		[SerializeField] private bool _useTag = true;
		[SerializeField, ShowIf(nameof(_useTag))] private string _tagToDetect;
		[SerializeField, HideIf(nameof(_useTag))] private LayerMask _layerToDetect;

		[SerializeField] private UnityEvent<Collision2D> _onCollisionEnter2D;
		[SerializeField] private UnityEvent<Collision2D> _onCollisionStay2D;
		[SerializeField] private UnityEvent<Collision2D> _onCollisionExit2D;

		private void Awake() {
			GetComponent<Collider>().isTrigger = false;
		}

		private void OnCollisionEnter2D(Collision2D other) {
			if (_useTag) {
				if (other.transform.CompareTag(_tagToDetect)) {
					_onCollisionEnter2D?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onCollisionEnter2D?.Invoke(other);
			}
		}

		private void OnCollisionStay2D(Collision2D other) {
			if (_useTag) {
				if (other.transform.CompareTag(_tagToDetect)) {
					_onCollisionStay2D?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onCollisionStay2D?.Invoke(other);
			}
		}

		private void OnCollisionExit2D(Collision2D other) {
			if (_useTag) {
				if (other.transform.CompareTag(_tagToDetect)) {
					_onCollisionExit2D?.Invoke(other);
				}

				return;
			}

			if (Comparator.CompareLayerAndMask(other.gameObject.layer, _layerToDetect)) {
				_onCollisionExit2D?.Invoke(other);
			}
		}

	}
}