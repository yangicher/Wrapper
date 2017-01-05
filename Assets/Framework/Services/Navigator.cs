using System;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

namespace Assets.Framework.Services
{
    public class Navigator<T> where T : IScreen
    {
        public Navigator()
        {
            Screens = new Dictionary<Type, T>();
            History = new Stack<T>();
        }

        public Dictionary<Type, T> Screens { get; private set; }

        public Stack<T> History { get; private set; }

        public T GetCurrent()
        {
            return History.Peek();
        }

        public void Register(Type type, Object o)
        {
            Screens.Add(type, (T)o);
        }

        public void GoBack()
        {
            var currentScreen = History.Pop();
            var prevScreen = History.Peek();
            Transition(currentScreen, prevScreen);
        }

        public void Transition(T fromScreen, T toScreen)
        {
            fromScreen.OnUnload(() => 
            {
                (Screens[fromScreen.GetType()] as View).gameObject.SetActive(false);
            });

            (Screens[toScreen.GetType()] as View).gameObject.SetActive(true);
            toScreen.OnLoad();
        }
    }
}
