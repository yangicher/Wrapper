using UnityEngine;

namespace Assets.Framework.Logger.Handlers
{
    public sealed class DebugHandler : ILogHandler
    {
        public void Log(LogType type, string message)
        {
            Debug.Log(message);
        }

        public void Warning(LogType type, string message)
        {
            Debug.LogWarning(message);
        }

        public void Error(LogType type, string message)
        {
            Debug.LogError(message);
        }
    }
}
