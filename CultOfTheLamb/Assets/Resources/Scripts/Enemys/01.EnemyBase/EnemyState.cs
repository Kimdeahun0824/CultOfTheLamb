using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDirection
{
    UP, DOWN
}
#region LegacyCode
public enum EnemyType
{
    ARCHER, SWORDMAN, FORESTWORM_BOSS
}

public abstract class EnemyState
{
    public Enemy enemy;

    public EnemyState(Enemy enemy_)
    {
        this.enemy = enemy_;
    }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
    public virtual void Action() { }
    public virtual void UpdateState() { }      // 상태의 전환 조건을 체크
    public virtual void Hit() { }              // 타격되었을때 상태 전환
    public virtual void UpdateAni() { }        // 각 상태에 따른 애니를 전환
}

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(Enemy enemy_) : base(enemy_)
    {
    }
    public override void UpdateState()
    {
        if (enemy.distance <= enemy.attackDistance)
        {
            switch (enemy.enemyType)
            {
                case EnemyType.ARCHER:
                    enemy.SetState(new EnemyRangedAttackState(enemy));
                    break;
                case EnemyType.SWORDMAN:
                    enemy.SetState(new EnemyMeleeAttackState(enemy));
                    break;
                default:
                    break;
            }
        }
    }

    public override void Action()
    {
        if (enemy.currentPlayerPos != enemy.player.transform.position)
        {
            enemy.PathFindingPlayer();
        }
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.currentWayPoint, enemy.speed * Time.deltaTime);
    }

    public override void Hit()
    {
        enemy.SetState(new EnemyHitState(enemy));
    }

    public override void UpdateAni()
    {
        enemy.enemyAnimationController.nextAnimation = enemy.enemyAnimationController.run;
    }
}

public class EnemyMeleeAttackState : EnemyState
{
    public EnemyMeleeAttackState(Enemy enemy_) : base(enemy_)
    {
    }
    public override void UpdateState()
    {
        if (enemy.attackDistance <= enemy.distance)
        {
            enemy.SetState(new EnemyPatrolState(enemy));
        }
    }

    public override void Action()
    {
        if (!enemy.isAttack)
        {
            enemy.StartCoroutine(EnemyAttack(enemy));
        }
    }

    public override void Hit()
    {
        enemy.SetState(new EnemyHitState(enemy));
    }

    public override void UpdateAni()
    {

    }

    IEnumerator EnemyAttack(Enemy enemy)
    {
        enemy.isAttack = true;
        enemy.attackCollider.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemy.attackCollider.SetActive(false);
        enemy.isAttack = false;
    }
}

public class EnemyRangedAttackState : EnemyState
{
    public EnemyRangedAttackState(Enemy enemy_) : base(enemy_)
    {

    }
    public override void UpdateState()
    {

    }

    public override void Action()
    {

    }

    public override void Hit()
    {
        enemy.SetState(new EnemyHitState(enemy));
    }

    public override void UpdateAni()
    {
        //enemy
    }

}

public class EnemyHitState : EnemyState
{
    public EnemyHitState(Enemy enemy_) : base(enemy_)
    {
    }

    public override void UpdateState()
    {
        if (!enemy.isHit)
        {
            enemy.StartCoroutine(EnemyHit());
        }
    }
    public override void Action()
    {

    }

    public override void Hit()
    {

    }

    public override void UpdateAni()
    {

    }

    IEnumerator EnemyHit()
    {
        enemy.isHit = true;
        yield return new WaitForSeconds(1f);
        enemy.isHit = false;
    }
}
#endregion
