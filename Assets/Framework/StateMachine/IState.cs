namespace Assets.Framework.StateMachine
{
    public interface IState<T>
    {
        void Initialize();

        void Execute(float deltaTime);

        void Terminate();

        void SetStateMachine(IStateMachine<T> stateMachine);
    }
}
