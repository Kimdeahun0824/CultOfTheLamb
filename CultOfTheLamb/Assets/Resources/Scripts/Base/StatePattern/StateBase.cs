namespace State
{
    using UnityEngine;
    public class StateMachine
    {
        private StateBase currentState;

        public void SetState(StateBase state)
        {
            if (currentState != null)
            {
                Debug.Log($"currentState Is Not Null");
                currentState.OnExit();
                currentState = state;
                currentState.OnEnter();
            }
            else
            {
                Debug.Log($"currentState Is Null");
                currentState = state;
                currentState.OnEnter();
            }
        }

        public void Update()
        {
            currentState.UpdateState();
        }

        public void OnEnter()
        {
            currentState.OnEnter();
        }

        public void OnExit()
        {
            currentState.OnExit();
        }

        public void ChangeState()
        {
            currentState.ChangeState();
        }

        public void Action()
        {
            currentState.Action();
        }

        public StateBase GetState()
        {
            return currentState;
        }
    }

    public abstract class StateBase
    {
        public abstract void OnEnter();
        public abstract void UpdateState();
        public abstract void OnExit();

        public abstract void ChangeState();

        public abstract void Action();
    }
}