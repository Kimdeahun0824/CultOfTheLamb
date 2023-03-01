using System.Collections;
using UnityEngine;
using State;

public enum Direction
{
    LEFT, RIGHT, UP, DOWN, UP_DIAGONAL, DOWN_DIAGONAL, HORIZONTAL
}
#region LegacyCode
// public interface IPlayerState
// {
//     public void Action(Player player);
//     public void Hit(Player player);
//     public void GetObject(Player player);
//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction);
// }

// public class IdleState : IPlayerState
// {
//     public void Action(Player player)
//     {
//         player.SetPosition(Vector3.zero);
//         if (!Input.GetAxisRaw("Horizontal").Equals(0) || !Input.GetAxisRaw("Vertical").Equals(0))
//         {
//             player.SetState(new MoveState());
//         }
//         else if (Input.GetKeyDown(KeyCode.Space))
//         {
//             player.SetState(new RollingState());
//         }
//         else if (Input.GetMouseButtonDown(0))
//         {
//             player.SetState(new AttackState());
//         }
//          else if (Input.GetMouseButtonDown(1))
//         {
//             player.SetState(new ChargeState());
//         }
//     }

//     public void Hit(Player player)
//     {
//         player.SetState(new HitState());
//     }

//     public void GetObject(Player player)
//     {
//         player.SetState(new GetObjectState());
//     }

//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
//     {
//         switch (direction)
//         {
//             case Direction.UP:
//             case Direction.UP_DIAGONAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.idle_up;
//                 skeletonAnimationHandler.PlayAnimation("Idle_Up", 0, true, 1f);
//                 break;
//             case Direction.HORIZONTAL:
//             case Direction.DOWN:
//             case Direction.DOWN_DIAGONAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.idle;
//                 skeletonAnimationHandler.PlayAnimation("Idle", 0, true, 1f);
//                 break;
//         }
//     }
// }

// public class MoveState : IPlayerState
// {
//     public void Action(Player player)
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             player.SetState(new AttackState());
//         }
//         else if (Input.GetKeyDown(KeyCode.Space))
//         {
//             player.SetState(new RollingState());
//         }
//         else if (Input.GetAxisRaw("Horizontal").Equals(0) && Input.GetAxisRaw("Vertical").Equals(0))
//         {
//             player.SetState(new IdleState());
//         }
//         else
//         {
//             Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
//             player.SetPosition(pos);
//             if (0 < pos.z)
//             {
//                 if (pos.x != 0)
//                 {
//                     player.SetDirection(Direction.UP_DIAGONAL);
//                 }
//                 else
//                 {
//                     player.SetDirection(Direction.UP);
//                 }
//             }
//             else if (pos.z < 0)
//             {
//                 if (pos.x != 0)
//                 {
//                     player.SetDirection(Direction.DOWN_DIAGONAL);
//                 }
//                 else
//                 {
//                     player.SetDirection(Direction.DOWN);
//                 }
//             }
//             else
//             {
//                 if (pos.x != 0)
//                 {
//                     player.SetDirection(Direction.HORIZONTAL);
//                 }
//             }
//         }
//     }
//     public void Hit(Player player)
//     {
//         player.SetState(new HitState());
//     }
//     public void GetObject(Player player)
//     {
//         player.SetState(new GetObjectState());
//     }
//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
//     {
//         switch (direction)
//         {
//             case Direction.UP:
//                 //playerAnimationController.nextAnimation = playerAnimationController.run_up;
//                 skeletonAnimationHandler.PlayAnimation("Run_Up", 0, true, 1f);
//                 break;
//             case Direction.UP_DIAGONAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.run_up_diagonal;
//                 skeletonAnimationHandler.PlayAnimation("Run_Up_Diagonal", 0, true, 1f);
//                 break;
//             case Direction.DOWN:
//                 //playerAnimationController.nextAnimation = playerAnimationController.run_down;
//                 skeletonAnimationHandler.PlayAnimation("Run_Down", 0, true, 1f);
//                 break;
//             case Direction.DOWN_DIAGONAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.run;
//                 skeletonAnimationHandler.PlayAnimation("Run", 0, true, 1f);
//                 break;
//             case Direction.HORIZONTAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.run_horizontal;
//                 skeletonAnimationHandler.PlayAnimation("Run_Horizontal", 0, true, 1f);
//                 break;
//             default:
//                 break;
//         }
//     }
// }

// public class RollingState : IPlayerState
// {
//     public void Action(Player player)
//     {
//         if (!player.IsRolling)
//         {
//             player.StateStartCoroutine(Rolling(player));
//         }
//         if (Input.GetMouseButtonDown(0))
//         {
//             player.SetState(new AttackState());
//         }
//     }

//     public void Hit(Player player)
//     {
//         player.SetState(new HitState());
//     }
//     public void GetObject(Player player)
//     {
//         player.SetState(new GetObjectState());
//     }

//     IEnumerator Rolling(Player player)
//     {
//         player.IsRolling = true;
//         player.Speed *= 2f;
//         yield return new WaitForSeconds(0.3f);
//         player.Speed = player.Default_Speed;
//         player.IsRolling = false;
//         player.SetState(new IdleState());
//     }

//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
//     {
//         switch (direction)
//         {
//             case Direction.HORIZONTAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.roll;
//                 skeletonAnimationHandler.PlayAnimation("Roll", 0, true, 1f);
//                 break;
//             case Direction.UP:
//             case Direction.UP_DIAGONAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.roll_up;
//                 skeletonAnimationHandler.PlayAnimation("Roll_Up", 0, true, 1f);
//                 break;
//             case Direction.DOWN:
//             case Direction.DOWN_DIAGONAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.roll_down;
//                 skeletonAnimationHandler.PlayAnimation("Roll_Down", 0, true, 1f);
//                 break;
//             default:
//                 break;
//         }
//     }
// }

// public class AttackState : IPlayerState
// {
//     public void Action(Player player)
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             player.Speed = player.Default_Speed;
//             player.SetState(new RollingState());
//             //player.StopCoroutine(Rolling)
//         }
//         if (!player.IsAttack)
//         {
//             //player.StartCoroutine(ComboAttack(player));
//             player.StartCoroutine(ComboAttack(player));
//         }
//     }

//     public void Hit(Player player)
//     {
//         player.SetState(new HitState());
//     }

//     public void GetObject(Player player)
//     {
//         player.SetState(new GetObjectState());
//     }

//     IEnumerator ComboAttack(Player player)
//     {
//         // 마우스 클릭이 되었을 때 해당 방향으로 전진하며 공격
//         // 방향키를 입력하면 해당 방향으로 전진하며 공격
//         player.AttackColloder.SetActive(true);
//         player.IsAttack = true;
//         player.Speed = 0;
//         yield return new WaitForSeconds(0.5f);
//         player.AttackColloder.SetActive(false);
//         player.IsAttack = false;
//         player.Speed = player.Default_Speed;
//         player.SetState(new IdleState());
//     }


//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
//     {
//         switch (direction)
//         {
//             case Direction.UP:
//             case Direction.UP_DIAGONAL:
//             case Direction.DOWN:
//             case Direction.DOWN_DIAGONAL:
//             case Direction.HORIZONTAL:
//                 //playerAnimationController.nextAnimation = playerAnimationController.attack_combo_1;
//                 skeletonAnimationHandler.PlayAnimation("Attack_Combo_1", 0, false, 1f);
//                 break;
//             default:
//                 break;
//         }
//     }
// }

// public class ChargeState : IPlayerState
// {
//     public void Action(Player player)
//     {
//     }
//     public void Hit(Player player)
//     {
//         player.SetState(new HitState());
//     }
//     public void GetObject(Player player)
//     {
//         player.SetState(new GetObjectState());
//     }
//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
//     {
//         //skeletonAnimationHandler.PlayAnimation("Die", 0, false, 1f);
//     }
// }

// public class HitState : IPlayerState
// {
//     public void Action(Player player)
//     {
//         if (!player.IsHit)
//         {
//             player.StartCoroutine(playerHit(player));
//         }
//     }

//     public void Hit(Player player)
//     {
//         player.SetState(new HitState());
//     }
//     public void GetObject(Player player)
//     {
//         player.SetState(new GetObjectState());
//     }

//     IEnumerator playerHit(Player player)
//     {
//         player.IsHit = true;
//         player.Speed *= -0.5f;
//         yield return new WaitForSeconds(0.5f);
//         player.IsHit = false;
//         player.Speed *= -2f;
//         player.SetState(new IdleState());
//     }
//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
//     {
//         skeletonAnimationHandler.PlayAnimation("KnockBack", 0, false, 1f);
//     }
// }

// public class DieState : IPlayerState
// {
//     public void Action(Player player)
//     {
//     }

//     public void Hit(Player player)
//     {
//         player.SetState(new HitState());
//     }
//     public void GetObject(Player player)
//     {
//         player.SetState(new GetObjectState());
//     }

//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
//     {
//         skeletonAnimationHandler.PlayAnimation("Die", 0, false, 1f);
//     }
// }

// public class GetObjectState : IPlayerState
// {
//     public void Action(Player player)
//     {

//     }

//     public void Hit(Player player)
//     {
//         player.SetState(new HitState());
//     }
//     public void GetObject(Player player)
//     {
//         player.SetState(new GetObjectState());
//     }

//     public void SetAnimation(SkeletonAnimationHandler skeletonAnimationHandler, Direction direction)
//     {
//         //skeletonAnimationHandler.PlayAnimation("", 0, false, 1f);
//     }
//}
#endregion

public class Player_Idle_State : StateBase
{
    private Player player;
    public Player_Idle_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        player.IsEventComplete = false;
        Direction direction = player.GetDirection();
        switch (direction)
        {
            case Direction.UP:
            case Direction.UP_DIAGONAL:
                player.SetAnimation("Idle_Up", 0, true, 1f);
                break;
            case Direction.HORIZONTAL:
            case Direction.DOWN:
            case Direction.DOWN_DIAGONAL:
            default:
                player.SetAnimation("Idle", 0, true, 1f);
                break;
        }
    }
    public override void UpdateState()
    {
        if (!Input.GetAxisRaw("Horizontal").Equals(0) || !Input.GetAxisRaw("Vertical").Equals(0))
        {
            player.SetState(new Player_Move_State(player));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new Player_Rolling_State(player));
        }
        else if (Input.GetMouseButtonDown(0))
        {
            player.SetState(new Player_Attack_Combo_1State(player));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            player.SetState(new Player_CastDown_State(player));
        }
    }
    public override void OnExit()
    {
        player.IsEventComplete = true;
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
    }
}

public class Player_Move_State : StateBase
{
    private Player player;
    private Direction previousDirection;
    public Player_Move_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        previousDirection = player.GetDirection();
        switch (previousDirection)
        {
            case Direction.UP:
                player.SetAnimation("Run_Up", 0, true, 1f);
                break;
            case Direction.UP_DIAGONAL:
                player.SetAnimation("Run_Up_Diagonal", 0, true, 1f);
                break;
            case Direction.DOWN:
                player.SetAnimation("Run_Down", 0, true, 1f);
                break;
            case Direction.DOWN_DIAGONAL:
                player.SetAnimation("Run", 0, true, 1f);
                break;
            case Direction.HORIZONTAL:
            default:
                player.SetAnimation("Run_Horizontal", 0, true, 1f);
                break;
        }
        player.IsEventComplete = false;
    }
    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.SetState(new Player_Attack_Combo_1State(player));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            player.SetState(new Player_CastDown_State(player));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new Player_Rolling_State(player));
        }
        else if (Input.GetAxisRaw("Horizontal").Equals(0) && Input.GetAxisRaw("Vertical").Equals(0))
        {
            player.SetState(new Player_Idle_State(player));
            player.SetPosition(Vector3.zero);
        }
        else
        {
            Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
            player.SetPosition(pos);
            if (0 < pos.x)
            {
                player.IsFlip = true;
            }
            else
            {
                player.IsFlip = false;
            }
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


        Direction direction = player.GetDirection();
        if (previousDirection != direction)
        {
            previousDirection = direction;
            switch (direction)
            {
                case Direction.UP:
                    player.SetAnimation("Run_Up", 0, true, 1f);
                    break;
                case Direction.UP_DIAGONAL:
                    player.SetAnimation("Run_Up_Diagonal", 0, true, 1f);
                    break;
                case Direction.DOWN:
                    player.SetAnimation("Run_Down", 0, true, 1f);
                    break;
                case Direction.DOWN_DIAGONAL:
                    player.SetAnimation("Run", 0, true, 1f);
                    break;
                case Direction.HORIZONTAL:
                    player.SetAnimation("Run_Horizontal", 0, true, 1f);
                    break;
                default:
                    break;
            }
        }
    }
    public override void OnExit()
    {
        player.IsEventComplete = true;
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
    }
}

public class Player_DungeonMove_State : StateBase
{
    private Player player;
    Vector3 startPos = default;
    float distance = default;
    public Player_DungeonMove_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        Direction direction = player.GetDirection();
        startPos = player.transform.position;
        Debug.Log($"DungeonMove State Debug(startPos : {startPos})");
        Vector3 pos = default;
        switch (direction)
        {
            case Direction.LEFT:
                player.IsFlip = false;
                player.SetAnimation("Run_Horizontal", 0, true, 1f);
                pos = new Vector3(-1f, 0f, 0f);
                break;
            case Direction.RIGHT:
                player.IsFlip = true;
                player.SetAnimation("Run_Horizontal", 0, true, 1f);
                pos = new Vector3(1f, 0f, 0f);
                break;
            case Direction.UP:
                player.SetAnimation("Run_Up", 0, true, 1f);
                pos = new Vector3(0f, 0f, 1f);
                break;
            case Direction.DOWN:
                player.SetAnimation("Run_Down", 0, true, 1f);
                pos = new Vector3(0f, 0f, -1f);
                break;
            default:
                break;
        }
        player.SetPosition(pos);
        player.Speed = player.Default_Speed;
        GameManager.Instance.RoomWallOff();
        player.IsEventComplete = false;
    }
    public override void UpdateState()
    {
        distance = (startPos - player.transform.position).magnitude;
        if (5 <= distance)
        {
            player.SetState(new Player_Idle_State(player));
        }
    }
    public override void OnExit()
    {
        GameManager.Instance.RoomMoveComplete();
        player.SetPosition(Vector3.zero);
        player.IsEventComplete = true;
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
    }
}

public class Player_Rolling_State : StateBase
{
    private Player player;
    public Player_Rolling_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        player.IsRolling = true;
        player.Speed *= 2f;
        Direction direction = player.GetDirection();
        Vector3 pos = default;
        switch (direction)
        {
            case Direction.HORIZONTAL:
            default:
                player.SetAnimation("Roll", 0, false, 1f);
                if (player.IsFlip)
                {
                    pos = new Vector3(1f, 0f, 0f).normalized;
                }
                else
                {
                    pos = new Vector3(-1f, 0f, 0f).normalized;
                }
                break;
            case Direction.UP:
                player.SetAnimation("Roll_Up", 0, false, 1f);
                pos = new Vector3(0f, 0f, 1f).normalized;
                break;
            case Direction.UP_DIAGONAL:
                player.SetAnimation("Roll_Up", 0, false, 1f);
                if (player.IsFlip)
                {
                    pos = new Vector3(1f, 0f, 1f).normalized;
                }
                else
                {
                    pos = new Vector3(-1f, 0f, 1f).normalized;
                }
                break;
            case Direction.DOWN:
                player.SetAnimation("Roll_Down", 0, false, 1f);
                pos = new Vector3(0f, 0f, -1f).normalized;
                break;
            case Direction.DOWN_DIAGONAL:
                player.SetAnimation("Roll_Down", 0, false, 1f);
                if (player.IsFlip)
                {
                    pos = new Vector3(1f, 0f, -1f).normalized;
                }
                else
                {
                    pos = new Vector3(-1f, 0f, -1f).normalized;
                }
                break;
        }
        player.SetPosition(pos);
        player.IsEventComplete = false;
    }
    public override void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.SetState(new Player_RollingAttack_State(player));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            player.SetState(new Player_CastDown_State(player));
        }
    }
    public override void OnExit()
    {
        player.IsRolling = false;
        player.Speed = player.Default_Speed;
        player.SetPosition(Vector3.zero);
        player.IsEventComplete = true;
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
        if (player.IsRolling)
        {
            player.SetState(new Player_Idle_State(player));
        }
    }
}

public class Player_Attack_Combo_1State : StateBase
{
    private Player player;
    public Player_Attack_Combo_1State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        player.IsEventComplete = false;
        Direction direction = player.GetDirection();
        switch (direction)
        {
            case Direction.UP:
            case Direction.UP_DIAGONAL:
            case Direction.DOWN:
            case Direction.DOWN_DIAGONAL:
            case Direction.HORIZONTAL:
            default:
                player.SetAnimation("Attack_Combo_1", 0, false, 1f);
                break;
        }
        player.AttackColloder.SetActive(true);
        player.IsAttack = true;
        player.SetPosition(Vector3.zero);
    }
    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new Player_Rolling_State(player));
        }
    }
    public override void OnExit()
    {
        player.IsEventComplete = true;
        player.AttackColloder.SetActive(false);
        player.IsAttack = false;
        player.SetPosition(Vector3.zero);
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
        if (player.IsAttack)
        {
            player.SetState(new Player_Idle_State(player));
        }
    }
}

public class Player_RollingAttack_State : StateBase
{
    private Player player;
    public Player_RollingAttack_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        player.IsEventComplete = false;
        Direction direction = player.GetDirection();
        switch (direction)
        {
            case Direction.UP:
            case Direction.UP_DIAGONAL:
            case Direction.DOWN:
            case Direction.DOWN_DIAGONAL:
            case Direction.HORIZONTAL:
            default:
                player.SetAnimation("Attack_Combo_1", 0, false, 1f);
                break;
        }
        player.AttackColloder.SetActive(true);
        player.IsAttack = true;
        player.Speed = 0;
    }
    public override void UpdateState()
    {
    }
    public override void OnExit()
    {
        player.IsEventComplete = true;
        player.AttackColloder.SetActive(false);
        player.IsAttack = false;
        player.Speed = player.Default_Speed;
        player.SetPosition(Vector3.zero);
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
        if (player.IsAttack)
        {
            player.SetState(new Player_Idle_State(player));
        }
    }
}

public class Player_CastDown_State : StateBase
{
    private Player player;
    public Player_CastDown_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        player.IsEventComplete = false;
        player.SetAnimation("CastDown", 0, true, 1f);
        player.SetPosition(Vector3.zero);
    }
    public override void UpdateState()
    {
        if (Input.GetMouseButtonUp(1))
        {
            player.SetState(new Player_CastUp_State(player));
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetState(new Player_Rolling_State(player));
        }
    }
    public override void OnExit()
    {
        player.IsEventComplete = true;
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
    }
}

public class Player_CastUp_State : StateBase
{
    private Player player;
    public Player_CastUp_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        player.IsEventComplete = false;
        player.SetAnimation("CastUp", 0, false, 1f);
    }
    public override void UpdateState()
    {
    }
    public override void OnExit()
    {
        player.IsEventComplete = true;
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
        player.SetState(new Player_Idle_State(player));
    }
}

public class Player_Hit_State : StateBase
{
    private Player player;
    public Player_Hit_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        player.IsEventComplete = false;
        player.IsHit = true;
        player.Speed *= -0.5f;
        Vector3 pos = default;
        if (player.IsFlip)
        {
            pos = new Vector3(1f, 0f, 0f);
        }
        else
        {
            pos = new Vector3(-1f, 0f, 0f);
        }
        player.SetPosition(pos);
        player.SetAnimation("KnockBack", 0, false, 1f);
    }
    public override void UpdateState()
    {
    }
    public override void OnExit()
    {
        player.IsEventComplete = true;
        player.IsHit = false;
        player.Speed = player.Default_Speed;
        player.SetPosition(Vector3.zero);
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {

        player.SetState(new Player_Idle_State(player));
    }
}

public class Player_Die_State : StateBase
{
    private Player player;
    public Player_Die_State(Player player_)
    {
        this.player = player_;
    }
    public override void OnEnter()
    {
        player.IsEventComplete = false;
        player.IsDie = true;
        player.Speed = 0;
        player.SetPosition(Vector3.zero);
        player.SetAnimation("Die", 0, false, 1f);
    }
    public override void UpdateState()
    {
    }
    public override void OnExit()
    {
        player.IsEventComplete = true;
    }
    public override void Action()
    {
    }
    public override void ChangeState()
    {
    }
}

