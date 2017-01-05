using Assets.Framework.SocialNetwork.Commands;
using Assets.Framework.SocialNetwork.Data;
using Assets.Framework.SocialNetwork.Facebook;
using Assets.Framework.SocialNetwork.Interfaces;
using Assets.Framework.SocialNetwork.Managers;
using strange.extensions.context.impl;
using UnityEngine;

namespace Assets.Framework.SocialNetwork
{
    public class SocialNetworkContext : MVCSContext
    {
        public SocialNetworkContext (MonoBehaviour view) : base(view)
		{
		}

        protected override void mapBindings()
        {
            base.mapBindings();

            crossContextBridge.Bind<ISocialServiceApi>();
            crossContextBridge.Bind<IFriendManager>();

            commandBinder.Bind(SocialEvent.INIT).To<SocialInitCommand>();
            commandBinder.Bind(SocialEvent.LOGIN).To<SocialLoginCommand>();
            commandBinder.Bind(SocialEvent.GET_USER_AND_FRIENDS).To<SocialGetCurrentUserAndFriendsCommand>();
            commandBinder.Bind(SocialEvent.LOGOUT).To<SocialLogoutCommand>();
            commandBinder.Bind(SocialEvent.SEND_REQUEST).To<SocialInviteFriendsCommand>();
            commandBinder.Bind(SocialEvent.GET_USERS).To<SocialGetUsersCommand>();

            //fb
            injectionBinder.Bind<ISocialServiceApi>().To<FacebookSocialApi>().ToSingleton().CrossContext();
            injectionBinder.Bind<IFriendManager>().To<FriendsManager>().ToSingleton().CrossContext();
        }

        public override void Launch()
        {
            //dispatcher.Dispatch(SocialEvent.INIT);
        }
    }
}