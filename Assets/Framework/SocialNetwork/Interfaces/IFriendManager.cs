using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Data.Models;

namespace Assets.Framework.SocialNetwork.Interfaces
{
    public interface IFriendManager
    {
        List<IInvitableUserVo> InvitableFriends { get; set; }

        List<string> AppFriendsIds { get; }

        List<IInvitableUserVo> AppFriends { get; set; }

        List<IInvitableUserVo> Users { get; set; }

        IUserVO User { get; set; }

        void UpdateInvitableFriends();

        void SaveInvitableFriendsNames(string names);

        string GetFriendNameById(string friendId);
    }
}
