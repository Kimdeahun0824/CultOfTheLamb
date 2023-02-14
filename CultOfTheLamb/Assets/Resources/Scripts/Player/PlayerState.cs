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
    public void Die(Player player);
    public void GetObject(Player player);
    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction);
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
    public void Die(Player player)
    {
        player.SetState(new DieState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
            case Direction.UP_DIAGONAL:
                playerAnimationController.nextAnimation = playerAnimationController.idle_up;
                break;
            case Direction.HORIZONTAL:
            case Direction.DOWN:
            case Direction.DOWN_DIAGONAL:
                playerAnimationController.nextAnimation = playerAnimationController.idle;
                break;
        }
    }
}

public class MoveState : IPlayerState
{
    public void Action(Player player)
    {
        if (Input.GetAxisRaw("Horizontal").Equals(0) && Input.GetAxisRaw("Vertical").Equals(0))
        {
            player.SetState(new IdleState());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new RollingState());
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
            Debug.Log($"Player Direction Debug : {player.GetDirection()}");
        }
    }
    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }

    public void Die(Player player)
    {
        player.SetState(new DieState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                playerAnimationController.nextAnimation = playerAnimationController.run_up;
                break;
            case Direction.UP_DIAGONAL:
                playerAnimationController.nextAnimation = playerAnimationController.run_up_diagonal;
                break;
            case Direction.DOWN:
                playerAnimationController.nextAnimation = playerAnimationController.run_down;
                break;
            case Direction.DOWN_DIAGONAL:
                playerAnimationController.nextAnimation = playerAnimationController.run;
                break;
            case Direction.HORIZONTAL:
                playerAnimationController.nextAnimation = playerAnimationController.run_horizontal;
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

    public void Die(Player player)
    {
        player.SetState(new DieState());
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
        player.Speed *= 0.5f;
        player.IsRolling = false;
        player.SetState(new IdleState());
    }

    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction)
    {
        switch (direction)
        {
            case Direction.HORIZONTAL:
                playerAnimationController.nextAnimation = playerAnimationController.roll;
                break;
            case Direction.UP:
            case Direction.UP_DIAGONAL:
                playerAnimationController.nextAnimation = playerAnimationController.roll_up;
                break;
            case Direction.DOWN:
            case Direction.DOWN_DIAGONAL:
                playerAnimationController.nextAnimation = playerAnimationController.roll_down;
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

    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }

    public void Die(Player player)
    {
        player.SetState(new DieState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    IEnumerator ComboAttack()
    {

        yield return new WaitForSeconds(0.5f);
    }

    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction)
    {
        throw new System.NotImplementedException();
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

    public void Die(Player player)
    {
        player.SetState(new DieState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction)
    {
        throw new System.NotImplementedException();
    }
}

public class HitState : IPlayerState
{
    public void Action(Player player)
    {

    }

    public void Hit(Player player)
    {
        player.SetState(new HitState());
    }

    public void Die(Player player)
    {
        player.SetState(new DieState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction)
    {
        throw new System.NotImplementedException();
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

    public void Die(Player player)
    {
        player.SetState(new DieState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }



    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction)
    {
        throw new System.NotImplementedException();
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

    public void Die(Player player)
    {
        player.SetState(new DieState());
    }

    public void GetObject(Player player)
    {
        player.SetState(new GetObjectState());
    }

    public void SetAnimation(PlayerAnimationController playerAnimationController, Direction direction)
    {
        throw new System.NotImplementedException();
    }
}

