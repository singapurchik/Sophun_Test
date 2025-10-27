using Zenject;

namespace Popup
{
	public class PopupInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<IPopupManagerService>().To<PopupManagerService>().AsSingle();
		}
	}
}