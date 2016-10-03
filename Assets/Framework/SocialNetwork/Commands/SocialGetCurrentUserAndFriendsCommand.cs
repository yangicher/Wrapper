using Assets.Framework.SocialNetwork.Data;
using Assets.Framework.SocialNetwork.Interfaces;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Commands
{
    public class SocialGetCurrentUserAndFriendsCommand : Command
    {
        [Inject]
        public ISocialServiceApi Social { get; set; }

        public override void Execute()
        {
            Retain();
            Social.GetUserInfo(OnGetUserInfoCompleteHandler, OnGetCurrentUserAndFriendsErrorHandler, OnGetCurrentUserAndFriendsCancelHandler);
        }

        private void OnGetUserInfoCompleteHandler()
        {
            Social.GetUserFriends(OnGetUserFriendsHandler, OnGetCurrentUserAndFriendsErrorHandler, OnGetCurrentUserAndFriendsCancelHandler);
        }

        private void OnGetUserFriendsHandler()
        {
            Release();

            Social.CrossContextDispatcher.Dispatch(SocialEvent.ON_GET_USER_AND_FRIENDS);
        }

        private void OnGetCurrentUserAndFriendsCancelHandler()
        {
            Debug.LogError("OnGetCurrentUserAndFriendsCancelHandler");
            Social.CrossContextDispatcher.Dispatch(SocialEvent.LOGIN_CANCEL);
        }

        private void OnGetCurrentUserAndFriendsErrorHandler()
        {
            Debug.LogError("OnGetCurrentUserAndFriendsErrorHandler");
            Social.CrossContextDispatcher.Dispatch(SocialEvent.LOGIN_ERROR);
        }
    }
}