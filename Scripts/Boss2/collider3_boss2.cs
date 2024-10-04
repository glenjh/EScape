using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class collider3_boss2 : MonoBehaviour
{
    public CapsuleCollider2D check3;

    // Start is called before the first frame update
    void Start()
    {
        check3 = GetComponent<CapsuleCollider2D>();

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss2.attackrange3 = true;

        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss2.attackrange3 = false;
        }
    }


}