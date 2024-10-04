using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_State : StateMachineBehaviour
{
    Transform enemyTransform;
    
    Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyTransform = animator.transform;
        enemy = animator.GetComponent<Enemy>();
        enemy.GetComponent<Animator>();



    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(enemy.player.position, enemyTransform.position) >= enemy.MinD)
            animator.SetBool("IsWalk", true);
        else
            animator.SetBool("IsWalk", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.atkDelay = enemy.atkCooltime;
    }

}
