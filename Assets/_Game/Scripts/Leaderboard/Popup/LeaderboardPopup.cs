using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Leaderboard.Item;
using UnityEngine;
using Zenject;
using Popup;

namespace Leaderboard.Popup
{
    public class LeaderboardPopup : MonoBehaviour, IPopupInitialization
    {
        [SerializeField] private LeaderboardPopupView _view;
        [SerializeField] private Transform _content;

        [Inject] private LeaderboardJsonProvider _provider;
        [Inject] private LeaderboardItemsPool _pool;

        private readonly List<LeaderboardItem> _spawned = new();
        private LeaderboardPopupInfo _currentInfo;
        private CancellationTokenSource _cancellationTokenSource;

        private void OnDestroy() => Cleanup();
        
        public Task Init(object param)
        {
            if (param is LeaderboardPopupInfo popupInfo)
                _currentInfo = popupInfo;

            _view.OnCloseButtonClicked += Close;
            _view.AddListeners();

            _cancellationTokenSource = new CancellationTokenSource();
            _ = PopulateAsync(_cancellationTokenSource.Token);
            return Task.CompletedTask;
        }

        private async Task PopulateAsync(CancellationToken cancellationToken)
        {
            var list = await _provider.LoadAsync(cancellationToken);
            
            for (int i = 0; i < list.Count; i++)
            {
                if (cancellationToken.IsCancellationRequested)
	                break;
                
                var item = _pool.Get();
                item.transform.SetParent(_content, false);
                item.Initialize(list[i]);
                _spawned.Add(item);
            }
        }

        private void Cleanup()
        {
	        _cancellationTokenSource?.Cancel();
            _view.OnCloseButtonClicked -= Close;
            _view.RemoveListeners();
            
            for (int i = 0; i < _spawned.Count; i++)
	            _spawned[i].Remove();
            
            _spawned.Clear();
        }
        
        private void Close()
        {
	        Cleanup();
            _currentInfo.OnClose();
        }
    }
}
