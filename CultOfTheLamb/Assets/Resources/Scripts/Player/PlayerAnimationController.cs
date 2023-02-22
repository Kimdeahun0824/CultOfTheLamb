using UnityEngine;
using Spine.Unity;

public class PlayerAnimationController : MonoBehaviour
{
    public SkeletonAnimationHandler skeletonAnimationHandler;
    Player player;
    IPlayerState previousPlayerState;
    Direction previousDirection;

    void Start()
    {
        //skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        skeletonAnimationHandler = GetComponent<SkeletonAnimationHandler>();
        if (skeletonAnimationHandler == null) return;
        player = GetComponent<Player>();
        if (player == null) return;
        previousPlayerState = player.GetState();
        previousDirection = player.GetDirection();
        PlayNewStableAnimation();
    }

    void Update()
    {
        IPlayerState currentPlayerState = player.GetState();
        Direction currentDirection = player.GetDirection();
        if (previousPlayerState != currentPlayerState || previousDirection != currentDirection)
        {
            //currentPlayerState.SetAnimation(this, currentDirection);
            //PlayNewStableAnimation();
            //player.state
            player.GetState().SetAnimation(skeletonAnimationHandler, currentDirection);
        }
        previousPlayerState = currentPlayerState;
        previousDirection = currentDirection;
        Turn();
    }
    void PlayNewStableAnimation()
    {
        player.GetState().SetAnimation(skeletonAnimationHandler, Direction.DOWN);
    }

    void Turn()
    {
        if (0 < player.GetPosition().x)
        {
            skeletonAnimationHandler.skeletonAnimation.skeleton.ScaleX = -1;
        }
        else if (player.GetPosition().x < 0)
        {
            skeletonAnimationHandler.skeletonAnimation.skeleton.ScaleX = 1;
        }
    }
}
