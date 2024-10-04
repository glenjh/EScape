using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWalk_State : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    public Vector2 inputVec;
    Rigidbody2D rigid;



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
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.player.position, Time.deltaTime * enemy.speed);


     
        }

        else
        {
            animator.SetBool("IsWalk", false);
        }

  


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
