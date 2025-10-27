using UnityEngine;
using System;

namespace Leaderboard.Item
{
	public class LeaderboardItem : MonoBehaviour
	{
		[SerializeField] private LeaderboardItemView _view;
		
		public event Action<LeaderboardItem> OnRemoved;

		public void Initialize(LeaderboardItemData data)
			=> _view.Initialize(data.name, data.score.ToString(), data.type);

		public void Remove() => OnRemoved?.Invoke(this);
	}
}