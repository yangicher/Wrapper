using Assets.Framework.Services;
using Assets.Wrapper.Scripts.Controller;
using strange.extensions.mediation.impl;

namespace Assets.Wrapper.Scripts.Views
{
    public class HudMediator : Mediator
    {
        [Inject]
        public HudView View { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        [Inject]
        public CommonSignal.StartGameSignal StartGameSignal { get; set; }

        [Inject]
        public CommonSignal.StopGameSignal StopGameSignal { get; set; }

        [Inject]
        public CommonSignal.ChangeScoreSignal ChangeScoreSignal { get; set; }

        public override void OnRegister()
        {
            View.PauseButton.onClick.AddListener(OnPauseButtonClickHandler);

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
            View.PauseButton.onClick.RemoveListener(OnPauseButtonClickHandler);

            StartGameSignal.RemoveListener(OnStartGameHadler);
            StopGameSignal.RemoveListener(OnStopGameHadler);
        }

        private void OnPauseButtonClickHandler()
        {
            View.PauseButton.onClick.RemoveListener(OnPauseButtonClickHandler);
            
            StopGameSignal.Dispatch();

            IScreen screen = NavigationService.GetScreen<MenuView>();
            NavigationService.Navigate(screen);
        }

        private void OnStartGameHadler()
        {
            View.SetScore(0);
            View.PauseButton.interactable = false;
        }

        private void OnStopGameHadler()
        {
            View.PauseButton.onClick.AddListener(OnPauseButtonClickHandler);
            View.PauseButton.interactable = true;
        }
    }
}
