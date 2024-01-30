using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace VVT.UI {

	internal sealed class VUI_Button : VUI_Component {

		[SerializeField] private Style _buttonStyle;
		[SerializeField] private Style _textStyle;
		[SerializeField] private UnityEvent _onClick;

		private Button _button;
		private TextMeshProUGUI _textButton;

		internal override void Configure() {
			_button = GetComponentInChildren<Button>();
			_textButton = _button.GetComponentInChildren<TextMeshProUGUI>();

			var colorBlock = _button.colors;
			colorBlock.normalColor = _MainTheme.GetBackgroundColor(_buttonStyle);

			_button.colors = colorBlock;
			_textButton.color = _MainTheme.GetTextColor(_textStyle);
		}

		public void OnClick() {
			_onClick?.Invoke();
		}

	}
}
