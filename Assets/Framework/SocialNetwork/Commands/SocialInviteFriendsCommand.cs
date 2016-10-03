using Assets.Framework.SocialNetwork.Data.Models;
using Assets.Framework.SocialNetwork.Interfaces;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Commands
{
    public class SocialInviteFriendsCommand : Command
    {
        [Inject]
        public ISocialServiceApi Social { get; set; }

        public override void Execute()
        {
            Social.InviteFriends((data as TmEvent).data as RequestVo, OnInviteFriendsCompleteHandler, OnInviteFriendsErrorHandler, OnInviteFriendsCancelHandler);
        }

        private void OnInviteFriendsCompleteHandler()
        {
            //Debug.LogError("OnInviteFriendsCompleteHandler");
        }

        private void OnInviteFriendsCancelHandler()
        {
            Debug.LogError("OnInviteFriendsCancelHandler");
        }

        private void OnInviteFriendsErrorHandler()
        {
            Debug.LogError("OnInviteFriendsErrorHandler");
        }
    }
}