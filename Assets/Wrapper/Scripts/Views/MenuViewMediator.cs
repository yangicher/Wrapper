using Assets.Framework.Services;
using strange.extensions.mediation.impl;

namespace Assets.Wrapper.Scripts.Views
{
    public class MenuViewMediator : Mediator
    {
        [Inject]
        public MenuView View { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public override void OnRegister()
        {
            View.StartClick.AddListener(OnStartClickHandler);
        }

        public override void OnRemove()
        {
            View.StartClick.RemoveListener(OnStartClickHandler);
        }

        private void OnStartClickHandler()
        {
            IScreen screen = NavigationService.GetScreen<GameView>();
            NavigationService.Navigate(screen);
        }
    }
}
