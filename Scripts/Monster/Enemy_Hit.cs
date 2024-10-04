using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hit : MonoBehaviour
{
    Enemy enemy;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        enemy = animator.GetComponent<Enemy>();
        enemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        
            animator.SetTrigger("Hit");
            Debug.Log("Hit Trigger Activated!");

           
        
    }
}
