using System;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.Framework.Services
{
    public abstract class ScreenBase : View, IScreen
    {
        [Inject]
        public INavigationService NavigationService { get; set; }

        private Vector3 _initPosition;
        private float _height;

        internal Signal RequestDataSignal = new Signal();

        private float _animationTime = .4f;
        private GoEaseType _easeType = GoEaseType.ExpoOut;

        internal object[] Parametrs { get; set; }

        public abstract string GetTitle();

        protected override void Awake()
        {
            base.Awake();
            _initPosition = transform.localPosition;
            var rect = gameObject.GetComponent<RectTransform>();
            if (rect != null)
            {
                _height = rect.rect.height;
            }
        }

        public virtual void OnLoad(Action onComplete = null)
        {
            //Debug.Log(this.GetType().Name + " OnLoad()");
            //Debug.Log("Current Screen: " + NavigationService.Navigator.GetCurrent().GetType().Name);
            RequestDataSignal.Dispatch();

//            if (this is IScreenOnMap)
//            {
//                MoveDown(false);
//                //MoveUp(true, onComplete);
//                bool isAnimated = NavigationService.IsMoveForward;
//                MoveUp(isAnimated, onComplete);
//            }
//            else
//            {
//                if (onComplete != null) onComplete();
//            }

            if (onComplete != null) onComplete();
        }

        public virtual void OnUnload(Action onComplete = null)
        {
            //Debug.Log(this.GetType().Name + " OnUnload()");
            //Debug.Log("Current Screen: " + NavigationService.Navigator.GetCurrent().GetType().Name);
//            if (this is IScreenOnMap)
//            {
//                MoveDown(true, onComplete);
//            }
//            else
//            {
//                if (onComplete != null) onComplete();
//            }

            if (onComplete != null) onComplete();
        }

        protected void MoveDown(bool animated, Action onComplete = null)
        {
            //Debug.Log("MoveDown() animated: " + animated);
            var pos = _initPosition - new Vector3(0, _height, 0);
            onComplete = onComplete ?? delegate { };
            if (animated)
            {
                Go.to(transform, _animationTime, new GoTweenConfig().localPosition(pos).setEaseType(_easeType).onComplete(tween => onComplete()));
            }
            else
            {
                transform.localPosition = pos;
                onComplete();
            }
        }

        protected void MoveUp(bool animated, Action onComplete = null)
        {
            //Debug.Log("MoveUp() animated: " + animated);
            onComplete = onComplete ?? delegate { };
            if (animated)
            {
                Go.to(transform, _animationTime, new GoTweenConfig().localPosition(_initPosition).setEaseType(_easeType).onComplete(tween => onComplete()));
            }
            else
            {
                transform.localPosition = _initPosition;
                onComplete();
            }
        }

        internal virtual void UpdateScreen()
        {
            //override if need
        }
    }
}
