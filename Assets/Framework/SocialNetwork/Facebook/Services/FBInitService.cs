using Assets.Framework.Logger;
using Assets.Framework.SocialNetwork.Core;
using UnityEngine;
using Facebook.Unity;

namespace Cov.Client.SocialNetwork.Facebook.Services
{
    public class FbInitService : AbstractSocialService
    {
        private string _appId;

        public FbInitService(string appId)
        {
            _appId = appId;
        }

        public override void Call()
        {
            if (!FB.IsInitialized)
            {
                LogService.Log(this, "Fb Init, appId: " + _appId);

                FB.Init(OnInitComplete, OnHideUnity);
            }
            else
            {
                // Already initialized, signal an app activation App Event
                FB.ActivateApp();
            }
        }

        private void OnInitComplete()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
                //Debug.LogError("FB.IsLoggedIn " + FB.IsLoggedIn);
                //SocialApiStatics.IsLoginIn = FB.IsLoggedIn;
            }
            else
            {
                Debug.LogError("Failed to Initialize the Facebook SDK");
            }
        }

        private void OnHideUnity(bool isGameShown)
        {
            Debug.LogError("Is game shown: " + isGameShown);
        }
    }
}