using Zenject;

namespace Modules.StartStopGameUI
{
    public sealed class StartFinishPopupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<StartFinishUIPopup>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}