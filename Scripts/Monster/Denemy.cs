using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Denemy : MonoBehaviour, IDamageAble
{
    public Transform player;
    public float speed;
    public int Health;
    //public Rigidbody2D target;
    Animator animator;
    SpriteRenderer spriter;
    Rigidbody2D rigid;
    Collider2D coll;
    [Header("SFX")]
    public AudioClip hit_SFX;
    public AudioClip dead_SFX;
    public bool isDead = false;

    public float atkCooltime = 2;
    public float atkDelay = 1;

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

        spriter.flipX = player.position.x < rigid.position.x;

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
                SoundManager.instance.SFXPlay("dead_sfx", dead_SFX);
                animator.SetBool("IsDead", true);
                //Dead();
            }
        }

    }

    public void Dead()
    {
        isDead = true;



        Destroy(gameObject);

    }
}
