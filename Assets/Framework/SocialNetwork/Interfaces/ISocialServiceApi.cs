using System;
using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Data.Models;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Interfaces
{
    public interface ISocialServiceApi
    {
        IEventDispatcher Dispatcher { get; set; }

        IEventDispatcher CrossContextDispatcher { get; set; }

        void Init(string appId);

        void Login(Action onComplete, Action onError, Action onCancel);

        void GetUserInfo(Action onComplete, Action onError, Action onCancel);

        void GetUserFriends(Action onComplete, Action onError, Action onCancel);

        void InviteFriends(RequestVo data, Action onComplete, Action onError, Action onCancel);

        void LoadFriendImgById(string id, Action<Texture> callback);

        void GetUsers(List<string> ids, Action onComplete, Action onError, Action onCancel);

        void Logout();
    }
}