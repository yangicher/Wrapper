using System;

namespace Assets.Framework.Logger
{
    [Flags]
    public enum LogLevel
    {
        Log = 1,
        Warning = 2,
        Error = 4
    }
}
