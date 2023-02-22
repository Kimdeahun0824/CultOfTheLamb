using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Spike : MonoBehaviour
{
    //SkeletonAnimationHandler skeletonAnimationHandler = default;
    public SkeletonAnimation skeletonAnimation;
    [SpineSkin] public string Skin_1;
    [SpineSkin] public string Skin_2;
    [SpineSkin] public string Skin_3;
    [SpineSkin] public string Skin_4;
    private void Awake()
    {
        //skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
        //skeletonAnimation.state.SetAnimation(0, "tunneling", false).TimeScale = 0.5f;


    }

    private void OnEnable()
    {
        // Debug.Log($"skeletonAnimaton State Test : {skeletonAnimation.state}");
        // skeletonAnimation.Initialize(false, false);
        // skeletonAnimation.state.SetAnimation(0, "tunneling", false).TimeScale = 0.5f;
        // skeletonAnimation.state.Complete += HandleAnimationStateCompleteEvent;
    }

    private void Start()
    {
        Skin skin = default;
        int randNum = Random.Range(0, 4);
        switch (randNum)
        {
            case 0:
                skin = skeletonAnimation.skeleton.Data.FindSkin(Skin_1);
                break;
            case 1:
                skin = skeletonAnimation.skeleton.Data.FindSkin(Skin_2);
                break;
            case 2:
                skin = skeletonAnimation.skeleton.Data.FindSkin(Skin_3);
                break;
            case 3:
                skin = skeletonAnimation.skeleton.Data.FindSkin(Skin_4);
                break;
        }
        skeletonAnimation.skeleton.SetSkin(skin);
        Debug.Log($"Spike Skin Name : {skeletonAnimation.skeleton.Skin}");
        skeletonAnimation.state.SetAnimation(0, "tunneling", false).TimeScale = 0.5f;
        skeletonAnimation.state.Complete += HandleAnimationStateCompleteEvent;
    }

    protected virtual void HandleAnimationStateCompleteEvent(TrackEntry trackEntry)
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }


}
