using UnityEngine;
using Zenject;

namespace Leaderboard.Popup
{
	public class LeaderboardPopupInstaller : MonoInstaller
	{
		[SerializeField] private LeaderboardPopupItemsPool _pool;
		[SerializeField] private LeaderboardPopup _leaderboardPopup;

		public override void InstallBindings()
		{
			Container.BindInstance(_pool).WhenInjectedIntoInstance(_leaderboardPopup);
		}
	}
}