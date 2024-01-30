using NaughtyAttributes;
using UnityEngine;

namespace VVT.UI {

	internal abstract class VUI_Component : MonoBehaviour {

		[SerializeField] private Optional<ThemeData> _overwriteTheme;
		[SerializeField] private bool _updateOnValidate;

		private void Awake() {
			Initialize();
		}

		internal abstract void Configure();

		[Button("Update UI")]
		private void Initialize() {
			Configure();
		}

		private void OnValidate() {
			if (_updateOnValidate) {
				Initialize();
			}
		}

		protected ThemeData _MainTheme {
			get {
				if (_overwriteTheme.Enabled) {
					return _overwriteTheme.Value;
				}
				else if (ThemeManager.Instance != null) {
					return ThemeManager.Instance.MainTheme;
				}

				Debug.LogError("Fatal: Main Theme is null!");
				return null;
			}
		}

	}
}