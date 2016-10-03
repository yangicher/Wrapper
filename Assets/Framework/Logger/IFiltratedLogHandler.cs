namespace Assets.Framework.Logger
{
    interface IFiltratedLogHandler : ILogHandler
    {
        void AddFilter(ILogFilter filter);
    }
}
