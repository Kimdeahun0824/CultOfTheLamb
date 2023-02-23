using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Opening : MonoBehaviour
{
    public SkeletonAnimationHandler skeletonAnimationHandler;

    void Start()
    {
        skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
        skeletonAnimationHandler.PlayAnimation("Animation", 0, false, 1f);
        skeletonAnimationHandler.skeletonAnimation.AnimationState.Complete += HandleAnimationStateCompleteEvent;
    }

    private void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
        gameObject.SetActive(false);
    }
}
