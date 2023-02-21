namespace State
{
    using UnityEngine;
    public class tempEnemyStateMachine
    {
        private State currentState;

        public void SetState(State state)
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
    }

    public abstract class State
    {
        public abstract void OnEnter();
        public abstract void UpdateState();
        public abstract void OnExit();
    }

    public enum tempEnemyType
    {
        FORESTWORM, SWORDMAN, ARCHER
    }

}