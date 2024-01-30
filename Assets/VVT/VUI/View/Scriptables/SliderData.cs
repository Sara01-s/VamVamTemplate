using UnityEngine;

namespace VVT.UI {

	[CreateAssetMenu(menuName = "VVT UI/New Slider Data", fileName = "New Slider Data")]
	internal sealed class SliderData : ScriptableObject {

		public bool Interactable;
		public float Min;
		public float Max;
		public Color BackgroundColor;
		public Color FillColor;

	}
}
