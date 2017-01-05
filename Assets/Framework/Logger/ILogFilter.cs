namespace Assets.Framework.Logger
{
    public interface ILogFilter
    {
        bool Allowed(LogLevel level, LogType type);
    }
}
