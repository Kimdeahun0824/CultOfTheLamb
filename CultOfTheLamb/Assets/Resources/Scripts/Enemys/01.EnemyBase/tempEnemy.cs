using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;
using Spine;

public class tempEnemy : MonoBehaviour
{
    public tempEnemyType enemyType;
    public tempEnemyStateMachine enemyStateMachine;
    public SkeletonAnimationHandler skeletonAnimationHandler;

    [Space(5)]
    [Header("EnemyStat")]
    public float maxHp;
    public float currentHp;
    public float damage;

    protected void Start()
    {
        skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
        enemyStateMachine = new tempEnemyStateMachine();

        switch (enemyType)
        {
            case tempEnemyType.FORESTWORM:
                var forestWormBoss = GetComponent<ForestWormBoss>();
                Debug.Log($"forestWormBoss : {forestWormBoss}");
                enemyStateMachine.SetState(new ForestWormIntroState(forestWormBoss));
                break;
        }
        Debug.Log($"currentState : {enemyStateMachine}");
    }

    protected void Update()
    {
        enemyStateMachine.Update();
    }

    protected virtual void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {

    }

    protected virtual void HandleAnimationStateStartEvent(TrackEntry trackEntry)
    {

    }

    protected virtual void HandleAnimationStateEndEvent(TrackEntry trackEntry)
    {

    }

    protected virtual void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {

    }






}