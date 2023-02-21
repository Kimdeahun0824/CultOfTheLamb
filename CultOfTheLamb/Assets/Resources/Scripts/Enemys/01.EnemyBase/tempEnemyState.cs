namespace State
{
    public class tempEnemyStateMachine
    {
        private State currentState;

        public void SetState(State state)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
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