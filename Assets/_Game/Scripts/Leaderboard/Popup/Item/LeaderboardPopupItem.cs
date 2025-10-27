using UnityEngine;
using System;

namespace Leaderboard.Popup
{
	public class LeaderboardPopupItem : MonoBehaviour
	{
		[SerializeField] private LeaderboardPopupItemView _view;
		
		public event Action<LeaderboardPopupItem> OnRemoved;

		public void Initialize(LeaderboardItemData data)
		{
			_view.Initialize(data.name, data.score, data.type);
		}

		public void Remove() => OnRemoved?.Invoke(this);
	}
}