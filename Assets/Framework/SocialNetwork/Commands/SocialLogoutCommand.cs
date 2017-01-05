using Assets.Framework.SocialNetwork.Data;
using Assets.Framework.SocialNetwork.Interfaces;
using strange.extensions.command.impl;

namespace Assets.Framework.SocialNetwork.Commands
{
    public class SocialLogoutCommand : Command
    {
        [Inject]
        public ISocialServiceApi Social { get; set; }

        public override void Execute()
        {
            Social.Logout();

            Social.CrossContextDispatcher.Dispatch(SocialEvent.LOGOUT_SUCCESS);
        }
    }
}
