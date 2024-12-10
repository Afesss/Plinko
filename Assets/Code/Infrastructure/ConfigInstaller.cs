using UnityEngine;
using Zenject;

namespace Code.Infrastructure
{
    [CreateAssetMenu(fileName = "ConfigInstaller", menuName = "Installers/ConfigInstaller")]
    public class ConfigInstaller : ScriptableObjectInstaller<ConfigInstaller>
    {
        public Config Config;
        public override void InstallBindings()
        {
            Container.BindInstance(Config);
        }
    }
}
