using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;
using Spine;

public class Enemy_Archer : tempEnemy
{
    protected new void Start()
    {
        enemyType = tempEnemyType.ARCHER;
        base.Start();
    }
    protected override void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
    }

    protected override void HandleAnimationStateEndEvent(TrackEntry trackEntry)
    {
    }

    protected override void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
    }

    protected override void HandleAnimationStateStartEvent(TrackEntry trackEntry)
    {
    }
}
