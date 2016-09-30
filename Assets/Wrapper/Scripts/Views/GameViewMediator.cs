using System.Collections;
using Assets.Wrapper.Scripts.Controller;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Wrapper.Scripts.Views
{
    public class GameViewMediator : Mediator
    {
        [Inject]
        public GameView View { get; set; }

        [Inject]
        public CommonSignal.StartGameSignal StartGameSignal { get; set; }

        [Inject]
        public CommonSignal.StopGameSignal StopGameSignal { get; set; }

        [Inject]
        public CommonSignal.ChangeScoreSignal ChangeScoreSignal { get; set; }

        private bool _gameStarted;

        public override void OnRegister()
        {
            //StartGameSignal.AddListener(OnGameStartHandler);

            _gameStarted = true;
            View.StartGame(3, true);
        }

        public override void OnRemove()
        {
            //StartGameSignal.RemoveListener(OnGameStartHandler);
        }

        public void Update()
        {
            if (!_gameStarted)
            {
                return;    
            }

            if (Input.touchCount > 0)
            {
                int touchCount = Input.touchCount;

                for (int touchIndex = 0; touchIndex < touchCount; ++touchIndex)
                {
                    Touch touch = Input.GetTouch(touchIndex);
                    if (touch.phase == TouchPhase.Began)
                    {
                        OnInputHandler();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnInputHandler();
            }
        }

        private void OnInputHandler()
        {
            StopGameSignal.Dispatch();
            View.StopGame();

            if (View.IsWin())
            {
                ChangeScoreSignal.Dispatch();
                View.ShowResult(true);
            }
            else
            {
                View.ShowResult(false);
            }

            StartCoroutine(ShowResult());
        }

        private IEnumerator ShowResult()
        {
            yield return new WaitForSeconds(0.5f);

            View.StartGame(Random.Range(3, 8), true);
        }
    }
}
