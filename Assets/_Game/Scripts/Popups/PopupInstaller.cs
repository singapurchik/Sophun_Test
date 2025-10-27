using Zenject;

namespace Popup
{
	public class PopupInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<IPopupManagerService>().To<PopupManagerServiceService>().AsSingle();
		}
	}
}