using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2statemachine : MonoBehaviour
{
    Boss2 boss2;
    public Bossstate2 currentstate2 { get; private set; }
    public Boss2statemachine(Bossstate2 state, Boss2 boss)
    {
        this.boss2 = boss;
        currentstate2 = state;
    }
    public void SetState(Bossstate2 state)
    {
        if (currentstate2 == state || currentstate2 == null)
        {
            return;
        }
        currentstate2.Exit(boss2);
        currentstate2 = state;
        currentstate2.Enter(boss2);

    }
    public void Updated()
    {
        currentstate2.Execute(boss2);
    }
}
