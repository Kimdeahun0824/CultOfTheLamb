using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Spike : MonoBehaviour
{
    SkeletonAnimationHandler skeletonAnimationHandler;

    private void Awake()
    {
        skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
    }
    private void Start()
    {
        //skeletonAnimationHandler.skeletonAnimation.state.SetAnimation(0, "tunneling", false);
        //skeletonAnimationHandler.PlayAnimationForState("Tunneling", 0, false, 0.5f);
        skeletonAnimationHandler.skeletonAnimation.state.Complete += HandleAnimationStateCompleteEvent;
    }

    private void OnEnable()
    {
        skeletonAnimationHandler.skeletonAnimation.state.SetAnimation(0, "tunneling", false);
    }

    protected virtual void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
        gameObject.SetActive(false);
    }


}
