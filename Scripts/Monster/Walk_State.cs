using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Walk_State : StateMachineBehaviour
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
        

            //Vector2 dirVec = enemy.player.position - enemyTransform.position;
            //Vector2 nextVec = dirVec.normalized * enemy.speed * Time.fixedDeltaTime;
            //rigid.MovePosition(rigid.position + nextVec);
            //rigid.velocity = Vector2.zero;
        }

        else 
        {
            animator.SetBool("IsWalk", false);
        }

        //if (enemy.atkDelay <= 0)
        //  animator.SetTrigger("Attack");

        //if (enemy.Health <= 0)
        // animator.SetBool("IsDead", true);

        if (Vector2.Distance(enemy.player.position, enemyTransform.position) <= 1)
        {
            animator.SetTrigger("Attack");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
