using System.Collections.Generic;
using System.Threading.Tasks;
using Leaderboard.Item;
using System.Threading;
using UnityEngine;
using System;

namespace Leaderboard
{
	public sealed class LeaderboardJsonProvider
	{
		private const string ResourcePath = "Leaderboard";

		[Serializable]
		private sealed class Wrapper
		{
			public List<LeaderboardItemData> leaderboard;
		}

		public async Task<IReadOnlyList<LeaderboardItemData>> LoadAsync(CancellationToken ct)
		{
			await Task.Yield();
			var textAsset = Resources.Load<TextAsset>(ResourcePath);
			
			if (!textAsset)
				return Array.Empty<LeaderboardItemData>();
			
			var trim = textAsset.text?.Trim() ?? "[]";
			
			List<LeaderboardItemData> list;
			
			if (trim.Length > 0 && trim[0] == '[')
				trim = $"{{\"leaderboard\":{trim}}}";
			
			try
			{
				list = JsonUtility.FromJson<Wrapper>(trim)?.leaderboard ?? new List<LeaderboardItemData>();
			}
			catch
			{
				list = new List<LeaderboardItemData>();
			}
			
			list.Sort((a, b) => b.score.CompareTo(a.score));
			return list;
		}
	}
}