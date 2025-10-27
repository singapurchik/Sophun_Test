using System;

namespace Leaderboard.Popup
{
	public readonly struct LeaderboardPopupInfo
	{
		public readonly Action OnClose;
		
		public LeaderboardPopupInfo(Action onClose) => OnClose = onClose;
	}
}