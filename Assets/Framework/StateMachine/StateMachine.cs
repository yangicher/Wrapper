using System;
using System.Collections.Generic;
using Assets.Framework.Logger;
using UnityEngine;
using LogType = Assets.Framework.Logger.LogType;

namespace Assets.Framework.StateMachine
{
    public class StateMachine<T> : IStateMachine<T> where T : struct, IConvertible
    {
        private IDictionary<T, IState<T>> _states;

        private IState<T> _currentState;

        private T _currentCommand;

        public StateMachine(GameObject gameObject)
        {
            GameObject = gameObject;
            _states = new Dictionary<T, IState<T>>();
        }

        public T CurrentState
        {
            get
            {
                return _currentCommand;
            }
        }

        protected IDictionary<T, IState<T>> States
        {
            get { return _states; }
        }

        public GameObject GameObject { get; private set; }

        public void AddState(T command, IState<T> state)
        {
            state.SetStateMachine(this);
            _states[command] = state;
        }

        public void RemoveState(T command)
        {
            if (!_states.ContainsKey(command))
            {
                LogService.Log(this, LogType.None, "State machine doesn't contain state for command: " + command);
                return;
            }

            _states.Remove(command);
        }

        public virtual void Transition(T command)
        {
            _currentCommand = command;

            if (_states.TryGetValue(command, out _currentState))
            {
                _currentState.Initialize();
            }
            else
            {
                _currentState = null;
            }

            LogTransition(command);
        }

        public void Update()
        {
            if (_currentState == null)
            {
                return;
            }

            _currentState.Execute(Time.deltaTime);
        }

        public void Terminate()
        {
            if (_currentState != null)
            {
                _currentState.Terminate();
            }
        }

        public void Clear()
        {
            _currentCommand = default(T);
            _currentState = null;
            _states.Clear();
        }

        protected virtual void LogTransition(T command)
        {
            LogService.Log(this, LogType.None, "Transition by {0} command to {1}", command, _currentState);
        }
    }
}
