using UnityEngine;
using System;

namespace Leaderboard
{
	public interface ILeaderboardView
	{
		public Transform CanvasTransform { get; }
		
		public event Action OnOpenPopupButtonClicked;
	}
}