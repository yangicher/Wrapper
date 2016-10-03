using System;
using System.Text;

namespace Assets.Framework.Logger
{
    public class TimedLogger : BaseLogger
    {
        private readonly StringBuilder _stringBuilder;

        public TimedLogger()
        {
            _stringBuilder = new StringBuilder();
        }

        protected override string FormattedLog(object context, string message, params object[] args)
        {
            _stringBuilder.Remove(0, _stringBuilder.Length);
            _stringBuilder.Append('[').Append(DateTime.UtcNow.ToLongTimeString()).Append("] ").Append(context.GetType().Name).Append(": ").AppendFormat(message, args);

            return _stringBuilder.ToString();
        }
    }
}
