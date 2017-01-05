using System.Collections.Generic;
using Assets.Framework.SocialNetwork.Core;
using Assets.Framework.SocialNetwork.Data;
using Assets.Framework.SocialNetwork.Data.Models;
using Facebook.Unity;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace Assets.Framework.SocialNetwork.Facebook.Services
{
    public class FbRequestService : AbstractSocialService
    {
        private IEventDispatcher _dispatcher;
        private RequestVo _requestVo;

        private string _ids = string.Empty;
        private List<string> _sendPackage;
        private const int Limit = 50;

        public FbRequestService(IEventDispatcher dispatcher, RequestVo dataRequestVo)
        {
            _dispatcher = dispatcher;
            _requestVo = dataRequestVo;

            _sendPackage = new List<string>(dataRequestVo.To);
        }

        public override void Call()
        {
            RepeatSendMessage();
        }

        private void RepeatSendMessage()
        {
            List<string> ids = new List<string>(_sendPackage);

            if (_sendPackage.Count > Limit)
            {
                ids = new List<string>(_sendPackage.GetRange(0, Limit));
                _sendPackage.RemoveRange(0, Limit);
            }
            else
            {
                ids = new List<string>(_sendPackage.GetRange(0, _sendPackage.Count));
                _sendPackage.RemoveRange(0, _sendPackage.Count);
            }

            SendRequest(ids);
        }

        private void SendRequest(List<string> idsList)
        {
            _requestVo.To = idsList.ToArray();

            FB.AppRequest(
                _requestVo.Message,
                _requestVo.To,
                _requestVo.Filters,
                null,
                null,
                null,
                _requestVo.Title,
                AppRequestCallback
                );
        }

        private void AppRequestCallback(IAppRequestResult result)
        {
            if (result.Error != null)
            {
                Debug.LogError(result.Error);

                _dispatcher.Dispatch(SocialEvent.REQUEST_ERROR);

                if (OnError != null)
                    OnError();
                return;
            }
            //Debug.Log(result.RawResult);

            // Check response for success - show user a success popup if so
            object obj;
            if (result.ResultDictionary.TryGetValue("cancelled", out obj))
            {
                Debug.Log("Request cancelled");
                _dispatcher.Dispatch(SocialEvent.REQUEST_CANCEL);

                if (OnCancel != null)
                    OnCancel();
            }
            else if (result.ResultDictionary.TryGetValue("request", out obj))
            {
                //Debug.Log("Request sent " + result.ResultDictionary["to"]);

                object toArr;
                if (result.ResultDictionary.TryGetValue("to", out toArr))
                {
                    if (string.IsNullOrEmpty(_ids))
                    {
                        _ids += toArr.ToString();
                    }
                    else
                    {
                        _ids += ", " + toArr;
                    }

                    //Debug.LogError("_sendPackage.Count " + _sendPackage.Count);

                    if (_sendPackage.Count > 0)
                    {
                        RepeatSendMessage();
                    }
                    else
                    {
                        _dispatcher.Dispatch(SocialEvent.REQUEST_SUCCESS, _ids);

                        if (OnComplete != null)
                            OnComplete();
                    }
                }
            }
        }
    }
}