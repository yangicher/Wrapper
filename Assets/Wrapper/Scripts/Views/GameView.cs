using Assets.Framework.Services;
using UnityEngine;

namespace Assets.Wrapper.Scripts.Views
{
    public class GameView : ScreenBase
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

        private float _scaleFromFloor = 0.2f;
        private float _scaleFromCeil = 0.8f;

        private float _scaleToFloor = 1.1f;
        private float _scaleToCeil = 1.4f;

        public void StartGame(int sidesCount, bool rotate)
        {
            _checkImage.SetActive(false);
            _crossImage.SetActive(false);

            _mainPolygon.DrawPolygon(sidesCount);
            _subPolygon.DrawPolygon(sidesCount);

            _mainPolygonTransform.localScale = Vector3.one;
            _subPolygonTransform.localScale = ScaleToVector;

            var scaleProp = new ScaleTweenProperty(ScaleFromVector);

            var scaleConfig = new GoTweenConfig();
            scaleConfig.easeType = (Random.Range(0f, 1f) > 0.5f) ? GoEaseType.SineInOut : GoEaseType.BackInOut;
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

            _canvas.gameObject.SetActive(false);
            _canvas.gameObject.SetActive(true);
        }

        private Vector3 ScaleToVector
        {
            get
            {
                var koef = Random.Range(_scaleFromFloor, _scaleFromCeil);
                return new Vector3(koef, koef, koef);
            }
        }

        private Vector3 ScaleFromVector
        {
            get
            {
                var koef = Random.Range(_scaleToFloor, _scaleToCeil);
                return new Vector3(koef, koef, koef);
            }
        }

        public void StopGame()
        {
            Go.killAllTweensWithTarget(_mainPolygonTransform);
            Go.killAllTweensWithTarget(_subPolygonTransform);
        }

        public bool IsWin()
        {
            var difference = _mainPolygonTransform.localScale.x - _subPolygonTransform.localScale.x;
            return difference > 0 && difference <= _difficult;
        }

        public void ShowResult(bool won)
        {
            _checkImage.SetActive(won);
            _crossImage.SetActive(!won);
        }

        protected override void Awake()
        {
            base.Awake();

            _mainPolygonTransform = _mainPolygon.gameObject.transform;
            _subPolygonTransform = _subPolygon.gameObject.transform;
        }

        public override string GetTitle()
        {
            throw new System.NotImplementedException();
        }
    }
}
