using Leaderboard.Popup;
using Zenject;
using Popup;

namespace Leaderboard
{
	public class Leaderboard
	{
		[Inject] private IPopupManagerService _popupManager;
		[Inject] private ILeaderboardView _view;

		private string _popupAddress;

		public void Initialize(string popupAddress)
		{
			_popupAddress = popupAddress;
			_view.OnOpenPopupButtonClicked += OnOpenPopupButtonClicked;
		}

		private void OnOpenPopupButtonClicked()
		{
			_view.HideOpenButton();
			var info = new LeaderboardPopupInfo(OnPopupClosed);
			_popupManager.OpenPopup(_popupAddress, info, _view.CanvasTransform);
		}

		private void OnPopupClosed()
		{
			_popupManager.ClosePopup(_popupAddress);
			_view.ShowOpenButton();
		}
	}
}