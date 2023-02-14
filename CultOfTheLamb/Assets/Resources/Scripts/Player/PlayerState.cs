using System.Collections;
using UnityEngine;

public interface IPlayerState
{
    public void Action(Player player);
    public void Hit(Player player);
    public void Die(Player player);
    public void GetObject(Player player);
    public void SetAnimation(PlayerAnimationController playerAnimationController);
}

public class IdleState : IPlayerState
{
    public void Action(Player player)
    {
        player.SetPosition(Vector3.zero);
        if (!Input.GetAxis("Horizontal").Equals(0) || !Input.GetAxis("Vertical").Equals(0))
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

    public void SetAnimation(PlayerAnimationController playerAnimationController)
    {
        playerAnimationController.nextAnimation = playerAnimationController.idle;
    }
}

public class MoveState : IPlayerState
{
    public void Action(Player player)
    {
        if (Input.GetAxis("Horizontal").Equals(0) && Input.GetAxis("Vertical").Equals(0))
        {
            player.SetState(new IdleState());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new RollingState());
        }
        else
        {
            Vector3 pos = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            player.SetPosition(pos);
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

    public void SetAnimation(PlayerAnimationController playerAnimationController)
    {
        playerAnimationController.nextAnimation = playerAnimationController.run;
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
        player.Speed *= 2.0f;
        yield return new WaitForSeconds(0.5f);
        player.Speed *= 0.5f;
        player.IsRolling = false;
        player.SetState(new IdleState());

    }

    public void SetAnimation(PlayerAnimationController playerAnimationController)
    {
        playerAnimationController.nextAnimation = playerAnimationController.roll;
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

    public void SetAnimation(PlayerAnimationController playerAnimationController)
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

    public void SetAnimation(PlayerAnimationController playerAnimationController)
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

    public void SetAnimation(PlayerAnimationController playerAnimationController)
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

    public void SetAnimation(PlayerAnimationController playerAnimationController)
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

    public void SetAnimation(PlayerAnimationController playerAnimationController)
    {
        throw new System.NotImplementedException();
    }
}

