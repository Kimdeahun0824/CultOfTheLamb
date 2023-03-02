using UnityEngine;
using System.Collections;

namespace State
{

    public class PatrolState : StateBase
    {
        Enemy enemy;
        public PatrolState(Enemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
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

    public class ChasingState : StateBase
    {
        Enemy enemy;
        public ChasingState(Enemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Run", 0, true, 1f);
        }
        public override void UpdateState()
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.currentWayPoint, enemy.currentSpeed * Time.deltaTime);
            bool flip = enemy.transform.position.x <= enemy.currentWayPoint.x;
            enemy.SetFlip(flip);

            float distance = Vector3.Distance(enemy.transform.position, enemy.PlayerPos);
            if (distance <= enemy.distance)
            {
                enemy.SetState(new Attack_ChargeState(enemy));
            }
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

    public class Attack_ChargeState : StateBase
    {
        Enemy enemy;
        public Attack_ChargeState(Enemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Attack_Charge", 0, false, 1f);
            enemy.targetPos = enemy.PlayerPos;
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
            enemy.SetState(new Attack_ImpactState(enemy));
        }
    }

    public class Attack_ImpactState : StateBase
    {
        Enemy enemy;
        public Attack_ImpactState(Enemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Attack_Impact", 0, false, 1f);
            enemy.currentSpeed *= 5;
            enemy.attackCollider.SetActive(true);
        }
        public override void UpdateState()
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.targetPos, enemy.currentSpeed * Time.deltaTime);
            if (enemy.transform.position == enemy.targetPos)
            {
                enemy.attackCollider.SetActive(false);
            }
        }
        public override void OnExit()
        {
            enemy.currentSpeed = enemy.defaultSpeed;
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