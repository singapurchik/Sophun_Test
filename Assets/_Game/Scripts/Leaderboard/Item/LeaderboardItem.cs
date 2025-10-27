using System.Threading.Tasks;
using Leaderboard.Avatar;
using System.Threading;
using UnityEngine;
using Zenject;
using System;

namespace Leaderboard.Item
{
	public class LeaderboardItem : MonoBehaviour
	{
		[SerializeField] private LeaderboardItemView _view;
		
		[Inject] private PlayerTypeStyleSet _styles;
		[Inject] private IAvatarLoader _avatars;

		private CancellationTokenSource _cts;
		
		public event Action<LeaderboardItem> OnRemoved;

		public void Initialize(LeaderboardItemData data)
		{
			_cts?.Cancel();
			_cts = new CancellationTokenSource();
			
			_view.Initialize(data.name, data.score.ToString(), data.type);
			_view.ApplyTypeStyle(_styles.Get(data.type));
			_view.ShowLoading();
			
			_ = LoadAvatarAsync(data.avatar, _cts.Token);
		}
		
		private async Task LoadAvatarAsync(string url, CancellationToken ct)
		{
			try
			{
				var sprite = await _avatars.LoadSpriteAsync(url, 100, ct);
				if (!this || ct.IsCancellationRequested) return;
				_view.SetAvatar(sprite);
			}
			catch
			{
				if (!this) return;
				_view.SetAvatar(null);
			}
		}

		public void Remove() => OnRemoved?.Invoke(this);
	}
}