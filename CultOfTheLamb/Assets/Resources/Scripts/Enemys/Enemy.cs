using UnityEngine;
public interface IEnemyState
{
    public void Action(Enemy enemy);
}

// public class EnemyIdleState : IEnemyState
// {

// }

// public class EnemyMoveState : IEnemyState
// {

// }

// public class EnemyAttackState : IEnemyState
// {

// }

// public class EnemeyHitState : IEnemyState
// {

// }

// public class EnemyDieState : IEnemyState
// {

// }

public class Enemy : MonoBehaviour
{
    protected IEnemyState m_EnemyState = default;
    protected float speed;
    protected float damage;
    protected float maxHp;
    protected float currentHp;
    protected bool isDie;
    protected virtual void Move()
    {

    }

    protected virtual void Attack()
    {

    }

    public virtual void Hit(float damage)
    {
        if (currentHp - damage < 0)
        {
            currentHp = 0;
            Die();
        }
        else
        {
            currentHp -= damage;
        }
    }

    protected virtual void Die()
    {

    }

}