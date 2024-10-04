using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider1 : MonoBehaviour
{

    public CapsuleCollider2D check1;
    private void Start()
    {
        
        
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss1.attackrange1 = true;
           
            
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss1.attackrange1 = false;
        }
    }



}
