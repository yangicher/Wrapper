using System;

namespace Assets.Framework.Singleton
{
    public sealed class SingletonPrefabAttribute : Attribute
    {
        private readonly string _prefabPath;

        public SingletonPrefabAttribute(string prefabPath)
        {
            _prefabPath = prefabPath;
        }

        public string PrefabPath
        {
            get
            {
                return _prefabPath;
            }
        }
    }
}
