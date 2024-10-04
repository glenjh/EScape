using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy : MonoBehaviour, IDamageAble
{
    // Start is called before the first frame update
    public float speed;
    public int health;
    public int maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Vector2 inputVec;
    public Rigidbody2D target;

    bool isLive; 

    Rigidbody2D rigid;
    Collider2D coll;
    SpriteRenderer spriter;
    Animator anim;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        // anim.SetFloat("speed", inputVec.magnitude); : require add speed

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        // target = GameManager.instance.player.GetComponent<Rigidbody2D>(); :need player
        isLive = true;
   

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            Dead();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait; // delay
        //Vector3 playerPos = GameManager.Instance.player.transform.position; :need player
        //Vector3 dirVec = transform.position - playerPos; :need player
        //rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse); :need player
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage, Transform other)
    {
        health -= damage;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        health = data.health;

    }
    
}
public class SpawnData
{
    public int spriteType;
    public int health;
    public float speed;
}