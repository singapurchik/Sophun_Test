using System;

namespace Leaderboard.Item
{
	[Serializable]
	public sealed class LeaderboardItemData
	{
		public string name;
		public int score;
		public string avatar;
		public string type;
	}
}