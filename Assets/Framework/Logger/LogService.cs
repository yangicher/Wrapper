using Assets.Framework.Logger.Handlers;

namespace Assets.Framework.Logger
{
    public static class LogService
    {
        static LogService()
        {
            Logger = new TimedLogger();
#if HOP_DEBUG_MODE || UNITY_EDITOR
            Logger.AddHandler(new DebugHandler());
#endif
        }

        private static ILogger Logger { get; set; }

        public static void Log(object context, string message)
        {
            Logger.Log(context, LogLevel.Log, message);
        }

        public static void Log(object context, LogType type, string message)
        {
            Logger.Log(context, LogLevel.Log, type, message);
        }

        public static void Log(object context, LogType type, string message, params object[] args)
        {
            Logger.Log(context, LogLevel.Log, type, message, args);
        }

        public static void Warning(object context, string message)
        {
            Logger.Log(context, LogLevel.Warning, message);
        }

        public static void Warning(object context, LogType type, string message)
        {
            Logger.Log(context, LogLevel.Warning, type, message);
        }

        public static void Warning(object context, LogType type, string message, params object[] args)
        {
            Logger.Log(context, LogLevel.Warning, type, message, args);
        }

        public static void Error(object context, string message)
        {
            Logger.Log(context, LogLevel.Error, message);
        }

        public static void Error(object context, LogType type, string message)
        {
            Logger.Log(context, LogLevel.Error, type, message);
        }

        public static void Error(object context, LogType type, string message, params object[] args)
        {
            Logger.Log(context, LogLevel.Error, type, message, args);
        }
        
        public static void AddHandler(ILogHandler handler)
        {
            Logger.AddHandler(handler);
        }

        public static void AddFilter(ILogFilter filter)
        {
            Logger.AddFilter(filter);
        }
    }
}
