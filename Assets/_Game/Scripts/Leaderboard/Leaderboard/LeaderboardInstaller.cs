using UnityEngine;
using Zenject;

namespace Leaderboard
{
	public sealed class LeaderboardInstaller : MonoInstaller
	{
		[SerializeField] private LeaderboardView _view;
		[SerializeField] private string _popupAddress = "LeaderboardPopup";
		
		private readonly Leaderboard _leaderboard = new ();
		
		public override void InstallBindings()
		{
			Container.Bind<ILeaderboardView>().FromInstance(_view).WhenInjectedIntoInstance(_leaderboard);
			Container.Bind<LeaderboardJsonProvider>().AsSingle();
			Container.QueueForInject(_leaderboard);
		}

		public override void Start()
		{
			_leaderboard.Initialize(_popupAddress);
		}
	}
}