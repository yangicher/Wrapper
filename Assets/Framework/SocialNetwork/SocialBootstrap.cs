using Assets.Framework.SocialNetwork.Data;
using Assets.Framework.SocialNetwork.Data.Statics;
using strange.extensions.context.impl;

namespace Assets.Framework.SocialNetwork
{
    public class SocialBootstrap : ContextView
    {
        private string _appId;

        public static SocialBootstrap Instance { get; private set; }

        public SocialNetworkContext SocialNetworkContext
        {
            get { return (SocialNetworkContext)context; }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                context = new SocialNetworkContext(this);

                //TODO: model
                //SocialApiStatics.IsLoginIn = App.Instance.IsSocialConnect;

                SocialNetworkContext.dispatcher.AddListener(SocialEvent.LOGOUT_SUCCESS, OnLogout);
                SocialNetworkContext.dispatcher.AddListener(SocialEvent.LOGIN_ERROR, OnLogout);
                SocialNetworkContext.dispatcher.AddListener(SocialEvent.LOGIN_CANCEL, OnLogout);

                SocialNetworkContext.dispatcher.AddListener(SocialEvent.LOGIN_SUCCESS, OnLogin);

                SocialNetworkContext.dispatcher.Dispatch(SocialEvent.INIT, AppId);
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnLogout()
        {
            //TODO: model
            //App.Instance.IsSocialConnect = false;
            SocialApiStatics.IsLoginIn = false;
        }

        private void OnLogin()
        {
            //TODO: model
            //App.Instance.IsSocialConnect = true;
            SocialApiStatics.IsLoginIn = true;
        }

        void OnApplicationPause(bool pauseStatus)
        {
            // Check the pauseStatus to see if we are in the foreground
            // or background
            if (!pauseStatus)
            {
                SocialNetworkContext.dispatcher.Dispatch(SocialEvent.INIT, AppId);
            }
        }

        private string AppId
        {
            get
            {
                if (string.IsNullOrEmpty(_appId))
                {
                    //TODO: model
                    //_appId = App.Instance.FacebookAppId;
                }

                return _appId;
            }
        }

        protected override void OnDestroy()
        {
            if (SocialNetworkContext != null)
            {
                SocialNetworkContext.dispatcher.RemoveListener(SocialEvent.LOGOUT_SUCCESS, OnLogout);
                SocialNetworkContext.dispatcher.RemoveListener(SocialEvent.LOGIN_ERROR, OnLogout);
                SocialNetworkContext.dispatcher.RemoveListener(SocialEvent.LOGIN_CANCEL, OnLogout);

                SocialNetworkContext.dispatcher.RemoveListener(SocialEvent.LOGIN_SUCCESS, OnLogin);
            }
            base.OnDestroy();
        }
    }
}