using Assets.Wrapper.Scripts.Controller;
using strange.extensions.mediation.impl;

namespace Assets.Wrapper.Scripts.Views
{
    public class HudMediator : Mediator
    {
        [Inject]
        public HudView View { get; set; }

        [Inject]
        public CommonSignal.StartGameSignal StartGameSignal { get; set; }

        [Inject]
        public CommonSignal.StopGameSignal StopGameSignal { get; set; }

        [Inject]
        public CommonSignal.ChangeScoreSignal ChangeScoreSignal { get; set; }

        public override void OnRegister()
        {
            View.StartButton.onClick.AddListener(OnStartButtonClickHandler);

            StartGameSignal.AddListener(OnStartGameHadler);
            StopGameSignal.AddListener(OnStopGameHadler);
            ChangeScoreSignal.AddListener(OnChangeScoreHandler);
        }

        private void OnChangeScoreHandler()
        {
            View.UpdateScore();
        }

        public override void OnRemove()
        {
            View.StartButton.onClick.RemoveListener(OnStartButtonClickHandler);

            StartGameSignal.RemoveListener(OnStartGameHadler);
            StopGameSignal.RemoveListener(OnStopGameHadler);
        }

        private void OnStartButtonClickHandler()
        {
            View.StartButton.onClick.RemoveListener(OnStartButtonClickHandler);
            StartGameSignal.Dispatch();
        }

        private void OnStartGameHadler()
        {
            View.SetScore(0);
            View.StartButton.interactable = true;
        }

        private void OnStopGameHadler()
        {
            View.StartButton.onClick.AddListener(OnStartButtonClickHandler);
            View.StartButton.interactable = false;
        }
    }
}
