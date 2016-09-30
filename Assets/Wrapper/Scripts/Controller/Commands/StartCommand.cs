using Assets.Wrapper.Scripts.Views;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

namespace Assets.Wrapper.Scripts.Controller.Commands
{
    public class StartCommand : Command
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject contextView { get; set; }

        public override void Execute()
        {
            var menuViewGameObject = GameObject.Instantiate(Resources.Load<MenuView>("MenuView"));
            menuViewGameObject.transform.parent = contextView.transform;
        }
    }
}
