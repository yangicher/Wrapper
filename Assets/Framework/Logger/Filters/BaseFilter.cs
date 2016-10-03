using System.Collections.Generic;

namespace Assets.Framework.Logger.Filters
{
    public class BaseFilter : ILogFilter
    {
        private readonly List<LogPair> _filters;

        public BaseFilter()
        {
            _filters = new List<LogPair>();
        }

        public bool Allowed(LogLevel level, LogType type)
        {
            if (_filters.Exists(pair => pair.Level == level && pair.Type == type))
            {
                return false;
            }

            return true;
        }

        public void AddFilter(LogLevel level, LogType type)
        {
            _filters.Add(new LogPair { Level = level, Type = type });
        }

        private struct LogPair
        {
            public LogLevel Level;

            public LogType Type;
        }
    }
}
