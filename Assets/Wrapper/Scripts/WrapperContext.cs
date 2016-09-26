using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Assets.Wrapper.Scripts.Controller;
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
            CommonSignal.StartSignal startSignal = (CommonSignal.StartSignal)injectionBinder.GetInstance<CommonSignal.StartSignal>();
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
            //
            //            mediationBinder.Bind<IntroUI>().To<IntroUIMediator>();
            //            //
            //            mediationBinder.Bind<PlayerPaddleUI>().To<PlayerPaddleUIMediator>();
            //            mediationBinder.Bind<CPUPaddleUI>().To<CPUPaddleUIMediator>();
            //            mediationBinder.Bind<SoccerBallUI>().To<SoccerBallUIMediator>();
            //            //
            //            mediationBinder.Bind<VirtualControllerUI>().To<VirtualControllerUIMediator>();
            //            mediationBinder.Bind<KeyboardControllerUI>().To<KeyboardControllerUIMediator>();
            //            //
            //            mediationBinder.Bind<GameManagerUI>().To<GameManagerUIMediator>();
            //            //
            //            mediationBinder.Bind<HUDUI>().To<HUDUIMediator>();
            //            mediationBinder.Bind<SoundManagerUI>().To<SoundManagerUIMediator>();
            //
            //DEBUGGING



            /**
             * CONTROLLER
             * 
             * 
            **/
            //	1. (MAPPED COMMANDS) 
            //            commandBinder.Bind<StartSignal>().To<StartCommand>(); //TODO add once()
            //            commandBinder.Bind<AllViewsInitializedSignal>().To<AllViewsInitializedCommand>();//TODO add once()


            //	2. (INJECTED SIGNALS - DIRECTLY OBSERVED)
            //            injectionBinder.Bind<SoundPlaySignal>().ToSingleton();
            //            //
            //            injectionBinder.Bind<PlayerDoMoveSignal>().ToSingleton();
            //
            //            //
            //            injectionBinder.Bind<PromptStartSignal>().ToSingleton();
            //            injectionBinder.Bind<PromptEndedSignal>().ToSingleton();
            //            //
            //            commandBinder.Bind<GameResetSignal>().To<GameResetCommand>();


            //	3. (PAIRS OF MAPPED/INJECTED SIGNALS)
            //
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
