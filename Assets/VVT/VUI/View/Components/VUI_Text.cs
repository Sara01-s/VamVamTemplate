using UnityEngine;
using TMPro;

namespace VVT.UI {

	internal sealed class VUI_Text : VUI_Component {

		[SerializeField] private TextData _textData;
		[SerializeField] private Style _style;

		private TextMeshProUGUI _text;

		internal override void Configure() {
			_text = GetComponentInChildren<TextMeshProUGUI>();

			_text.color = _MainTheme.GetTextColor(_style);
			_text.font = _textData.Font;
			_text.fontSize = _textData.FontSize;
		}

		public void SetText(string text) {
			_text.text = text;
		}

	}
}