using Leaderboard.Popup;
using Leaderboard.Item;
using UnityEngine;
using Zenject;

namespace Leaderboard
{
	public sealed class LeaderboardInstaller : MonoInstaller
	{
		[SerializeField] private LeaderboardView _view;
		[SerializeField] private LeaderboardItemsPool _pool;
		[SerializeField] private string _popupAddress = "LeaderboardPopup";
		
		private readonly Leaderboard _leaderboard = new ();
		
		public override void InstallBindings()
		{
			Container.Bind<ILeaderboardView>().FromInstance(_view).WhenInjectedIntoInstance(_leaderboard);
			Container.BindInstance(_pool).WhenInjectedInto<LeaderboardPopup>();
			Container.Bind<LeaderboardJsonProvider>().AsSingle();
			Container.QueueForInject(_leaderboard);
		}

		public override void Start()
		{
			_leaderboard.Initialize(_popupAddress);
		}
	}
}