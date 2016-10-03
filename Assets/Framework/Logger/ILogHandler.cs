namespace Assets.Framework.Logger
{
    public interface ILogHandler
    {
        void Log(LogType type, string message);

        void Warning(LogType type, string message);

        void Error(LogType type, string message);
    }
}
