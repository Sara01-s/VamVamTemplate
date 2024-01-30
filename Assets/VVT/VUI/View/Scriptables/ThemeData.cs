using UnityEngine;

namespace VVT.UI {

	internal enum Style {
		Primary,
		Secondary,
		Tertiary
	}

	[CreateAssetMenu(menuName = "VVT UI/New Theme Data", fileName = "New Theme Data")]
	internal sealed class ThemeData : ScriptableObject {
		
		[Header("Primary")]
		public Color PrimaryBG;
		public Color PrimaryText;

		[Header("Secondary")]
		public Color SecondaryBG;
		public Color SecondaryText;
		
		[Header("Tertiary")]
		public Color TertiaryBG;
		public Color TertiaryText;

		[Header("Other")]
		public Color Disable;

		public Color GetBackgroundColor(Style style) {
			return style switch {
				Style.Primary => PrimaryBG,
				Style.Secondary => SecondaryBG,
				Style.Tertiary => TertiaryBG,
				_ => Disable,
			};
		}

		public Color GetTextColor(Style style) {
			return style switch {
				Style.Primary => PrimaryText,
				Style.Secondary => SecondaryText,
				Style.Tertiary => TertiaryText,
				_ => Disable,
			};
		}

	}
}
