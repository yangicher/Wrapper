using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Wrapper.Scripts.Views
{
    public class HudView : View
    {
        [SerializeField]
        private Button _startButton;

        [SerializeField]
        private Text _scoreText;

        public Button StartButton
        {
            get { return _startButton; }
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void UpdateScore()
        {
            _scoreText.text = (int.Parse(_scoreText.text) + 1).ToString();
        }
    }
}
