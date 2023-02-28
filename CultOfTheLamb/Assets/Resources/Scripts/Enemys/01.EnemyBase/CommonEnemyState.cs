using UnityEngine;
using System.Collections;

namespace State
{
    public enum EnemyType
    {
        FORESTWORM, SWORDMAN, ARCHER
    }
    public class Enemy_Spawn_State : StateBase
    {
        Enemy enemy;
        public Enemy_Spawn_State(Enemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.PlayAnimation("Spawn", 0, false, 1f);
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

    public class EnemyIdleState : StateBase
    {
        Enemy enemy;
        public EnemyIdleState(Enemy enemy_)
        {
            this.enemy = enemy_;
        }
        public override void OnEnter()
        {
            enemy.PlayAnimation("Idle", 0, true, 1f);
            if (enemy.playerIsDieCheck) return;
            enemy.StartCoroutine(ChangeStateDelay());
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

        IEnumerator ChangeStateDelay()
        {
            yield return new WaitForSeconds(1f);
            switch (enemy.enemyType)
            {
                case EnemyType.SWORDMAN:
                    enemy.SetState(new ChasingState(enemy));
                    break;
                case EnemyType.ARCHER:
                    Enemy_Archer archer = enemy.GetComponent<Enemy_Archer>();
                    if (archer == null) yield break;
                    float distance = Vector3.Distance(enemy.transform.position, enemy.PlayerPos);
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
            enemy.PlayAnimation("Hit", 0, false, 1f);
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
            enemy.PlayAnimation("Die", 0, false, 1f);
            enemy.StartCoroutine(EnemyDie());
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

        IEnumerator EnemyDie()
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.EnemyDie(enemy);
            GameManager.Instance.PlayerRemoveObserver(enemy);
            enemy.Destroy();
        }
    }
}