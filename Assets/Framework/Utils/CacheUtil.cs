using System.Collections.Generic;
using UnityEngine;

namespace Assets.Framework.Utils
{
    public static class CacheUtil
    {
        public static char CacheSpliter = '!';

        public static void SaveToCacheNewCard(string unitId, string type)
        {
            if (PlayerPrefs.HasKey(type))
            {
                if (!IsNewCard(unitId, type))
                {
                    string cards = PlayerPrefs.GetString(type);
                    cards += CacheSpliter + unitId;
                    PlayerPrefs.SetString(type, cards);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                PlayerPrefs.SetString(type, unitId);
                PlayerPrefs.Save();
            }
        }

        public static bool IsNewCard(string cardId, string type)
        {
            if (PlayerPrefs.HasKey(type))
            {
                string cards = PlayerPrefs.GetString(type);
                var arr = cards.Split(CacheSpliter);
                List<string> listId = new List<string>();
                foreach (var cId in arr)
                {
                    listId.Add(cId);
                }

                return listId.Contains(cardId);
            }

            return false;
        }

        public static void RemoveCardIdFromCache(string cardId, string type)
        {
            if (PlayerPrefs.HasKey(type))
            {
                string cards = PlayerPrefs.GetString(type);
                var arr = cards.Split(CacheSpliter);
                List<string> listId = new List<string>();
                foreach (var cId in arr)
                {
                    if (cId != cardId)
                        listId.Add(cId);
                }

                if (listId.Count == 0)
                {
                    PlayerPrefs.DeleteKey(type);
                }
                else
                {
                    var str = "";
                    for (int i = 0; i < listId.Count; i++)
                    {
                        str += (i == 0) ? listId[i] : (CacheSpliter + listId[i]);
                    }

                    PlayerPrefs.SetString(type, str);
                    PlayerPrefs.Save();
                }
            }
        }

        public static int GetCount(string type)
        {
            if (PlayerPrefs.HasKey(type))
            {
                string cards = PlayerPrefs.GetString(type);
                var arr = cards.Split(CacheSpliter);
                List<string> listId = new List<string>();
                foreach (var cId in arr)
                {
                    listId.Add(cId);
                }

                return listId.Count;
            }

            return 0;
        }
    }
}