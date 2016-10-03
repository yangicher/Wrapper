using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Data.Models;
using Assets.Framework.SocialNetwork.Interfaces;
using UnityEngine;
#if !UNITY_WEBPLAYER
using System.IO;
#endif

namespace Assets.Framework.SocialNetwork.Managers
{
    public class FriendsManager : IFriendManager
    {
        [Inject]
        public ISocialServiceApi Social { get; set; }

        private IUserVO _user;

        private readonly string _invitedFriendsPath = Application.persistentDataPath + "/InvitedFriends.txt";

        private List<IInvitableUserVo> _invitableFriends;
        private List<IInvitableUserVo> _appFriends = new List<IInvitableUserVo>();
        private List<IInvitableUserVo> _users = new List<IInvitableUserVo>();

        private List<string> _appFriendIds;
        private List<string> _allFriendIds;

        protected static Dictionary<string, IUserVO> _players;

        public List<IInvitableUserVo> InvitableFriends
        {
            get { return _invitableFriends; }
            set { _invitableFriends = value; }
        }

        public List<string> AppFriendsIds
        {
            get
            {
                if (_appFriendIds == null)
                {
                    _appFriendIds = new List<string>();

                    for (int i = 0; i < _appFriends.Count; i++)
                    {
                        _appFriendIds.Add(_appFriends[i].Id);
                    }
                }

                return _appFriendIds;
            }
        }

        public IUserVO User
        {
            get { return _user; }
            set { _user = value; }
        }

        public List<IInvitableUserVo> AppFriends
        {
            get { return _appFriends; }
            set { _appFriends = value; }
        }

        public List<IInvitableUserVo> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        public void UpdateInvitableFriends()
        {
#if !UNITY_WEBPLAYER
            if (File.Exists(_invitedFriendsPath))
            {
                var text = File.ReadAllText(_invitedFriendsPath);

                for (int i = 0; i < InvitableFriends.Count; i++)
                {
                    if (text.Contains(InvitableFriends[i].Name))
                    {
                        InvitableFriends[i].IsInvitedAlready = true;
                    }
                }
            }
#endif
        }

        public void SaveInvitableFriendsNames(string names)
        {
#if !UNITY_WEBPLAYER
            var text = "";
            if (File.Exists(_invitedFriendsPath))
            {
                text = File.ReadAllText(_invitedFriendsPath);
            }

            text += names;

            File.WriteAllText(_invitedFriendsPath, text);
#endif
        }

        public string GetFriendNameById(string friendId)
        {
            for (int i = 0; i < AppFriends.Count; i++)
            {
                if (AppFriends[i].Id == friendId)
                    return AppFriends[i].Name;
            }
            return "No name";
        }
    }
}