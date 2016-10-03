using Assets.Framework.Services;
using Assets.Wrapper.Scripts.Views;
using strange.extensions.command.impl;

namespace Assets.Wrapper.Scripts.Controller.Commands
{
    public class StartCommand : Command
    {
        [Inject]
        public INavigationService NavigationService { get; set; }

        public override void Execute()
        {
            NavigationService.SetBootstrap<MenuView>();
            NavigationService.OnBootstrap();
        }
    }
}
