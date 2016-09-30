using strange.extensions.mediation.impl;
using UnityEngine;

namespace Assets.Wrapper.Scripts.Views
{
    public class MenuViewMediator : Mediator
    {
        [Inject]
        public MenuView View { get; set; }


        public override void OnRegister()
        {
            View.StartClick.AddListener(OnStartClickHandler);
            View.Initialize();
        }

        public override void OnRemove()
        {
            View.StartClick.RemoveListener(OnStartClickHandler);
        }

        private void OnStartClickHandler()
        {
            View.RemoveView();

            //TODO: 
            var menuViewGameObject = GameObject.Instantiate(Resources.Load<GameView>("GameView"));
            menuViewGameObject.transform.parent = contextView.transform;
        }
    }
}
