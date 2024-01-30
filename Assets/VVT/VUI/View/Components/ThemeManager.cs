using UnityEngine;

namespace VVT.UI {

	[DefaultExecutionOrder(-50)]
	internal sealed class ThemeManager : Singleton<ThemeManager> {

		[field:SerializeField] internal ThemeData MainTheme { get; private set; }

		protected override void Awake() {
			base.Awake();
		}

	}
}
