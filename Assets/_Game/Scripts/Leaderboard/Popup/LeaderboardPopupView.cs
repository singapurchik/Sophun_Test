using UnityEngine.UI;
using UnityEngine;
using System;

namespace Leaderboard.Popup
{
	[Serializable]
	public sealed class LeaderboardPopupView
	{
		[SerializeField] private Button _closeButton;

		public event Action OnCloseButtonClicked;

		public void RemoveListeners() => _closeButton.onClick.RemoveListener(InvokeOnCloseButtonClicked);
		
		public void AddListeners() => _closeButton.onClick.AddListener(InvokeOnCloseButtonClicked);

		private void InvokeOnCloseButtonClicked() => OnCloseButtonClicked?.Invoke();
	}
}