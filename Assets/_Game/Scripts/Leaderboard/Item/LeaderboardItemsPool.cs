using Core;

namespace Leaderboard.Item
{
	public class LeaderboardItemsPool : ObjectPool<LeaderboardItem>
	{
		protected override void InitializeObject(LeaderboardItem leaderboardItem)
		{
			leaderboardItem.OnRemoved += ReturnToPool;
		}

		protected override void CleanupObject(LeaderboardItem leaderboardItem)
		{
			leaderboardItem.OnRemoved -= ReturnToPool;
		}
	}
}