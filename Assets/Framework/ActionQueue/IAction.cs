namespace Assets.Framework.ActionQueue
{
    public interface IAction
    {
        event System.Action<object> CompleteEvent;

        void Execute();

        void Destroy();
    }
}
