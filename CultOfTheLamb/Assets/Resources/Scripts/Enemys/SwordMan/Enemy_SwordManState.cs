namespace State
{
    using UnityEngine;

    public class PatrolState : State
    {
        tempEnemy enemy;
        public PatrolState(tempEnemy enemy_)
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

    public class ChasingState : State
    {
        tempEnemy enemy;
        public ChasingState(tempEnemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Run", 0, true, 1f);
        }
        public override void UpdateState()
        {
            AStarPathRequestManager.Instance.RequestPath(enemy.transform.position, enemy.player.transform.position, enemy.OnPathFound);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.currentWayPoint, enemy.currentSpeed * Time.deltaTime);
            bool flip = enemy.transform.position.x <= enemy.currentWayPoint.x;
            enemy.skeletonAnimationHandler.SetFlip(flip);
            float distance = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
            if (distance <= enemy.distance)
            {
                ChangeState();
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
            enemy.SetState(new Attack_ChargeState(enemy));
        }
    }

    public class Attack_ChargeState : State
    {
        tempEnemy enemy;
        public Attack_ChargeState(tempEnemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.skeletonAnimationHandler.PlayAnimation("Attack_Charge", 0, false, 1f);
            enemy.targetPos = enemy.player.transform.position;
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

    public class Attack_ImpactState : State
    {
        tempEnemy enemy;
        public Attack_ImpactState(tempEnemy enemy_)
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
        }
        public override void OnExit()
        {
            enemy.currentSpeed = enemy.defaultSpeed;
            enemy.attackCollider.SetActive(false);
        }
        public override void Action()
        {
        }
        public override void ChangeState()
        {
            enemy.SetState(new IdleState(enemy));
        }
    }
}