using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1statemachine
{
    Boss1 boss1;
    public Bossstate currentstate { get; private set; }
    public Boss1statemachine(Bossstate state,Boss1 boss)
    {
        this.boss1 = boss;
        currentstate = state;
    }
    public void SetState(Bossstate state)
    {
        if (currentstate == state|| currentstate ==null)
        {
            return;
        }
        currentstate.Exit(boss1);
        currentstate = state;
        currentstate.Enter(boss1);

    }
    public void Updated()
    {
        currentstate.Execute(boss1);
    }
}
