using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
public class SkeletonAnimationHandler : MonoBehaviour
{
    public Spine.Animation TargetAnimation { get; private set; }
    [Header("Spine")]
    public SkeletonAnimation skeletonAnimation;
    public List<StateNameToAnimationReference> statesAndAnimations = new List<StateNameToAnimationReference>();
    public List<AnimationTransition> transitions = new List<AnimationTransition>();

    [System.Serializable]
    public class StateNameToAnimationReference
    {
        public string stateName;
        public AnimationReferenceAsset animation;
    }

    [System.Serializable]
    public class AnimationTransition
    {
        public AnimationReferenceAsset from;
        public AnimationReferenceAsset to;
        public AnimationReferenceAsset transition;
    }

    private void Awake()
    {
        // 초기화 과정
        foreach (var entry in statesAndAnimations)
        {
            entry.animation.Initialize();
        }

        foreach (var entry in transitions)
        {
            entry.from.Initialize();
            entry.to.Initialize();
            entry.transition.Initialize();
        }
    }

    public void SetFlip(float horizontal)
    {
        if (horizontal != 0)
        {
            skeletonAnimation.skeleton.ScaleX = horizontal > 0 ? 1f : -1f;
        }
    }

    public void SetFlip(bool flip)
    {
        skeletonAnimation.skeleton.ScaleX = flip ? -1f : 1f;
    }

    /// <summary>
    /// 애니메이션 재생 함수
    /// </summary>
    /// <param name="stateName">실행하고자 하는 애니메이션 이름</param>
    /// <param name="layerIndex">트랙/레이어 번호</param>
    /// <param name="loop">반복 여부</param>
    /// <param name="speed">애니메이션 재생 속도</param>
    public void PlayAnimation(string stateName, int layerIndex, bool loop, float speed)
    {
        PlayAnimation(StringToHash(stateName), layerIndex, loop, speed);
    }

    /// <summary>
    /// 애니메이션 재생 함수
    /// </summary>
    /// <param name="shortNameHash">실행하고자 하는 애니메이션 번호</param>
    /// <param name="layerIndex">트랙/레이어 번호</param>
    /// <param name="loop">반복 여부</param>
    /// <param name="speed">애니메이션 재생 속도</param>
    public void PlayAnimation(int shortNameHash, int layerIndex, bool loop, float speed)
    {
        var foundAnimation = GetAnimationForState(shortNameHash);
        if (foundAnimation == null) return;

        skeletonAnimation.state.SetAnimation(layerIndex, foundAnimation, loop).TimeScale = speed;
    }

    /// <summary>
    /// 재생 애니메이션 추가 함수
    /// </summary>
    /// <param name="stateName">추가하고자 하는 애니메이션 이름</param>
    /// <param name="layerIndex">트랙/레이어 번호</param>
    /// <param name="loop">반복 여부</param>
    /// <param name="speed">애니메이션 재생 속도</param>
    public void AddPlayAnimation(string stateName, int layerIndex, bool loop, float speed, float delay)
    {
        AddPlayAnimation(StringToHash(stateName), layerIndex, loop, speed, delay);
    }

    /// <summary>
    /// 재생 애니메이션 추가 함수
    /// </summary>
    /// <param name="shortNameHash">추가하고자 하는 애니메이션 번호</param>
    /// <param name="layerIndex">트랙/레이어 번호</param>
    /// <param name="loop">반복 여부</param>
    /// <param name="speed">애니메이션 재생 속도</param>
    public void AddPlayAnimation(int shortNameHash, int layerIndex, bool loop, float speed, float delay)
    {
        var foundAnimation = GetAnimationForState(shortNameHash);
        if (foundAnimation == null) return;

        skeletonAnimation.state.AddAnimation(layerIndex, foundAnimation, loop, delay).TimeScale = speed;
    }


    public void PlayAnimationForState(string stateShortName, int layerIndex, bool oneshot, float speed)
    {
        PlayAnimationForState(StringToHash(stateShortName), layerIndex, oneshot, speed);
    }

    /// <summary>
    /// PlayAnimationForState Overloading 해당 애니메이션을 실행
    /// </summary>
    /// <param name="stateShortName">실행하고자 하는 애니메이션 이름</param>
    /// <param name="layerIndex">트랙/레이어 번호</param>
    public void PlayAnimationForState(int shortNameHash, int layerIndex, bool oneshot, float speed)
    {
        var foundAnimation = GetAnimationForState(shortNameHash);
        if (foundAnimation == null)
            return;

        if (oneshot)
        {
            PlayOneShot(foundAnimation, layerIndex, speed);
        }
        else
        {
            PlayNewAnimation(foundAnimation, layerIndex, speed);
        }
    }

    public Spine.Animation GetAnimationForState(string stateShortName)
    {
        return GetAnimationForState(StringToHash(stateShortName));
    }

    /// <summary>
    /// GetAnimationForState Overloading 해당 애니메이션을 반환(없다면 null)
    /// </summary>
    /// <param name="stateShortName">찾고자 하는 애니메이션 이름(정수로 들어옴)</param>
    /// <returns>해당 애니메이션</returns>
    public Spine.Animation GetAnimationForState(int shortNameHash)
    {
        var foundState = statesAndAnimations.Find(entry => StringToHash(entry.stateName) == shortNameHash);
        return (foundState == null) ? null : foundState.animation;
    }

    /// <summary>
    /// 애니메이션 재생 메서드
    /// 현재 진행중인 애니메이션이 없다면 || 전환 애니메이션이 없다면 바로 애니메이션 전환
    /// 있다면 전환 애니메이션 우선 재생 후 재생
    /// </summary>
    /// <param name="target"></param>
    /// <param name="layerIndex"></param>
    public void PlayNewAnimation(Spine.Animation target, int layerIndex, float speed)
    {
        Spine.Animation transition = null;
        Spine.Animation current = null;

        current = GetCurrentAnimation(layerIndex);
        if (current != null)
            transition = TryGetTransition(current, target);

        if (transition != null)
        {
            skeletonAnimation.AnimationState.SetAnimation(layerIndex, transition, false).TimeScale = speed;
            skeletonAnimation.AnimationState.AddAnimation(layerIndex, target, true, 0).TimeScale = speed;
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(layerIndex, target, true).TimeScale = speed;
        }

        this.TargetAnimation = target;
    }

    /// <summary>
    /// OneShot 애니메이션 메서드
    /// </summary>
    /// <param name="oneShot">한번 재생할 애니메이션 클립</param>
    /// <param name="layerIndex">null</param>
    public void PlayOneShot(Spine.Animation oneShot, int layerIndex, float speed)
    {
        var state = skeletonAnimation.AnimationState;
        TrackEntry a = state.SetAnimation(layerIndex, oneShot, false);
        a.TimeScale = speed;

        var transition = TryGetTransition(oneShot, TargetAnimation);
        if (transition != null)
            state.AddAnimation(layerIndex, transition, false, 0f).TimeScale = speed;
        if (layerIndex > 0)
        {
            state.AddEmptyAnimation(layerIndex, 0, a.Animation.Duration);
        }

        state.AddAnimation(0, this.TargetAnimation, true, a.Animation.Duration).TimeScale = speed;
    }

    /// <summary>
    /// 현재 애니메이션에서 다음 애니메이션으로 전환될 때 전환 애니메이션이 있는지 판단
    /// </summary>
    /// <param name="from">현재 애니메이션</param>
    /// <param name="to">다음 애니메이션</param>
    /// <returns>없다면 null 있다면 전환애니메이션(ex)ldel-to-jump)</returns>
    Spine.Animation TryGetTransition(Spine.Animation from, Spine.Animation to)
    {
        foreach (var transition in transitions)
        {
            if (transition.from.Animation == from && transition.to.Animation == to)
            {
                return transition.transition.Animation;
            }
        }
        return null;
    }

    /// <summary>
    /// 현재 애니메이션을 불러오는 메서드
    /// </summary>
    /// <param name="layout">해당 애니메이션 트랙/layout</param>
    /// <returns>Spine.Animation</returns>
    private Spine.Animation GetCurrentAnimation(int layerIndex)
    {
        var currentTrackEntry = skeletonAnimation.AnimationState.GetCurrent(layerIndex);
        return (currentTrackEntry != null) ? currentTrackEntry.Animation : null;
    }

    int StringToHash(string s)
    {
        return Animator.StringToHash(s);
    }
}