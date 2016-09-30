using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using Assets.Wrapper.Scripts.Controller;
using Assets.Wrapper.Scripts.Controller.Commands;
using Assets.Wrapper.Scripts.Model;
using Assets.Wrapper.Scripts.Views;
using UnityEngine;

namespace Assets.Wrapper.Scripts
{
    public class WrapperContext : MVCSContext
    {
        public WrapperContext(MonoBehaviour view, bool autoStartup) : base(view, autoStartup)
        {
            //Debug.Log ("WrapperContext.constructor()");
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        public override void Launch()
        {
            base.Launch();

            CommonSignal.StartSignal startSignal = (CommonSignal.StartSignal)injectionBinder.GetInstance<CommonSignal.StartSignal>();
            startSignal.Dispatch();
        }

        protected override void mapBindings()
        {
            base.mapBindings();

            // MODEL
            injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();

            // VIEW
            mediationBinder.Bind<MenuView>().To<MenuViewMediator>();
            mediationBinder.Bind<GameView>().To<GameViewMediator>();
            mediationBinder.Bind<HudView>().To<HudMediator>();

            //            mediationBinder.Bind<VirtualControllerUI>().To<VirtualControllerUIMediator>();
            //            mediationBinder.Bind<KeyboardControllerUI>().To<KeyboardControllerUIMediator>();
            //            mediationBinder.Bind<GameManagerUI>().To<GameManagerUIMediator>();
            //            mediationBinder.Bind<SoundManagerUI>().To<SoundManagerUIMediator>();
            //

            /**
             * CONTROLLER
            **/
            //	1. (MAPPED COMMANDS) 
            commandBinder.Bind<CommonSignal.StartSignal>().To<StartCommand>().Once();

            //	2. (INJECTED SIGNALS - DIRECTLY OBSERVED)
            injectionBinder.Bind<CommonSignal.StartGameSignal>().ToSingleton();
            injectionBinder.Bind<CommonSignal.StopGameSignal>().ToSingleton();
            injectionBinder.Bind<CommonSignal.ChangeScoreSignal>().ToSingleton();
            //            commandBinder.Bind<GameResetSignal>().To<GameResetCommand>();


            //	3. (PAIRS OF MAPPED/INJECTED SIGNALS)
            //            commandBinder.Bind<RightPaddleScoreChangeSignal>().To<RightPaddleScoreChangeCommand>();
            //            injectionBinder.Bind<RightPaddleScoreChangedSignal>().ToSingleton();

            commandBinder.Bind<CommonSignal.GameStateChangeSignal>().To<GameStateChangeCommand>();
            injectionBinder.Bind<CommonSignal.GameStateChangedSignal>().ToSingleton();

            // SERVICE
            //(None. This project doesn't load/send any files/data)
        }
    }
}
