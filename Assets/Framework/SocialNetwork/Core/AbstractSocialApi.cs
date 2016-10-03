namespace Assets.Framework.SocialNetwork.Core
{
    public abstract class AbstractSocialApi
    {
        protected static AbstractSocialApi _instance;

        public static AbstractSocialApi GetInstance()
        {
            return _instance;
        }
	}
}
