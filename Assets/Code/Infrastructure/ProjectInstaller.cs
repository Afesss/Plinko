using Zenject;

namespace Code.Infrastructure
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCurrencyService();
        }

        private void BindCurrencyService()
        {
            Container.BindInterfacesAndSelfTo<CurrencyService>()
                .AsSingle()
                .NonLazy();
        }
    }
}
