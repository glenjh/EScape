using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface Bossstate2
{

    void Enter(Boss2 entity);
    void Execute(Boss2 entity);
    void Exit(Boss2 entity);

}
public class State2Idle : Bossstate2
{
    public void Enter(Boss2 boss)
    {
        boss.anim.SetBool("Isfollow", false);

    }
    public void Execute(Boss2 boss)
    {

    }
    public void Exit(Boss2 boss)
    {

    }
}
public class State2follow : Bossstate2
{
    public void Enter(Boss2 boss)
    {
        boss.anim.SetBool("Isfollow", true);

    }
    public void Execute(Boss2 boss)
    {
        boss.Chase();
        if(boss.bossHP<boss.MaxbossHP && boss.bossHP>0 && lightmanager.upstatus==true)
        boss.bossHP += Time.deltaTime*2;
        if (boss.bossHP > boss.MaxbossHP)
        {
            boss.bossHP = boss.MaxbossHP;
        }
        boss.hpui();
    }
    public void Exit(Boss2 boss)
    {
        boss.anim.SetBool("Isfollow", false);
    }
}
public class State2attack1 : Bossstate2
{
    public void Enter(Boss2 boss)
    {
        boss.anim.SetTrigger("Isattackable1");
        boss.skillcool--;

    }
    public void Execute(Boss2 boss)
    {

    }
    public void Exit(Boss2 boss)
    {
        if (boss.bossHP > 0 && boss.skillcool > 0)
            boss.anim.SetTrigger("Idletrigger");
        boss.cooltime = 1f;
        boss.attacking = false;
    }
}
public class State2attack2 : Bossstate2
{
    public void Enter(Boss2 boss)
    {
        boss.anim.SetTrigger("Isattackable2");
        boss.skillcool--;

    }
    public void Execute(Boss2 boss)
    {

    }
    public void Exit(Boss2 boss)
    {
        if (boss.bossHP > 0 && boss.skillcool > 0)
            boss.anim.SetTrigger("Idletrigger");
        boss.cooltime = 1f;
        boss.attacking = false;
    }
}
public class State2attack3 : Bossstate2
{
    public void Enter(Boss2 boss)
    {
        boss.anim.SetTrigger("Isattackable3");
        boss.skillcool--;

    }
    public void Execute(Boss2 boss)
    {

    }
    public void Exit(Boss2 boss)
    {
        if (boss.bossHP > 0 && boss.skillcool > 0)
            boss.anim.SetTrigger("Idletrigger");
        boss.cooltime = 1f;
        boss.attacking = false;
    }
}
public class State2skill1 : Bossstate2
{
    public void Enter(Boss2 boss)
    {
        boss.anim.SetTrigger("Isattackableskill");
        boss.cooltime = 0;
        boss.mark();



    }
    public void Execute(Boss2 boss)
    {

    }
    public void Exit(Boss2 boss)
    {
        if (boss.bossHP > 0)
        {
            boss.anim.SetTrigger("Idletrigger");
            if(lightmanager.upstatus==true)
            boss.bossHP += 10;
        }
        if (boss.bossHP > boss.MaxbossHP)
        {
            boss.bossHP = boss.MaxbossHP;
        }
        boss.hpui();
        boss.cooltime = 1f;
        boss.attacking = false;
        boss.skillnum = -1;
    }
}
public class State2dead : Bossstate2
{
    public void Enter(Boss2 boss)
    {
        boss.bossHP = 0;
        boss.hpui();
        boss.BossCol.enabled = false;
        boss.effect.Play();
        SoundManager.instance.SFXPlay("boss_dead", boss.dead_SFX);
        boss.anim.SetTrigger("Isdead");

    }
    public void Execute(Boss2 boss)
    {
        
        boss.sprite.sprite = boss.dead;
    }
    public void Exit(Boss2 boss)
    {

    }
}
