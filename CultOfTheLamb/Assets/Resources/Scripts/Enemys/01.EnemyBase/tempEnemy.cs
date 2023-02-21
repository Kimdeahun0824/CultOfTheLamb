using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class tempEnemy : MonoBehaviour
{
    public tempEnemyType enemyType;
    public tempEnemyStateMachine enemyStateMachine;

    void Start()
    {
        enemyStateMachine = new tempEnemyStateMachine();

        switch (enemyType)
        {
            case tempEnemyType.FORESTWORM:
                enemyStateMachine.SetState(new ForestWormIntroState());
                break;
        }
    }

}