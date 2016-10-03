using System;
using Assets.Framework.SocialNetwork.Core;

namespace Assets.Framework.SocialNetwork.Managers
{
    public class SocialCommsManager
    {
        private static SocialCommsManager _instance;

		public static SocialCommsManager GetInstance() 
		{
			if (_instance == null)
				_instance = new SocialCommsManager();

			return _instance;
		}

		public void Call(AbstractSocialService service, Action onComplete = null, Action onError = null, Action onCancel = null)
		{
			if (onComplete != null)
				service.OnComplete = onComplete;

			if (onError != null)
				service.OnError = onError;
				
			if (onCancel != null)
				service.OnCancel = onCancel;	

			service.Call();
		}
    }
}