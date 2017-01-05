using Assets.Framework.Logger;

namespace Assets.Framework.ActionQueue
{
    public abstract class Action : IAction
    {
        private System.Action<float> _progressChanged;

        protected Action(System.Action<float> progressChanged)
        {
            _progressChanged = progressChanged;
        }

        public event System.Action<object> CompleteEvent;

        protected object Result { get; set; }

        public void Execute()
        {
            LogService.Log(this, "Action started");
            OnExecute();
        }

        public void Destroy()
        {
            _progressChanged = null;
            CompleteEvent = null;
            OnDestroy();
        }

        protected virtual void OnExecute()
        {
        }

        protected virtual void OnDestroy()
        {
        }

        protected virtual void OnComplete()
        {
        }

        protected void Complete()
        {
            LogService.Log(this, "Action finished");

            OnComplete();

            if (CompleteEvent != null)
            {
                CompleteEvent(Result);
            }
        }

        protected void OnProgressChanged(float progress)
        {
            if (_progressChanged != null)
            {
                _progressChanged(progress);
            }
        }
    }
}