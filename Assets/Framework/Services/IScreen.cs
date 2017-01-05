using System;
using strange.extensions.mediation.api;

namespace Assets.Framework.Services
{
    public interface IScreen : IView
    {
        string GetTitle();

        void OnLoad(Action onComplete = null);

        void OnUnload(Action onComplete = null);
    }
}
