using UnityEngine;
using strange.extensions.mediation.impl;

namespace Assets.Wrapper.Scripts.Views
{
    public class GameView : View
    {
        [SerializeField]
        private float _difficult = 0.05f;

        [SerializeField]
        private UIPolygon _mainPolygon;

        [SerializeField]
        private UIPolygon _subPolygon;

        [SerializeField]
        private GameObject _checkImage;

        [SerializeField]
        private GameObject _crossImage;

        [SerializeField]
        private Canvas _canvas;

        private Transform _mainPolygonTransform;
        private Transform _subPolygonTransform;

        private float _rotationSpeed = 3f;
        private float _scaleSpeed = 1.5f;

        public void StartGame(int sidesCount, float startScale, bool rotate)
        {
            _checkImage.SetActive(false);
            _crossImage.SetActive(false);

            _mainPolygon.DrawPolygon(sidesCount);
            _subPolygon.DrawPolygon(sidesCount);

            _canvas.gameObject.SetActive(false);
            _canvas.gameObject.SetActive(true);

            _mainPolygonTransform.localScale = Vector3.one;
            _subPolygonTransform.localScale = new Vector3(startScale, startScale, 1f);

            var scaleProp = new ScaleTweenProperty(new Vector3(1.1f, 1.1f, 1.1f), true);

            var scaleConfig = new GoTweenConfig();
            scaleConfig.addTweenProperty(scaleProp);
            scaleConfig.loopType = GoLoopType.PingPong;
            scaleConfig.setIterations(-1);
            Go.addTween(new GoTween(_subPolygonTransform, _scaleSpeed, scaleConfig));

            if (rotate)
            {
                var degree = (Random.Range(0f, 1f) > 0.5f) ? 360f : -360f;
                var rotateProp = new RotationTweenProperty(new Vector3(0f, 0f, degree), true);

                var rotateConfig = new GoTweenConfig();
                rotateConfig.addTweenProperty(rotateProp);
                rotateConfig.setIterations(-1);

                Go.addTween(new GoTween(_mainPolygonTransform, _rotationSpeed, rotateConfig));
                Go.addTween(new GoTween(_subPolygonTransform, _rotationSpeed, rotateConfig));
            }
        }

        public void StopGame()
        {
            Go.killAllTweensWithTarget(_mainPolygonTransform);
            Go.killAllTweensWithTarget(_subPolygonTransform);
        }

        public bool IsWin()
        {
            var difference = _subPolygonTransform.localScale.x - _mainPolygonTransform.localScale.x;
            return Mathf.Abs(difference) <= _difficult;
        }

        public void ShowResult(bool won)
        {
            _checkImage.SetActive(won);
            _crossImage.SetActive(!won);
        }

        protected override void Start()
        {
            base.Start();

            _mainPolygonTransform = _mainPolygon.gameObject.transform;
            _subPolygonTransform = _subPolygon.gameObject.transform;
        }
    }
}
