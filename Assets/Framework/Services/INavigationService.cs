using System;

namespace Assets.Framework.Services
{
    public interface INavigationService
    {
        Navigator<ScreenBase> Navigator { get; }

        // TODO: remake.
        bool IsMoveForward { get; }

        void Navigate<T>(T screen) where T : class, IScreen;

        void Navigate<T>(T screen, Action<T> action, params object[] parametrs) where T : class, IScreen;

        bool CanNavigateTo<T>() where T : class, IScreen;

        void SetBootstrap<T>() where T : class, IScreen;

        void OnBootstrap();

        void GoBack();

        T GetScreen<T>() where T : class, IScreen;
    }
}
