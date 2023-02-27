using UnityEngine;

namespace State
{
    public enum EnemyType
    {
        FORESTWORM, SWORDMAN, ARCHER
    }

    public class EnemyIdleState : StateBase
    {
        Enemy enemy;
        public EnemyIdleState(Enemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Idle", 0, true, 1f);
            if (enemy.player.GetComponent<Player>().IsDie) return;
            switch (enemy.enemyType)
            {
                case EnemyType.SWORDMAN:
                    enemy.SetState(new ChasingState(enemy));
                    break;
                case EnemyType.ARCHER:
                    Enemy_Archer archer = enemy.GetComponent<Enemy_Archer>();
                    if (archer == null) return;
                    float distance = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
                    if (distance <= enemy.distance)
                    {
                        enemy.SetState(new Attack_ChargeState(archer));
                    }
                    else
                    {
                        enemy.SetState(new Archer_Attack_ChargeState(archer));
                    }
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

    public class EnemyHitState : StateBase
    {
        Enemy enemy;
        public EnemyHitState(Enemy enemy_)
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
            enemy.SetState(new EnemyIdleState(enemy));
        }
    }

    public class EnemyDieState : StateBase
    {
        Enemy enemy;
        public EnemyDieState(Enemy enemy_)
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