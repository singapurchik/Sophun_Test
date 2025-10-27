using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Popup
{
    public class PopupManagerService : IPopupManagerService
    {
	    [Inject] private DiContainer _container;
	    
        private readonly Dictionary<string, GameObject> _popups = new ();

        public async void OpenPopup(string name, object param, Transform parent = null)
        {
            if (_popups.ContainsKey(name))
            {
                Debug.LogError($"Popup with name {name} is already shown");
                return;
            }

            await LoadPopup(name, param, parent);
        }

        public void ClosePopup(string name)
        {
	        if (_popups.TryGetValue(name, out var popup))
	        {
		        Addressables.ReleaseInstance(popup);
		        _popups.Remove(name);   
	        }
        }

        private async Task LoadPopup(string name, object param, Transform parent = null)
        {
	        var handle = Addressables.InstantiateAsync(name, parent);

	        await handle.Task;

	        if (handle.Status != AsyncOperationStatus.Succeeded)
	        {
		        Debug.LogError($"Failed to load Popup with name {name}");
		        return;
	        }

	        var popupObject = handle.Result;
	        _container.InjectGameObject(popupObject);
	        popupObject.SetActive(false);

	        var inits = popupObject.GetComponentsInChildren<IPopupInitialization>(true);
	        
	        for (int i = 0; i < inits.Length; i++)
		        await inits[i].Init(param);

	        popupObject.SetActive(true);

	        _popups.Add(name, popupObject);
        }
    }
}