using System.Collections;
using UnityEngine;
public enum Direction
{
    UP, DOWN, UP_DIAGONAL, DOWN_DIAGONAL, HORIZONTAL
}
public interface IPlayerState
{
    public void Action(Player player);
    public void Hit(Player player);
    public void GetObject(Player player);
    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction);
}

public class IdleState : IPlayerState
{
    public void Action(Player player)
    {
        player.SetPosition(Vector3.zero);
        if (!Input.GetAxisRaw("Horizontal").Equals(0) || !Input.GetAxisRaw("Vertical").Equals(0))
        {
            player.SetState(new MoveState());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new RollingState());
        }
        else if (Input.GetMouseButtonDown(0))
        {
            player.SetState(new AttackState());
        }
        // else if (Input.GetMouseButtonDown(1))
        // {
        //     player.SetState(new ChargeState());
        // }
    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
            case Direction.UP_DIAGONAL:
                //playerAnimationController.nextAnimation = playerAnimationController.idle_up;
                skeletonAnimationHandler.PlayAnimation("Idle_Up", 0, true, 1f);
                break;
            case Direction.HORIZONTAL:
            case Direction.DOWN:
            case Direction.DOWN_DIAGONAL:
                //playerAnimationController.nextAnimation = playerAnimationController.idle;
                skeletonAnimationHandler.PlayAnimation("Idle", 0, true, 1f);
                break;
        }
    }
}

public class MoveState : IPlayerState
{
    public void Action(Player player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.SetState(new AttackState());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new RollingState());
        }
        else if (Input.GetAxisRaw("Horizontal").Equals(0) && Input.GetAxisRaw("Vertical").Equals(0))
        {
            player.SetState(new IdleState());
        }
        else
        {
            Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            player.SetPosition(pos);
            if (0 < pos.z)
            {
                if (pos.x != 0)
                {
                    player.SetDirection(Direction.UP_DIAGONAL);
                }
                else
                {
                    player.SetDirection(Direction.UP);
                }
            }
            else if (pos.z < 0)
            {
                if (pos.x != 0)
                {
                    player.SetDirection(Direction.DOWN_DIAGONAL);
                }
                else
                {
                    player.SetDirection(Direction.DOWN);
                }
            }
            else
            {
                if (pos.x != 0)
                {
                    player.SetDirection(Direction.HORIZONTAL);
                }
            }
        }
    }
    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }
    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }
    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                //playerAnimationController.nextAnimation = playerAnimationController.run_up;
                skeletonAnimationHandler.PlayAnimation("Run_Up", 0, true, 1f);
                break;
            case Direction.UP_DIAGONAL:
                //playerAnimationController.nextAnimation = playerAnimationController.run_up_diagonal;
                skeletonAnimationHandler.PlayAnimation("Run_Up_Diagonal", 0, true, 1f);
                break;
            case Direction.DOWN:
                //playerAnimationController.nextAnimation = playerAnimationController.run_down;
                skeletonAnimationHandler.PlayAnimation("Run_Down", 0, true, 1f);
                break;
            case Direction.DOWN_DIAGONAL:
                //playerAnimationController.nextAnimation = playerAnimationController.run;
                skeletonAnimationHandler.PlayAnimation("Run", 0, true, 1f);
                break;
            case Direction.HORIZONTAL:
                //playerAnimationController.nextAnimation = playerAnimationController.run_horizontal;
                skeletonAnimationHandler.PlayAnimation("Run_Horizontal", 0, true, 1f);
                break;
            default:
                break;
        }
    }
}

public class RollingState : IPlayerState
{
    public void Action(Player player)
    {
        if (!player.IsRolling)
        {
            player.StateStartCoroutine(Rolling(player));
        }
        if (Input.GetMouseButtonDown(0))
        {
            player.SetState(new AttackState());
        }
    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }
    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    IEnumerator Rolling(Player player)
    {
        player.IsRolling = true;
        player.Speed *= 2f;
        yield return new WaitForSeconds(0.3f);
        player.Speed = player.Default_Speed;
        player.IsRolling = false;
        player.SetState(new IdleState());
    }

    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
    {
        switch (direction)
        {
            case Direction.HORIZONTAL:
                //playerAnimationController.nextAnimation = playerAnimationController.roll;
                skeletonAnimationHandler.PlayAnimation("Roll", 0, true, 1f);
                break;
            case Direction.UP:
            case Direction.UP_DIAGONAL:
                //playerAnimationController.nextAnimation = playerAnimationController.roll_up;
                skeletonAnimationHandler.PlayAnimation("Roll_Up", 0, true, 1f);
                break;
            case Direction.DOWN:
            case Direction.DOWN_DIAGONAL:
                //playerAnimationController.nextAnimation = playerAnimationController.roll_down;
                skeletonAnimationHandler.PlayAnimation("Roll_Down", 0, true, 1f);
                break;
            default:
                break;
        }
    }
}

public class AttackState : IPlayerState
{
    public void Action(Player player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Speed = player.Default_Speed;
            player.SetState(new RollingState());
            //player.StopCoroutine(Rolling)
        }
        if (!player.IsAttack)
        {
            //player.StartCoroutine(ComboAttack(player));
            player.StartCoroutine(ComboAttack(player));
        }
    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    IEnumerator ComboAttack(Player player)
    {
        // 마우스 클릭이 되었을 때 해당 방향으로 전진하며 공격
        // 방향키를 입력하면 해당 방향으로 전진하며 공격
        player.AttackColloder.SetActive(true);
        player.IsAttack = true;
        player.Speed = 0;
        yield return new WaitForSeconds(0.5f);
        player.AttackColloder.SetActive(false);
        player.IsAttack = false;
        player.Speed = player.Default_Speed;
        player.SetState(new IdleState());
    }


    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
            case Direction.UP_DIAGONAL:
            case Direction.DOWN:
            case Direction.DOWN_DIAGONAL:
            case Direction.HORIZONTAL:
                //playerAnimationController.nextAnimation = playerAnimationController.attack_combo_1;
                skeletonAnimationHandler.PlayAnimation("Attack_Combo_1", 0, false, 1f);
                break;
            default:
                break;

        }
    }
}

public class ChargeState : IPlayerState
{
    public void Action(Player player)
    {

    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }
    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }
    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
    {
        //skeletonAnimationHandler.PlayAnimation("Die", 0, false, 1f);
    }
}

public class HitState : IPlayerState
{
    public void Action(Player player)
    {
        if (!player.IsHit)
        {
            player.StartCoroutine(playerHit(player));
        }
    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }
    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    IEnumerator playerHit(Player player)
    {
        player.IsHit = true;
        player.Speed *= -0.5f;
        yield return new WaitForSeconds(0.5f);
        player.IsHit = false;
        player.Speed *= -2f;
        player.SetState(new IdleState());
    }
    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
    {
        skeletonAnimationHandler.PlayAnimation("KnockBack", 0, false, 1f);
    }
}

public class DieState : IPlayerState
{
    public void Action(Player player)
    {
    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }
    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
    {
        skeletonAnimationHandler.PlayAnimation("Die", 0, false, 1f);
    }
}


public class GetObjectState : IPlayerState
{
    public void Action(Player player)
    {

    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }
    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }


    public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
    {
        //skeletonAnimationHandler.PlayAnimation("", 0, false, 1f);
    }
}

