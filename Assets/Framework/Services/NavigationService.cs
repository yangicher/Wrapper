using System;
using Assets.Framework.Logger;
using UnityEngine;
using LogType = Assets.Framework.Logger.LogType;

namespace Assets.Framework.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Navigator<ScreenBase> _navigator;

        private Type _bootstrap;

        public NavigationService()
        {
            _navigator = new Navigator<ScreenBase>();
            Init();
            Instance = this;
        }

        public static NavigationService Instance { get; private set; }

        public bool IsMoveForward { get; private set; }

        public Navigator<ScreenBase> Navigator
        {
            get
            {
                return _navigator;
            }
        }

        public void Init(string rootObjectTag = "RootScreenContainer")
        {
            var p = GameObject.FindWithTag(rootObjectTag);
            var screens = p.GetComponentsInChildren<ScreenBase>(true);

            foreach (var screen in screens)
            {
                if (!_navigator.Screens.ContainsKey(screen.GetType()))
                {
                    _navigator.Register(screen.GetType(), screen);
                }   
            }
        }

        public bool CanNavigateTo<T>() where T : class, IScreen
        {
            return CanNavigateTo(typeof(T));
        }

        public void SetBootstrap<T>() where T : class, IScreen
        {
            _bootstrap = typeof(T);
        }

        public void OnBootstrap()
        {
            if (!CanNavigateTo(_bootstrap))
            {
                return;
            }

            var screen = _navigator.Screens[_bootstrap];
            _navigator.History.Push(screen);
            screen.gameObject.SetActive(true);
            screen.OnLoad();
        }

        public T GetScreen<T>() where T : class, IScreen
        {
            return _navigator.Screens[typeof(T)] as T;
        }

        public T GetCurrent<T>() where T : class, IScreen
        {
            return _navigator.GetCurrent() as T;
        }

        public void Navigate<T>(T screen) where T : class,  IScreen
        {
            Navigate(screen, null);
        }

        public void Navigate<T>(T screen, Action<T> action, params object[] parametrs) where T : class,  IScreen
        {
            IsMoveForward = true;
            var nextScreenType = screen.GetType();
            
            if (!CanNavigateTo(screen.GetType()))
            {
                LogService.Log(this, LogType.Core, "Can't navigate to {0}", screen.GetType());
                return;
            }

            var currentScreen = _navigator.GetCurrent();
            var nextScreen = _navigator.Screens[nextScreenType];
            _navigator.History.Push(nextScreen);

            if (action != null && nextScreen is T)
            {
                action(nextScreen as T);
            }

            if (parametrs != null)
            {
                nextScreen.Parametrs = parametrs;
            }

            _navigator.Transition(currentScreen, nextScreen);
        }

        public void GoBack()
        {
            IsMoveForward = false;
            _navigator.GoBack();
        }

        private bool CanNavigateTo(Type type)
        {
            return _navigator.Screens.ContainsKey(type);
        }
    }
}
