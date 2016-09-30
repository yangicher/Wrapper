using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Assets.Wrapper.Scripts.Controller;
using Assets.Wrapper.Scripts.Views;
using com.rmc.projects.paddle_soccer.mvcs.controller.commands;
using com.rmc.projects.paddle_soccer.mvcs.controller.signals;
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

        override public IContext Start()
        {
            base.Start();
            StartSignal startSignal = (StartSignal)injectionBinder.GetInstance<StartSignal>();
            startSignal.Dispatch();
            return this;
        }

        protected override void mapBindings()
        {
            /**
             * MODEL
             * 
             * 
            **/
            //injectionBinder.Bind<IGameModel>().To<GameModel>().ToSingleton();

            /**
             * VIEW
             * 
             * 
            **/
            mediationBinder.Bind<GameView>().To<GameViewMediator>();
            mediationBinder.Bind<HudView>().To<HudMediator>();

            //            mediationBinder.Bind<VirtualControllerUI>().To<VirtualControllerUIMediator>();
            //            mediationBinder.Bind<KeyboardControllerUI>().To<KeyboardControllerUIMediator>();
            //            mediationBinder.Bind<GameManagerUI>().To<GameManagerUIMediator>();
            //            mediationBinder.Bind<SoundManagerUI>().To<SoundManagerUIMediator>();
            //

            /**
             * CONTROLLER
             * 
             * 
            **/
            //	1. (MAPPED COMMANDS) 
            commandBinder.Bind<StartSignal>().To<StartCommand>().Once();

            //	2. (INJECTED SIGNALS - DIRECTLY OBSERVED)
            injectionBinder.Bind<CommonSignal.StartGameSignal>().ToSingleton();
            injectionBinder.Bind<CommonSignal.StopGameSignal>().ToSingleton();
            injectionBinder.Bind<CommonSignal.ChangeScoreSignal>().ToSingleton();
            //            commandBinder.Bind<GameResetSignal>().To<GameResetCommand>();


            //	3. (PAIRS OF MAPPED/INJECTED SIGNALS)
            //            commandBinder.Bind<RightPaddleScoreChangeSignal>().To<RightPaddleScoreChangeCommand>();
            //            injectionBinder.Bind<RightPaddleScoreChangedSignal>().ToSingleton();

            /**
             * SERVICE
             * 
             * 
            **/

            //(None. This project doesn't load/send any files/data)


        }
    }
}
