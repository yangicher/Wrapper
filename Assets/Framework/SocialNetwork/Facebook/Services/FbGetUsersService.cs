using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Core;
using Assets.Framework.SocialNetwork.Data.Models;
using Assets.Framework.SocialNetwork.Facebook.Models;
using Assets.Framework.SocialNetwork.Interfaces;
using Facebook.Unity;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Facebook.Services
{
    public class FbGetUsersService : AbstractSocialService
    {
        private readonly IFriendManager _friendManager;
        private readonly List<string> _listIds;

        public FbGetUsersService(IFriendManager friendManager, List<string> ids)
        {
            _friendManager = friendManager;
            _listIds = ids;
        }

        public override void Call()
        {
            string queryString = "/?ids=" + string.Join(",", _listIds.ToArray());
            FB.API(queryString, HttpMethod.GET, OnGetUsersCallback);
        }

        private void OnGetUsersCallback(IGraphResult result)
        {
            if (result == null)
            {
                Debug.LogError("FbGetUsersService, result null");
                if (OnError != null)
                    OnError();
                return;
            }

            if (!string.IsNullOrEmpty(result.RawResult))
            {
                //Debug.Log("Success, responce:\n" + result.RawResult);

                var friendsList = new List<IInvitableUserVo>();

                foreach (var keyValue in result.ResultDictionary)
                {
                    var friendDict = ((Dictionary<string, object>) (keyValue.Value));

                    var player = new FbUserFriendVO();
                    player.Id = (string) friendDict["id"];
                    player.Name = (string) friendDict["name"];

                    friendsList.Add(player);
                }

                _friendManager.Users = new List<IInvitableUserVo>(friendsList);

                if (OnComplete != null)
                    OnComplete();
            }
            else
            {
                if (OnError != null)
                    OnError();
            }

        }
    }
}
