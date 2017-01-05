using Assets.Framework.Logger;
using Assets.Framework.SocialNetwork.Data;
using Assets.Framework.SocialNetwork.Interfaces;
using strange.extensions.command.impl;

namespace Assets.Framework.SocialNetwork.Commands
{
    public class SocialLoginCommand : Command
    {
        [Inject]
        public ISocialServiceApi Social { get; set; }

        public override void Execute()
        {
            Retain();
			LogService.Log(this, LogType.Authentication, "Social login");
            Social.Login(OnLoginCompleteHandler, OnLoginErrorHandler, OnLoginCancelHandler);
        }

        private void OnLoginCompleteHandler()
        {
			LogService.Log(this, LogType.Authentication, "Social login succeed");
            Social.CrossContextDispatcher.Dispatch(SocialEvent.LOGIN_SUCCESS);
			Release();
        }

        private void OnLoginCancelHandler()
        {
			LogService.Warning(this, LogType.Authentication, "Social login canceled");
            Social.CrossContextDispatcher.Dispatch(SocialEvent.LOGIN_CANCEL);
			Release();
        }

        private void OnLoginErrorHandler()
        {
            LogService.Error(this, LogType.Authentication, "Social login error");
            Social.CrossContextDispatcher.Dispatch(SocialEvent.LOGIN_ERROR);
			Release();
        }
    }
}