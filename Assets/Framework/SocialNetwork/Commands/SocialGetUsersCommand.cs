using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Data;
using Assets.Framework.SocialNetwork.Interfaces;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

namespace Assets.Framework.SocialNetwork.Commands
{
    public class SocialGetUsersCommand : Command
    {
        [Inject]
        public ISocialServiceApi Social { get; set; }

        public override void Execute()
        {
            Social.GetUsers(new List<string>((List<string>)(data as TmEvent).data), OnGetUsersHandler, OnGetUsersCancelHandler, OnGetUsersErrorHandler);
        }

        private void OnGetUsersHandler()
        {
            Social.Dispatcher.Dispatch(SocialEvent.ON_GET_USERS);
        }

        private void OnGetUsersCancelHandler()
        {
            //Social.CrossContextDispatcher.Dispatch(SocialEvent.ON_GET_USERS);
        }

        private void OnGetUsersErrorHandler()
        {
            //Social.CrossContextDispatcher.Dispatch(SocialEvent.ON_GET_USERS);
        }
    }
}
