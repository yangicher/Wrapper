namespace Assets.Framework.Logger
{
    public interface ILogger
    {
        void Log(object context, string message);

        void Log(object context, LogLevel level, string message);

        void Log(object context, LogType type, string message);

        void Log(object context, LogLevel level, LogType type, string message, params object[] args);

        void AddHandler(ILogHandler handler);

        void AddFilter(ILogFilter filter);
    }
}
