using UnityEngine;

namespace Popup
{
    public interface IPopupManagerService
    {
        public void OpenPopup(string name, object param, Transform parent);
        
        public void ClosePopup(string name);
    }
}