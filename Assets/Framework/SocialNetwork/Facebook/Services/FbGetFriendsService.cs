using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Core;
using Assets.Framework.SocialNetwork.Data.Models;
using Assets.Framework.SocialNetwork.Facebook.Models;
using Assets.Framework.SocialNetwork.Interfaces;
using Facebook.Unity;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Facebook.Services
{
    public class FbGetFriendsService : AbstractSocialService
    {
        private readonly IFriendManager _friendManager;

        public FbGetFriendsService(IFriendManager friendManager)
        {
            _friendManager = friendManager;
        }

        public override void Call()
        {
            string queryString = "";
            queryString = "/me/invitable_friends?fields=name,picture.width(128).height(128)&limit=1000";
            FB.API(queryString, HttpMethod.GET, GetPlayerInfoCallback);
        }

        private void GetPlayerInfoCallback(IGraphResult result)
        {
            if (result == null)
            {
                Debug.LogError("Error, result null");
                if (OnError != null)
                    OnError();
                return;
            }

            if (!string.IsNullOrEmpty(result.Error))
            {
                Debug.LogError("Error, result:\n" + result.Error);
                if (OnError != null)
                    OnError();
            }
            else if (result.Cancelled)
            {
                Debug.LogWarning("Cancelled, responce:\n" + result.RawResult);
                if (OnCancel != null)
                    OnCancel();
            }
            else if (!string.IsNullOrEmpty(result.RawResult))
            {
                object dataList;
                if (result.ResultDictionary.TryGetValue("data", out dataList))
                {
                    var friendsObjList = (List<object>)dataList;
                    var friendsList = new List<IInvitableUserVo>();

                    Debug.LogWarning("FB Invitable friends count: " + friendsObjList.Count);

                    foreach (var o in friendsObjList)
                    {
                        var friendDict = ((Dictionary<string, object>)(o));
                        var friendPicture = ((Dictionary<string, object>)(friendDict["picture"]));

                        var player = new FbUserFriendVO();
                        player.Id = (string)friendDict["id"];
                        player.Name = (string)friendDict["name"];

                        var pictureData = (Dictionary<string, object>) (friendPicture["data"]);
                        player.PhotoUrl = (string) pictureData["url"];
                        player.IsSilhouette = (bool) pictureData["is_silhouette"];

                        friendsList.Add(player);
                    }

                    _friendManager.InvitableFriends = new List<IInvitableUserVo>(friendsList);

                    GetFriends();
                }
                else
                {
                    Debug.LogError("Error " + result.Error);
                    Debug.LogError("Error, result:\n" + result.RawResult);

                    if (OnError != null)
                        OnError();
                }
            }
            else
            {
                Debug.LogWarning("Empty Response");
                if (OnCancel != null)
                    OnCancel();
            }
        }

        private void GetFriends()
        {
            string queryString = "";
            queryString = "/me/friends?fields=name,picture.width(128).height(128)";
            FB.API(queryString, HttpMethod.GET, GetPlayerFriendsInfoCallback);
        }

        private void GetPlayerFriendsInfoCallback(IGraphResult result)
        {
            if (result == null)
            {
                Debug.LogError("Error, result null");
                return;
            }

            if (!string.IsNullOrEmpty(result.RawResult))
            {
                //Debug.Log("Success, responce:\n" + result.RawResult);

                object dataList;
                if (result.ResultDictionary.TryGetValue("data", out dataList))
                {
                    var friendsObjList = (List<object>)dataList;
                    var friendsList = new List<IInvitableUserVo>();

                    Debug.LogWarning("FB Friends count: " + friendsObjList.Count);

                    foreach (var o in friendsObjList)
                    {
                        var friendDict = ((Dictionary<string, object>)(o));
                        var friendPicture = ((Dictionary<string, object>)(friendDict["picture"]));

                        var player = new FbUserFriendVO();
                        player.Id = (string)friendDict["id"];
                        player.Name = (string)friendDict["name"];

                        var pictureData = (Dictionary<string, object>)(friendPicture["data"]);
                        player.PhotoUrl = (string)pictureData["url"];
                        player.IsSilhouette = (bool)pictureData["is_silhouette"];

                        friendsList.Add(player);
                    }

                    //_friendManager.Dispatch(SocialEvent.GET_FRIENDS, friendsList);
                    _friendManager.AppFriends = new List<IInvitableUserVo>(friendsList);

                    if (OnComplete != null)
                        OnComplete();

                }
            }
            else if (string.IsNullOrEmpty(result.Error))
            {
                Debug.LogError("Error, result:\n" + result.Error);
                if (OnError != null)
                    OnError();
            }
            else if (result.Cancelled)
            {
                Debug.LogWarning("Cancelled, responce:\n" + result.RawResult);
                if (OnCancel != null)
                    OnCancel();
            }
            else
            {
                Debug.LogWarning("Empty Response");
            }

        }
    }
}
