using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MBullet : MonoBehaviour, IDamageAble
{
    public float time = 3;
    public GameObject Bullet;
    float arrowhp = 10000;
    bool hit = false;

    public Rigidbody2D rigid;

    //public void Awake()
    //{
        //rigid = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //enemy = animator.GetComponent<Enemy>();
        
    //}

    public void Update()
    {
        if(hit==true)
        time -= Time.deltaTime;
        if (time <= 0)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage, Transform other)
    {

        arrowhp -= damage;



        if (arrowhp <= 0)
        {
            arrowhp = 0;

        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else
        {
            hit = true;
            rigid.velocity = Vector2.zero;
        }

    }
   


}

