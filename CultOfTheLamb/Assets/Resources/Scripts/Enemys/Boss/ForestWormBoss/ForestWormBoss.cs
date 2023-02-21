using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine.Collections;
using Spine;
using State;

public class ForestWormBoss : tempEnemy
{
    public GameObject attackSpike;
    private bool m_IsEndIntro = default;
    public bool IsEndIntro
    {
        get;
        private set;
    }

    protected new void Start()
    {
        base.Start();
        skeletonAnimationHandler.skeletonAnimation.AnimationState.Event += HandleAnimationStateEvent;
        skeletonAnimationHandler.skeletonAnimation.AnimationState.Start += HandleAnimationStateStartEvent;
        skeletonAnimationHandler.skeletonAnimation.AnimationState.End += HandleAnimationStateEndEvent;
        skeletonAnimationHandler.skeletonAnimation.AnimationState.Complete += HandleAnimationStateCompleteEvent;
    }

    protected override void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
    {
        Debug.Log($"Event Trigger Test");
    }

    protected override void HandleAnimationStateStartEvent(TrackEntry trackEntry)
    {
        Debug.Log($"Event Start Test");
    }

    protected override void HandleAnimationStateEndEvent(TrackEntry trackEntry)
    {
        Debug.Log($"Event End Test");
    }
    protected override void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
        if (trackEntry.ToString() == "intro2")
        {
            IsEndIntro = true;
        }
        if (trackEntry.ToString() == "die")
        {
            IsDie = true;
        }
        Debug.Log($"Event Complete Test : {trackEntry.ToString()}");
    }

}
