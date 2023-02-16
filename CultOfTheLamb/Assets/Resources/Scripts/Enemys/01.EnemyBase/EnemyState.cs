using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDirection
{
    UP, DOWN
}

public interface IEnemyState
{
    public void Action(Enemy enemy);
}

public class EnemyIdleState : IEnemyState
{
    public void Action(Enemy enemy)
    {

    }
}

public class EnemyMoveState : IEnemyState
{
    public void Action(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}

public class EnemyAttackState : IEnemyState
{
    public void Action(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}

public class EnemeyHitState : IEnemyState
{
    public void Action(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}

public class EnemyDieState : IEnemyState
{
    public void Action(Enemy enemy)
    {
        throw new System.NotImplementedException();
    }
}