using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class collider2_boss2 : MonoBehaviour
{
    public CapsuleCollider2D check2;

    // Start is called before the first frame update
    void Start()
    {
        check2 = GetComponent<CapsuleCollider2D>();

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss2.attackrange2 = true;

        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Boss2.attackrange2 = false;
        }
    }


}