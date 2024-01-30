using UnityEngine;

namespace VVT.UI {

	[CreateAssetMenu(menuName = "VVT UI/New View Data", fileName = "New View Data")]
	internal sealed class ViewData : ScriptableObject {
		
		public string Name;
		public RectOffset Padding;
		public float Spacing;

	}
}
