namespace State
{
    public class DieState : State
    {
        private tempEnemy enemy;
        public DieState(tempEnemy enemy_)
        {
            this.enemy = enemy_;
        }

        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Die", 0, false, 1f);
        }

        public override void OnExit()
        {
        }

        public override void UpdateState()
        {
        }
    }
}