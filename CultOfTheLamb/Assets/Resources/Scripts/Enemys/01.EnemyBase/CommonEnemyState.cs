namespace State
{
    public class IdleState : State
    {
        tempEnemy enemy;
        public IdleState(tempEnemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Idle", 0, true, 1f);
            switch (enemy.enemyType)
            {
                case tempEnemyType.SWORDMAN:
                    enemy.SetState(new ChasingState(enemy));
                    break;
                case tempEnemyType.ARCHER:
                    break;
                default:
                    break;
            }
        }
        public override void UpdateState()
        {
        }
        public override void OnExit()
        {
        }
        public override void Action()
        {
        }
        public override void ChangeState()
        {
        }
    }

    public class HitState : State
    {
        tempEnemy enemy;
        public HitState(tempEnemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Hit", 0, false, 1f);
        }
        public override void UpdateState()
        {
        }
        public override void OnExit()
        {

        }
        public override void Action()
        {
        }
        public override void ChangeState()
        {
            enemy.SetState(new IdleState(enemy));
        }
    }

    public class DieState : State
    {
        tempEnemy enemy;
        public DieState(tempEnemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Die", 0, false, 1f);
        }
        public override void UpdateState()
        {
        }
        public override void OnExit()
        {
        }
        public override void Action()
        {
        }
        public override void ChangeState()
        {
        }
    }
}