using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Hit_State : StateMachineBehaviour
{

    Transform enemyTransform;
    Animator animator;
    Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enemy.Health <= 0)
            animator.SetBool("IsDead", true);

        else
            animator.SetBool("IsDead", false);
            animator.SetBool("IsWalk", false);

   
    }

 

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }

    
}
