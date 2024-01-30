using UnityEngine.UI;
using UnityEngine;

namespace VVT.UI {

	internal sealed class VUI_View : VUI_Component {

		[SerializeField] private ViewData _viewData;

		[SerializeField] private GameObject _topContainer;
		[SerializeField] private GameObject _centerContainer;
		[SerializeField] private GameObject _bottomContainer;

		private Image _imageTop;
		private Image _imageCenter;
		private Image _imageBottom;

		private VerticalLayoutGroup _verticalLayout;

		internal override void Configure() {
			_verticalLayout = GetComponent<VerticalLayoutGroup>();
			_imageTop = _topContainer.GetComponent<Image>();
			_imageCenter = _centerContainer.GetComponent<Image>();
			_imageBottom = _bottomContainer.GetComponent<Image>();

			_verticalLayout.padding = _viewData.Padding;
			_verticalLayout.spacing = _viewData.Spacing;

			_imageTop.color = _MainTheme.PrimaryBG;
			_imageCenter.color = _MainTheme.SecondaryBG;
			_imageBottom.color = _MainTheme.TertiaryBG;
		}

	}
}
