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
		[SerializeField] private TextMeshProUGUI _nameText;
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private TextMeshProUGUI _typeText;
		[SerializeField] private TextMeshProUGUI _loadingText;

		public void Initialize(string name, string score, string type)
		{
			_nameText.text = name ?? "";
			_scoreText.text = score;
			_typeText.text = type ?? "";
		}
	}
}