using System.Reflection;
using Assets.Framework.Logger;
using UnityEngine;
using LogType = Assets.Framework.Logger.LogType;

namespace Assets.Framework.Singleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        private static bool _isQuitting;

        private GameObject _cachedGameObject;

        public static T Instance
        {
            get
            {
                if (_isQuitting || !Application.isPlaying)
                {
                    LogService.Warning(typeof(T), LogType.Core, "Instance already destroyed on application quit. Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();

                        if (FindObjectsOfType<T>().Length > 1)
                        {
                            LogService.Error(typeof(T), LogType.Core, "Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            CreateInstance();
                        }
                        else
                        {
                            LogService.Log(typeof(T), LogType.Core, "Using instance already created: {0}", _instance.gameObject.name);
                        }
                    }

                    return _instance;
                }
            }
        }

        public static bool CanUse
        {
            get
            {
                return !_isQuitting;
            }
        }

        public GameObject GameObject
        {
            get
            {
                return _cachedGameObject ?? (_cachedGameObject = gameObject);
            }
        }

        protected virtual void OnAwake()
        {   
        }

        protected virtual void OnUpdate()
        {   
        }

        protected virtual void OnReleaseResource()
        {   
        }

        private static void CreateInstance()
        {
            string prefabPath = null;

            MemberInfo info = typeof(T);
            var attributes = info.GetCustomAttributes(true);
            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i] is SingletonPrefabAttribute)
                {
                    prefabPath = (attributes[i] as SingletonPrefabAttribute).PrefabPath;
                }
            }

            if (string.IsNullOrEmpty(prefabPath))
            {
                var singleton = new GameObject();
                _instance = singleton.AddComponent<T>();
            }
            else
            {
                var prefab = Resources.Load<T>(prefabPath);
                _instance = Instantiate(prefab);
            }

            _instance.gameObject.name = typeof(T) + "(Singleton)";
            DontDestroyOnLoad(_instance.gameObject);

            LogService.Log(typeof(T), LogType.Core, "[Singleton] An instance is needed in the scene, so {0} was created with DontDestroyOnLoad.", _instance.gameObject);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);

                LogService.Warning(typeof(T), LogType.Core, "[Singleton] Another instance of {0} on scene will be destroyed!", name);

                return;
            }

            OnAwake();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void OnDestroy()
        {
            OnReleaseResource();
        }

        private void OnApplicationQuit()
        {
            _isQuitting = true;
        }
    }
}
