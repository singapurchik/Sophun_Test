using System.Threading.Tasks;

namespace Popup
{
    public interface IPopupInitialization
    {
        public Task Init(object param);
    }
}