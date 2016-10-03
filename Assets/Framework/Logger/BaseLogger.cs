using System.Collections.Generic;
using System.Text;

namespace Assets.Framework.Logger
{
    public class BaseLogger : ILogger
    {
        private readonly StringBuilder _stringBuilder;

        private readonly List<ILogHandler> _handlers;

        private readonly List<ILogFilter> _filters;

        public BaseLogger()
        {
            _stringBuilder = new StringBuilder();
            _handlers = new List<ILogHandler>();
            _filters = new List<ILogFilter>();
        }

        public void Log(object context, string message)
        {
            Log(context, LogLevel.Log, LogType.Debug, message);
        }

        public void Log(object context, LogLevel level, string message)
        {
            Log(context, level, LogType.Debug, message);
        }

        public void Log(object context, LogType type, string message)
        {
            Log(context, LogLevel.Log, type, message);
        }

        public void Log(object context, LogLevel level, LogType type, string message, params object[] args)
        {
            if (!Allowed(level, type))
            {
                return;
            }

            var result = FormattedLog(context, message, args);

            for (int index = 0; index < _handlers.Count; index++)
            {
                if (level == LogLevel.Warning)
                {
                    _handlers[index].Warning(type, result);
                }
                else if (level == LogLevel.Error)
                {
                    _handlers[index].Error(type, result);
                }
                else
                {
                    _handlers[index].Log(type, result);
                }
            }
        }

        public void AddHandler(ILogHandler handler)
        {
            _handlers.Add(handler);
        }

        public void AddFilter(ILogFilter filter)
        {
            _filters.Add(filter);
        }

        protected virtual string FormattedLog(object context, string message, params object[] args)
        {
            _stringBuilder.Remove(0, _stringBuilder.Length);
            _stringBuilder.Append("<").Append(context).Append(">: ").AppendFormat(message, args);

            return _stringBuilder.ToString();
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
