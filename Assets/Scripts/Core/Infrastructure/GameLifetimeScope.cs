using Core.Network.Client;
using Core.Network.Demo;
using Core.Network.Hosting;
using Core.Network.Interfaces;
using Core.Network.Services;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure
{
    /// <summary>
    /// Корневой контейнер для регистрации зависимостей приложения.
    /// </summary>
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<ISubscriptionManager, SubscriptionManager>(Lifetime.Singleton);
            builder.Register<INetworkMessageService, NetworkMessageService>(Lifetime.Singleton);
            builder.Register<IClientSubscriptionService, ClientSubscriptionService>(Lifetime.Singleton);

            builder.Register<ServerMessageHost>(Lifetime.Singleton);
            builder.Register<HelloMessageDemo>(Lifetime.Singleton);
            builder.Register<HelloMessageHandler>(Lifetime.Singleton);

            builder.RegisterEntryPoint<NetworkEntryPoint>();
        }
    }
}
