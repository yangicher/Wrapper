namespace Assets.Framework.SocialNetwork.Data.Models
{
    public interface IInvitableUserVo : IUserVO
    {
        bool IsSelected { get; set; }
        bool IsInvitedAlready { get; set; }
        bool IsSilhouette { get; set; }

        string GetPhotoUrl();
    }
}