using UnityEngine;
using TMPro;

namespace VVT.UI {

	[CreateAssetMenu(menuName = "VVT UI/New Text Data", fileName = "New Text Data")]
	internal sealed class TextData : ScriptableObject {
		
		public TMP_FontAsset Font;
		public float FontSize;

	}
}
