using Assets.Framework.SocialNetwork.Core;
using Assets.Framework.SocialNetwork.Facebook.Utils;
using Facebook.Unity;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Facebook.Services
{
    public class FbLoadFriendImageService : AbstractSocialService
    {
        private string _userId;
        private Texture _texture;

        public Texture Texture
        {
            get { return _texture; }
        }

        public FbLoadFriendImageService(string userId)
        {
            _userId = userId;
        }

        public override void Call()
        {
            FB.API(FbGraphUtil.GetPictureQuery(_userId, 128, 128), HttpMethod.GET, delegate (IGraphResult result)
               {
                   if (result.Error != null)
                   {
                       Debug.LogError(result.Error + ": for friend " + _userId);
                       return;
                   }
                   if (result.Texture == null)
                   {
                       Debug.Log("LoadFriendImg: No Texture returned");
                       return;
                   }
                   _texture = result.Texture;
                   if (OnComplete != null)
                       OnComplete();
               });
        }
    }
}