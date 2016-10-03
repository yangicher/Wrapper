using System;

namespace Assets.Framework.SocialNetwork.Core
{
    public abstract class AbstractSocialService
    {
        public Action OnCancel { get; set; }

        public Action OnComplete { get; set; }

        public Action OnError { get; set; }

        public abstract void Call();
    }
}