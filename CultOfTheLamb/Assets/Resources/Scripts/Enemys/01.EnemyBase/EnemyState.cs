using System.Collections;
using System.Collections.Generic;
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