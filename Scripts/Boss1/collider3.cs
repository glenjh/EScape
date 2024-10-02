using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider3 : MonoBehaviour
{
    public CircleCollider2D check3;
    public Player player;
    private Boss1statemachine statemachine;

    private void Start()
    {
        check3 = GetComponent<CircleCollider2D>();
      
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss1.attackrange3 = true;
            
        }
       
    }
   
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss1.attackrange3 = false;
        }
    }
    
}
