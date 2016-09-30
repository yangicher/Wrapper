using System;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Wrapper.Scripts.Views
{
    public class MenuView : View
    {
        [SerializeField]
        private Button _startButton;

        public Signal StartClick = new Signal();

        public void Initialize()
        {
            Debug.Log("init menu");

            _startButton.onClick.AddListener(OnStartClickHandler);
        }

        public void RemoveView()
        {
            Destroy(gameObject);
        }

        private void OnStartClickHandler()
        {
            _startButton.onClick.RemoveListener(OnStartClickHandler);

            StartClick.Dispatch();
        }
    }
}
