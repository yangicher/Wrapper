using System;
using Assets.Framework.Services;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Wrapper.Scripts.Views
{
    public class MenuView : ScreenBase
    {
        [SerializeField]
        private Button _startButton;

        public Signal StartClick = new Signal();

        public override void OnLoad(Action onComplete = null)
        {
            base.OnLoad(onComplete);

            _startButton.onClick.AddListener(OnStartClickHandler);
        }

        public override void OnUnload(Action onComplete = null)
        {
            base.OnUnload(onComplete);

            _startButton.onClick.RemoveListener(OnStartClickHandler);
        }

        private void OnStartClickHandler()
        {
            _startButton.onClick.RemoveListener(OnStartClickHandler);

            StartClick.Dispatch();
        }

        public override string GetTitle()
        {
            throw new NotImplementedException();
        }
    }
}
