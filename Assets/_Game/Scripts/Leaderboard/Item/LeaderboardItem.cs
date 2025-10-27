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

		private CancellationTokenSource _cancellationTokenSource;
		
		public event Action<LeaderboardItem> OnRemoved;

		public void Initialize(LeaderboardItemData data)
		{
			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource = new CancellationTokenSource();
			
			var styleType = _styles.Get(data.type);
			transform.localScale = new Vector3(1, styleType.ScaleFactor, 1);
			_view.Initialize(data.name, data.score.ToString());
			_view.SetBackgroundColor(styleType.Color);
			_view.ShowLoading();
			
			_ = LoadAvatarAsync(data.avatar, _cancellationTokenSource.Token);
		}
		
		private async Task LoadAvatarAsync(string url, CancellationToken ct)
		{
			Sprite sprite;

			try
			{
				sprite = await _avatars.LoadSpriteAsync(url, 100, ct);
			}
			catch (TaskCanceledException)
			{
				return;
			}
			catch (Exception)
			{
				sprite = null;
			}

			if (this&& !ct.IsCancellationRequested)
				_view.SetAvatar(sprite);
		}


		public void Remove()
		{
			_cancellationTokenSource?.Cancel();
			OnRemoved?.Invoke(this);
		}
	}
}