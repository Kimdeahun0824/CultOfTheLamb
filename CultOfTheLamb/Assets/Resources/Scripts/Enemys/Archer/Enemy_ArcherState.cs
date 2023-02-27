using UnityEngine;

namespace State
{

    public class Archer_Attack_ChargeState : StateBase
    {
        Enemy_Archer enemy;
        public Archer_Attack_ChargeState(Enemy_Archer enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Archer_Attack_Charge", 0, false, 1f);
        }

        public override void UpdateState()
        {
        }
        public override void OnExit()
        {
            enemy.targetPos = enemy.player.transform.position;
        }

        public override void Action()
        {
        }

        public override void ChangeState()
        {
            enemy.SetState(new Archer_Attack_ImpactState(enemy));
        }
    }

    public class Archer_Attack_ImpactState : StateBase
    {
        Enemy_Archer enemy;
        public Archer_Attack_ImpactState(Enemy_Archer enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Archer_Attack_Impact", 0, false, 1f);
            enemy.ShootArrow();
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

}