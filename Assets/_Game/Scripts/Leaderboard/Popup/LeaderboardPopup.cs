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
        private CancellationTokenSource _cts;

        public Task Init(object param)
        {
            if (param is LeaderboardPopupInfo popupInfo)
                _currentInfo = popupInfo;

            _view.OnCloseButtonClicked += Close;
            _view.AddListeners();

            _cts = new CancellationTokenSource();
            _ = PopulateAsync(_cts.Token);
            return Task.CompletedTask;
        }

        private async Task PopulateAsync(CancellationToken ct)
        {
            var list = await _provider.LoadAsync(ct);
            
            for (int i = 0; i < list.Count; i++)
            {
                if (ct.IsCancellationRequested) break;
                var item = _pool.Get();
                item.transform.SetParent(_content, false);
                item.Initialize(list[i]);
                _spawned.Add(item);
            }
        }

        private void Close()
        {
            _view.OnCloseButtonClicked -= Close;
            _view.RemoveListeners();

            for (int i = 0; i < _spawned.Count; i++)
                _spawned[i].Remove();
            _spawned.Clear();

            _cts?.Cancel();
            _currentInfo.OnClose();
        }

        private void OnDestroy()
        {
            _cts?.Cancel();
            _view.OnCloseButtonClicked -= Close;
            _view.RemoveListeners();

            for (int i = 0; i < _spawned.Count; i++)
                _spawned[i].Remove();
            _spawned.Clear();
        }
    }
}
