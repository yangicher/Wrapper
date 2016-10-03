using System;

namespace Assets.Framework.StateMachine
{
    public abstract class State<T> : IState<T> where T : struct, IConvertible
    {
        private IStateMachine<T> _stateMachine;

        protected State(params object[] args)
        {   
        }

        protected StateMachine<T> StateMachine
        {
            get
            {
                return (StateMachine<T>)_stateMachine;
            }
        }

        public void SetStateMachine(IStateMachine<T> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            OnInitialize();
        }

        public virtual void Execute(float deltaTime)
        {   
        }

        public virtual void Terminate()
        {   
        }

        protected virtual void OnInitialize()
        {   
        }
    }
}
