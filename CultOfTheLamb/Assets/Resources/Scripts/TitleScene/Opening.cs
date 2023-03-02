using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

public class Opening : MonoBehaviour
{
    public SkeletonAnimationHandler skeletonAnimationHandler;
    public Camera my_Camera;
    public GameObject mainMenu;
    Color color;
    void Start()
    {
        skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
        skeletonAnimationHandler.PlayAnimation("Animation", 0, false, 1f);
        skeletonAnimationHandler.skeletonAnimation.AnimationState.Complete += HandleAnimationStateCompleteEvent;
        color = new Color(1f, 0.39f, 0.39f);
    }

    private void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
        my_Camera.backgroundColor = color;
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
