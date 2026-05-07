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

        }
    }
}
