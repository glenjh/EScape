using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageAble
{
    public Transform player;
    public float speed;
    public int Health;
    //public Rigidbody2D target;
    Animator animator;
    SpriteRenderer spriter;
    Rigidbody2D rigid;
    Collider2D coll;
    public CapsuleCollider2D Collider;
    [Header("SFX")]
    public AudioClip hit_SFX;
    public AudioClip dead_SFX;
    public bool isDead = false;

    public float atkCooltime = 3;
    public float atkDelay = 2;

    public float MinD = 0;
    public float MaxD = 8;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

      

    }

    // Update is called once per frame
    void Update()
    {
        if (atkDelay >= 0)  
            atkDelay -= Time.deltaTime;

        
    }

    void FixedUpdate()
    {

       
    }

    void LateUpdate()
    {
      
        
        //spriter.flipX = player.position.x < rigid.position.x;
        float x = player.position.x - rigid.position.x;
        if(x< 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else
        {
            spriter.flipX = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }

    public void TakeDamage(int damage, Transform other)
    {
           // Health -= (int)damage;

        if (!isDead)
        {
            
            Health -= (int)damage;
            SoundManager.instance.SFXPlay("hit_sfx", hit_SFX);
            animator.SetTrigger("Hit");
            
            if (Health <= 0)
            {
                Destroy(Collider);
                SoundManager.instance.SFXPlay("dead_sfx", dead_SFX);
                animator.SetBool("IsDead", true);
                //Dead();
            }
        }

    }

    public void Dead()
    {
        isDead = true;


        // animator.SetBool("IsDead", true);



        Destroy(gameObject);
        
    }

    public MBulletControll MBulletControll;

    public void CallChildEvent()
    {
        MBulletControll.fireon();
    }

}
