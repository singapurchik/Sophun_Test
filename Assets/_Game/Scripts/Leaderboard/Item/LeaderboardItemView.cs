using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Leaderboard.Item
{
	[Serializable]
	public class LeaderboardItemView
	{
		[SerializeField] private Image _avatar;
		[SerializeField] private Image _background;
		[SerializeField] private TextMeshProUGUI _nameText;
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private TextMeshProUGUI _loadingText;

		public void Initialize(string name, string score)
		{
			_nameText.text = name ?? "";
			_scoreText.text = score;
		}
		
		public void ShowLoading()
		{
			_loadingText.enabled = true;
			_avatar.sprite = null;
		}

		public void SetAvatar(Sprite sprite)
		{
			_loadingText.enabled = false;
			_avatar.sprite = sprite;
		}
		
		public void SetBackgroundColor(Color color) => _background.color = color;
	}
}