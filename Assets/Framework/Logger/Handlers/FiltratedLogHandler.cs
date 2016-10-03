using System.Collections.Generic;

namespace Assets.Framework.Logger.Handlers
{
    public class FiltratedLogHandler : IFiltratedLogHandler
    {
        private readonly ILogHandler _logHandler;

        private readonly List<ILogFilter> _filters;

        public FiltratedLogHandler(ILogHandler logHandler)
        {
            _logHandler = logHandler;
            _filters = new List<ILogFilter>();
        }

        public void Log(LogType type, string message)
        {
            if (!Allowed(LogLevel.Log, type))
            {
                return;
            }

            _logHandler.Log(type, message);
        }

        public void Warning(LogType type, string message)
        {
            if (!Allowed(LogLevel.Warning, type))
            {
                return;
            }

            _logHandler.Log(type, message);
        }

        public void Error(LogType type, string message)
        {
            if (!Allowed(LogLevel.Error, type))
            {
                return;
            }

            _logHandler.Log(type, message);
        }

        public void AddFilter(ILogFilter filter)
        {
            _filters.Add(filter);   
        }

        private bool Allowed(LogLevel level, LogType type)
        {
            for (int index = 0; index < _filters.Count; index++)
            {
                if (!_filters[index].Allowed(level, type))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
