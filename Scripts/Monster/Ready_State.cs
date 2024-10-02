using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready_State : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(enemy.player.position, enemyTransform.position) <= enemy.MaxD && Vector2.Distance(enemy.player.position, enemyTransform.position) >= enemy.MinD)
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
            
        }

        //if (Vector2.Distance(enemy.player.position, enemyTransform.position) <= 4)
        //{
            //if (enemy.atkDelay <= 0)
                //animator.SetTrigger("Attack");
        //}

        if (Vector2.Distance(enemy.player.position, enemyTransform.position) <= 1)
        {
           animator.SetTrigger("Attack");
        }

        // if (enemy.Health <= 0)
        //  animator.SetBool("IsDead", true);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
