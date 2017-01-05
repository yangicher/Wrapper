using System;
using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Core;
using Assets.Framework.SocialNetwork.Data.Statics;
using Facebook.MiniJSON;
using Facebook.Unity;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Facebook.Services
{
    public class FbLoginService : AbstractSocialService
    {
        private static readonly List<string> _readPermissions = new List<string> { "public_profile", "user_friends" };

        public override void Call()
        {
            if (FB.IsLoggedIn)
            {
#if !UNITY_EDITOR
                var aToken = AccessToken.CurrentAccessToken;

                foreach (string perm in aToken.Permissions)
                {
                    if (!_readPermissions.Contains(perm))
                    {
                        FB.LogInWithReadPermissions(_readPermissions, HandleResult);
                        return;
                    }
                }
#endif

                LoggedIn();
            }
            else
            {
                FB.LogInWithReadPermissions(_readPermissions, HandleResult);
            }
        }

        protected void HandleResult(IResult result)
        {
            if (result == null)
            {
                Debug.LogError("Error, result null");
                if (OnError != null)
                    OnError();
                return;
            }

            // Some platforms return the empty string instead of null.
            if (result.Error != null)
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

#if !UNITY_EDITOR
                var aToken = AccessToken.CurrentAccessToken;

                foreach (string perm in aToken.Permissions)
                {
                    if (!_readPermissions.Contains(perm))
                    {
                        if (OnCancel != null)
                            OnCancel();

                        return;
                    }
                }
#endif

                var dict = Json.Deserialize(result.RawResult) as Dictionary<string, object>;
                SocialApiStatics.AccesToken = Convert.ToString(dict["access_token"]);
                SocialApiStatics.UserId = Convert.ToString(dict["user_id"]);
                //SocialApiStatics.ExpirationTimestamp = Convert.ToString(dict["expiration_timestamp"]);

                //Debug.Log("Fb Login Success, responce:\n" + result.RawResult);
                Debug.Log("ExpirationTimestamp \n" + AccessToken.CurrentAccessToken.ExpirationTime);
                LoggedIn();
            }
            else
            {
                Debug.LogWarning("Empty Response");
                if (OnCancel != null)
                    OnCancel();
            }
        }

        private void LoggedIn()
        {
            //FB.GetAppLink(OnResult);
            //FB.Mobile.FetchDeferredAppLinkData(OnResult);

            if (OnComplete != null)
                OnComplete();
        }

        protected void OnResult(IAppLinkResult result)
        {
            if (result == null)
            {
                Debug.LogError("Error, result null");
                return;
            }

            if (!String.IsNullOrEmpty(result.Url))
            {
                Debug.LogError(result.Url);

                //mockurl://testing.url unity

                //var index = (new Uri(result.Url)).Query.IndexOf("request_ids");
                //if (index != -1)
                // ...have the user interact with the friend who sent the request,
                // perhaps by showing them the gift they were given, taking them
                // to their turn in the game with that friend, etc.
            }
        }
    }
}