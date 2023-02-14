using System.Collections;
using UnityEngine;

public interface IPlayerState
{
    public void Action(Player player);
    public void Hit(Player player);
    public void Die(Player player);
    public void GetObject(Player player);
}

public class IdleState : IPlayerState
{
    public void Action(Player player)
    {
        if (!Input.GetAxis("Horizontal").Equals(0) || !Input.GetAxis("Vertical").Equals(0))
        {
            player.SetState(new MoveState());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new RollingState());
        }
        // else if (Input.GetMouseButtonDown(0))
        // {
        //     player.SetState(new AttackState());
        // }
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
}

public class RollingState : IPlayerState
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

    IEnumerator Rolling(Player player)
    {
        player.Speed *= 2;
        yield return new WaitForSeconds(0.5f);
        player.Speed *= 0.5f;
        player.SetState(new IdleState());

    }
}

public class AttackState : IPlayerState
{
    public void Action(Player player)
    {
        throw new System.NotImplementedException();
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
}

public class ChargeState : IPlayerState
{
    public void Action(Player player)
    {
        throw new System.NotImplementedException();
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
}

public class HitState : IPlayerState
{
    public void Action(Player player)
    {
        //throw new System.NotImplementedException();
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
}

public class DieState : IPlayerState
{
    public void Action(Player player)
    {
        throw new System.NotImplementedException();
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
}


public class GetObjectState : IPlayerState
{
    public void Action(Player player)
    {
        throw new System.NotImplementedException();
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
}

