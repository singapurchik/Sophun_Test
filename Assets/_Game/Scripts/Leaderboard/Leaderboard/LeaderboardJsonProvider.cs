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

		[Serializable] private sealed class Wrapper
		{
			public List<LeaderboardItemData> leaderboard;
		}

		public async Task<IReadOnlyList<LeaderboardItemData>> LoadAsync(CancellationToken ct)
		{
			await Task.Yield();
			var ta = Resources.Load<TextAsset>(ResourcePath);
			if (!ta) return Array.Empty<LeaderboardItemData>();
			var src = ta.text?.Trim() ?? "[]";
			List<LeaderboardItemData> list = null;
			if (src.Length > 0 && src[0] == '[')
				src = $"{{\"leaderboard\":{src}}}";
			try
			{
				var w = JsonUtility.FromJson<Wrapper>(src);
				list = w?.leaderboard ?? new List<LeaderboardItemData>();
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