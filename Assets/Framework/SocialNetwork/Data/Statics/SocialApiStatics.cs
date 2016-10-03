namespace Assets.Framework.SocialNetwork.Data.Statics
{
    public class SocialApiStatics
    {
        public static bool IsLoginIn;
        public static string UserId;
        public static string AccesToken;
        public static string ExpirationTimestamp;

        public static string GetFbPhotoUrl(string id)
        {
            return "https://graph.facebook.com/" + id + "/picture?type=normal";
        }
    }
}