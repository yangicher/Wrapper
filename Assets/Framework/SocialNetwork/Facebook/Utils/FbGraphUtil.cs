using System.Collections.Generic;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Facebook.Utils
{
    // Utility class for useful operations when working with the Graph API
    public class FbGraphUtil : ScriptableObject
    {
        // Generate Graph API query for a user/friend's profile picture
        public static string GetPictureQuery(string facebookID, int? width = null, int? height = null,
            string type = null, bool onlyURL = false)
        {
            string query = string.Format("/{0}/picture", facebookID);
            string param = width != null ? "&width=" + width.ToString() : "";
            param += height != null ? "&height=" + height.ToString() : "";
            param += type != null ? "&type=" + type : "";
            if (onlyURL) param += "&redirect=false";
            if (param != "") query += ("?g" + param);
            return query;
        }

        // Pull out the picture image URL from a JSON user object constructed in FBGraph.GetPlayerInfo() or FBGraph.GetFriends()
        public static string DeserializePictureURL(object userObject)
        {
            // friendObject JSON format in this situation
            // {
            //   "first_name": "Chris",
            //   "id": "10152646005463795",
            //   "picture": {
            //      "data": {
            //          "url": "https..."
            //      }
            //   }
            // }
            var user = userObject as Dictionary<string, object>;

            object pictureObj;
            if (user.TryGetValue("picture", out pictureObj))
            {
                var pictureData = (Dictionary<string, object>)(((Dictionary<string, object>)pictureObj)["data"]);
                return (string)pictureData["url"];
            }
            return null;
        }
    }
}