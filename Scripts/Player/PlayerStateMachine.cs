using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine
{
    Player player;
    Dictionary<PlayerStateType, PlayerState> states = new Dictionary<PlayerStateType, PlayerState>();
    PlayerState currState;

    public PlayerStateMachine(PlayerStateType ps, Player player)
    {
        this.player = player;
        states.Add(ps, StateManager.GetState(ps));
        ChangeState(ps);
    }

    public void ChangeState(PlayerStateType ps)
    {
        currState?.Exit(player);
        if (!states.ContainsKey(ps))
        {
            AddState(ps);
        }
        currState = states[ps];
        player._stateType = ps;
        currState.Enter(player);
    }

    public void Action()
    {
        currState.Update(player);
    }

    public void AddState(PlayerStateType ps)
    {
        states.Add(ps, StateManager.GetState(ps));
    }

    public PlayerState GetCurrState()
    {
        return currState;
    }

    public void DeleteState(PlayerStateType ps)
    {
        states.Remove(ps);
    }
}

public static class StateManager
{
    public static PlayerState GetState(PlayerStateType ps)
    {
        if(ps != null){
            switch (ps)
            {
                case PlayerStateType.Idle:
                    return new Idle(ps);

                case PlayerStateType.Move:
                    return new Move(ps);
            
                case PlayerStateType.Dash:
                    return new Dash(ps);
                
                case PlayerStateType.Stunned:
                    return new Stunned(ps);

                case PlayerStateType.Dead:
                    return new Dead(ps);

                default:
                    return null;
            }
        }
        return null;
    }
}