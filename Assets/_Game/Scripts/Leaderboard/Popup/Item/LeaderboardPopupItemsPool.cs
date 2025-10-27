using Core;

namespace Leaderboard.Popup
{
	public class LeaderboardPopupItemsPool : ObjectPool<LeaderboardPopupItem>
	{
		protected override void InitializeObject(LeaderboardPopupItem leaderboardPopupItem)
		{
			leaderboardPopupItem.OnRemoved += ReturnToPool;
		}

		protected override void CleanupObject(LeaderboardPopupItem leaderboardPopupItem)
		{
			leaderboardPopupItem.OnRemoved -= ReturnToPool;
		}
	}
}