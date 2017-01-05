using System;
using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Data.Models;
using Assets.Framework.SocialNetwork.Facebook.Services;
using Assets.Framework.SocialNetwork.Interfaces;
using Assets.Framework.SocialNetwork.Managers;
using Cov.Client.SocialNetwork.Facebook.Services;
using Facebook.Unity;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Facebook
{
    public class FacebookSocialApi : ISocialServiceApi
    {
        [Inject]
        public IEventDispatcher Dispatcher { get; set; }

        [Inject]
        public IFriendManager FriendManager { get; set; }

        [Inject(ContextKeys.CROSS_CONTEXT_DISPATCHER)]
        public IEventDispatcher CrossContextDispatcher { get; set; }

        public void Init(string appId)
        {
            SocialCommsManager.GetInstance().Call(new FbInitService(appId));
        }

        public void Login(Action onComplete, Action onError, Action onCancel)
        {
            SocialCommsManager.GetInstance().Call(new FbLoginService(), onComplete, onError, onCancel);
        }

        public void GetUserInfo(Action onComplete, Action onError, Action onCancel)
        {
            SocialCommsManager.GetInstance().Call(new FbGetUserService(FriendManager), onComplete, onError, onCancel);
        }

        public void GetUserFriends(Action onComplete, Action onError, Action onCancel)
        {
            SocialCommsManager.GetInstance().Call(new FbGetFriendsService(FriendManager), onComplete, onError, onCancel);
        }

        public void InviteFriends(RequestVo data, Action onComplete, Action onError, Action onCancel)
        {
            SocialCommsManager.GetInstance().Call(new FbRequestService(Dispatcher, data), onComplete, onError, onCancel);
        }

        public void GetUsers(List<string> ids, Action onComplete, Action onError, Action onCancel)
        {
            SocialCommsManager.GetInstance().Call(new FbGetUsersService(FriendManager, ids), onComplete, onError, onCancel);
        }

        public void Logout()
        {
            FB.LogOut();
        }

        public void LoadFriendImgById(string id, Action<Texture> callback)
        {
            var service = new FbLoadFriendImageService(id);
            SocialCommsManager.GetInstance().Call(service, () =>
            {
                callback(service.Texture);
            });
        }
    }
}
