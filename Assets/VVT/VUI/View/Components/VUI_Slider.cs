using UnityEngine.UI;
using UnityEngine;

namespace VVT.UI {

	internal sealed class VUI_Slider : VUI_Component {

		[SerializeField] private SliderData _sliderData;
		
		[SerializeField] private Slider _slider;
		[SerializeField] private Image _imageBackground;
		[SerializeField] private Image _imageFill;

		internal override void Configure() {
			_slider.interactable = _sliderData.Interactable;
			_slider.minValue = _sliderData.Min;
			_slider.maxValue = _sliderData.Max;

			_imageBackground.color = _sliderData.BackgroundColor;
			_imageFill.color = _sliderData.FillColor;
		}

		public void SetValue(float value) {
			_slider.value = value;
		}

	}
}