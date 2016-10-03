using UnityEngine;

namespace Assets.Framework.StateMachine
{
    public interface IStateMachine<T>
    {
        GameObject GameObject { get; }

        T CurrentState { get; }

        void AddState(T command, IState<T> state);

        void Transition(T command);

        void RemoveState(T command);

        void Update();

        void Terminate();

        void Clear();
    }
}
