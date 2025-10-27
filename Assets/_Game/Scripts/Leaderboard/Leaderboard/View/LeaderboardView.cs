using UnityEngine.UI;
using UnityEngine;
using System;

namespace Leaderboard
{
	public class LeaderboardView : MonoBehaviour, ILeaderboardView
	{
		[SerializeField] private Canvas _canvas;
		[SerializeField] private Button _openPopupButton;

		public Transform CanvasTransform => _canvas.transform;
		
		public event Action OnOpenPopupButtonClicked;

		private void OnEnable()
		{
			_openPopupButton.onClick.AddListener(InvokeOnOpenPopupButtonClicked);
		}

		private void OnDisable()
		{
			_openPopupButton.onClick.RemoveListener(InvokeOnOpenPopupButtonClicked);
		}

		private void InvokeOnOpenPopupButtonClicked() => OnOpenPopupButtonClicked?.Invoke();
	}
}