using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PlayerState
{
   public PlayerState() {}

   public virtual void Enter(Player player) {}

   public virtual void Update(Player player) {}

   public virtual void Exit(Player player) {}
}

public class Idle : PlayerState
{
   public Idle() {}

   public Idle(PlayerStateType stateType)
   {
      stateType = PlayerStateType.Idle;
   }

   public override void Enter(Player player)
   {
      player.anim.SetBool("isMoving", false);
   }

   public override void Update(Player player)
   {
      player.SetMove();
      player.SetDash();
      player.PotionHeal();
   }
   
   public override void Exit(Player player) {}
}

public class Move : PlayerState
{
   public Move() {}

   public Move(PlayerStateType stateType)
   {
      stateType = PlayerStateType.Move;
   }

   public override void Enter(Player player)
   {
      player.anim.SetBool("isMoving", true);
   }

   public override void Update(Player player)
   {
      player.Move();
      player.SetDash();
      player.PotionHeal();
   }

   public override void Exit(Player player)
   {
      player.anim.SetBool("isMoving", false);
   }
}

public class Dash : PlayerState
{
   public Dash() {}

   public Dash(PlayerStateType stateType)
   {
      stateType = PlayerStateType.Dash;
   }

   public override void Enter(Player player)
   {
      player.StartCoroutine("Dash");
   }

   public override void Update(Player player)
   {
      player.PotionHeal();
   }
   
   public override void Exit(Player player) {}
   
}

public class Stunned : PlayerState
{
   public Stunned() {}

   public Stunned(PlayerStateType stateType)
   {
      stateType = PlayerStateType.Stunned;
   }

   public override void Enter(Player player)
   {
      player.sprite.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
      player.StartCoroutine(player.Stun());
   }

   public override void Update(Player player) {}

   public override void Exit(Player player)
   {
      player.sprite.color = new Color(1, 1, 1, 1);
   }
}

public class Dead : PlayerState
{
   public Dead() {}

   public Dead(PlayerStateType stateType)
   {
      stateType = PlayerStateType.Dead;
   }

   public override void Enter(Player player)
   {
      player.anim.SetTrigger("isDead");
      player.rigid.constraints = RigidbodyConstraints2D.FreezeAll;
      player.rigid.velocity = new Vector2(0, 0);
   }

   public override void Update(Player player)
   {
   }
   
   public override void Exit(Player player) {}
   
}

