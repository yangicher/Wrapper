using Assets.Framework.SocialNetwork.Data.Models;

namespace Assets.Framework.SocialNetwork.Facebook.Models
{
    public class FbUserFriendVO : IInvitableUserVo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public bool IsSelected { get; set; }

        public bool IsInvitedAlready { get; set; }

        public bool IsSilhouette { get; set; }


        public string GetPhotoUrl()
        {
            if (string.IsNullOrEmpty(PhotoUrl))
            {
                return "https://graph.facebook.com/" + Id + "/picture?type=normal";
            }
            return PhotoUrl;
        }
    }
}