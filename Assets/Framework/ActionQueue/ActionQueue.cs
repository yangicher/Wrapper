using System.Collections.Generic;

namespace Assets.Framework.ActionQueue
{
    public class ActionQueue
    {
        private readonly List<IAction> _actions;
        private int _currentAction;

        public delegate void EventQueueDelegate();

        private EventQueueDelegate _evemtQueueDelegate;

        public ActionQueue()
        {
            _actions = new List<IAction>();
        }

        public void Start(EventQueueDelegate del)
        {
            _currentAction = 0;
            _evemtQueueDelegate += del;
            StartAction();
        }

        public void Add(IAction action)
        {
            _actions.Add(action);
        }

        public void StopAction()
        {
            OnActionComplete(null);
        }

        public void Destroy()
        {
            foreach (EventQueueDelegate eventDelegate in _evemtQueueDelegate.GetInvocationList())
            {
                _evemtQueueDelegate -= eventDelegate;
            }
        }

        private void OnActionComplete(object result)
        {
            IAction action = _actions[_currentAction];
            action.Destroy();
            _currentAction++;
            StartAction();
        }

        private void StartAction()
        {
            if (_currentAction < _actions.Count)
            {
                IAction action = _actions[_currentAction];
                action.CompleteEvent += OnActionComplete;
                action.Execute();
            }
            else
            {
                if (_evemtQueueDelegate != null)
                {
                    _evemtQueueDelegate();
                    Destroy();
                }
            }
        }
    }
}