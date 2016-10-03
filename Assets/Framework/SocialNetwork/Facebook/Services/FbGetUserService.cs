using Assets.Framework.SocialNetwork.Core;
using Assets.Framework.SocialNetwork.Facebook.Models;
using Assets.Framework.SocialNetwork.Interfaces;
using Facebook.Unity;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Facebook.Services
{
    public class FbGetUserService : AbstractSocialService
    {
        private readonly IFriendManager _friendManager;

        public FbGetUserService(IFriendManager friendManager)
        {
            _friendManager = friendManager;
        }

        public override void Call()
        {
            if (!FB.IsLoggedIn)
            {
                if (OnError != null)
                    OnError();

                return;
            }

            string queryString = "/me";
            FB.API(queryString, HttpMethod.GET, GetPlayerInfoCallback);
        }

        private void GetPlayerInfoCallback(IGraphResult result)
        {
            if (result == null)
            {
                Debug.LogError("Error, result null");
                if (OnError != null)
                    OnError();
                return;
            }

            if (!string.IsNullOrEmpty(result.Error))
            {
                Debug.LogError("Error, result:\n" + result.Error);
                if (OnError != null)
                    OnError();
            }
            else if (result.Cancelled)
            {
                Debug.LogWarning("Cancelled, responce:\n" + result.RawResult);
                if (OnCancel != null)
                    OnCancel();
            }
            else if (!string.IsNullOrEmpty(result.RawResult))
            {
                var user = new FbUserFriendVO();
                user.Id = (string)result.ResultDictionary["id"];
                user.Name = (string)result.ResultDictionary["name"];

                _friendManager.User = user;

                if (OnComplete != null)
                    OnComplete();
            }
            else 
            {
                Debug.LogWarning("Empty Response");
                if (OnCancel != null)
                    OnCancel();
            }

        }
    }
}
