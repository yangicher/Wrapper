using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Framework.Logger.Handlers
{
    public sealed class LogglyHandler : ILogHandler
    {
        private const string Url = "http://logs-01.loggly.com/inputs/111cf7fa-5ade-4c53-ada4-3593583148bf/tag/Wrapper";

        private readonly string _sessionDateTimeStamp;

        private readonly string _deviceId;

        private readonly string _deviceModel;

        private readonly string _environment;

        private readonly string _bundleVersionName;

        private readonly IDictionary<UnityEngine.LogType, string> _logTypeMapping;

        public LogglyHandler(string deviceId, string deviceModel, string environment, string bundleVersionName)
        {
            _logTypeMapping = new Dictionary<UnityEngine.LogType, string>
            {
                { UnityEngine.LogType.Log, UnityEngine.LogType.Log.ToString() },
                { UnityEngine.LogType.Warning, UnityEngine.LogType.Warning.ToString() },
                { UnityEngine.LogType.Error, UnityEngine.LogType.Error.ToString() }
            };

            _deviceId = deviceId;
            _deviceModel = deviceModel;
            _environment = environment;
            _bundleVersionName = bundleVersionName;

            _sessionDateTimeStamp = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss");
        }

        public void Log(LogType type, string message)
        {
            var data = new WWWForm();

            data.AddField("LEVEL", _logTypeMapping[UnityEngine.LogType.Log]);
            data.AddField("Message", message);
            
            SendMessage(data);
        }

        public void Warning(LogType type, string message)
        {
            var data = new WWWForm();

            data.AddField("LEVEL", _logTypeMapping[UnityEngine.LogType.Warning]);
            data.AddField("Message", message);

            SendMessage(data);
        }

        public void Error(LogType type, string message)
        {
            var data = new WWWForm();

            data.AddField("LEVEL", _logTypeMapping[UnityEngine.LogType.Error]);
            data.AddField("Message", message);

            SendMessage(data);
        }

        private void SendMessage(WWWForm data)
        {
            data.AddField("Environment", _environment);
			var stackTrace = StackTraceUtility.ExtractStackTrace ();
			if(!string.IsNullOrEmpty(stackTrace))
				data.AddField("Stack_Trace", StackTraceUtility.ExtractStackTrace());
            data.AddField("DeviceId", _deviceId);
            data.AddField("SessionDateTime", _sessionDateTimeStamp);
            data.AddField("Device_Model", _deviceModel);
            data.AddField("Version", _bundleVersionName);

            if (GlobalCoroutineRunner.CanUse)
            {
                GlobalCoroutineRunner.Instance.StartCoroutine(SendData(data));
            }
        }

        public IEnumerator SendData(WWWForm data)
        {
            var sendLog = new WWW(Url, data);

            yield return sendLog;
        }
    }
}
