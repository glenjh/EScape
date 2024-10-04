using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Bossstate
{

   void Enter(Boss1 entity) ;
   void Execute(Boss1 entity) ;
    void Exit(Boss1 entity) ;

}
public class StateIdle : Bossstate
{
    public void Enter(Boss1 boss)
    {
        boss.anim.SetBool("Isfollow", false);
        
    }
    public void Execute(Boss1 boss)
    {

    }
    public void Exit(Boss1 boss)
    {
        
    }
}
public class Statefollow : Bossstate
{
    public void Enter(Boss1 boss)
    {
       boss.anim.SetBool("Isfollow", true);
        

    }
    public void Execute(Boss1 boss)
    {
        boss.Chase();
    }
    public void Exit(Boss1 boss)
    {
        boss.anim.SetBool("Isfollow", false);
    }
}
public class Stateattack1 : Bossstate
{
    public void Enter(Boss1 boss)
    {
        boss.anim.SetTrigger("Isattackable1");
        boss.skillcool--;

    }
    public void Execute(Boss1 boss)
    {
        
    }
    public void Exit(Boss1 boss)
    {
        if(boss.bossHP> 0 && boss.skillcool>0)
        boss.anim.SetTrigger("Idletrigger");
        boss.cooltime = 1f; 
        boss.attacking = false;
    }
}
public class Stateattack2 : Bossstate
{
    public void Enter(Boss1 boss)
    {
        boss.anim.SetTrigger("Isattackable2");
        boss.skillcool--;
    }
    public void Execute(Boss1 boss)
    {

    }
    public void Exit(Boss1 boss)
    {
        if (boss.bossHP > 0 && boss.skillcool>0 )
            boss.anim.SetTrigger("Idletrigger");
        boss.cooltime = 1f;
        boss.attacking = false;
    }
}
public class Stateattack3 : Bossstate
{
    public void Enter(Boss1 boss)
    {
        boss.anim.SetTrigger("Isattackable3");
        boss.skillcool--;

    }
    public void Execute(Boss1 boss)
    {

    }
    public void Exit(Boss1 boss)
    {
        if (boss.bossHP > 0 && boss.skillcool>0)
            boss.anim.SetTrigger("Idletrigger");
        boss.cooltime = 1f;
        boss.attacking = false;
    }
}

public class Stateskill1 : Bossstate
{
    public void Enter(Boss1 boss)
    {
        boss.anim.SetTrigger("Isattackableskill");
        boss.cooltime = 0;
        
    }
    public void Execute(Boss1 boss)
    {
        if (boss.skillchasing == true)
        {
            boss.Skillchase();
        }
        if (boss.skillnum == 0)
        {
           
            boss.anim.SetTrigger("skill1");
        }
        else if (boss.skillnum == 1)
        {
           
            boss.anim.SetTrigger("skill2");
        }
        else if (boss.skillnum == 2)
        { 
            boss.anim.SetTrigger("skill3");
        }
    }
    public void Exit(Boss1 boss)
    {
        if (boss.bossHP > 0)
            boss.anim.SetTrigger("Idletrigger");
        boss.cooltime = 1f;
        boss.attacking = false;
        boss.skillnum = -1;
        
       
    }
}

public class Statedead : Bossstate
{
    public void Enter(Boss1 boss)
    {
        boss.BossCol.enabled = false;
        boss.effect.Play();
        boss.anim.SetTrigger("Isdead");
        SoundManager.instance.SFXPlay("boss_dead",boss.dead_SFX);


    }
    public void Execute(Boss1 boss)
    {
        boss.sprite.sprite = boss.dead;
    }
    public void Exit(Boss1 boss)
    {

    }
}

