using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Boss1state
{
    Idle,
    Follow,
    Attack1,
    Attack2,
    Attack3,
    Skill1,
    Dead
}
public class Boss1 : MonoBehaviour, IDamageAble
{

    [SerializeField] public ParticleSystem effect;
    public Rigidbody2D rigid;
    public Rigidbody2D target;
    public CapsuleCollider2D BossCol;
    //public CapsuleCollider2D collider1;
    //public CapsuleCollider2D collider2;
   //er public CircleCollider2D collider3;
    public SpriteRenderer sprite;
    public Animator anim;
    public Player player;
    private Boss1statemachine statemachine;
    public GameObject boss1;
    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;
    public Image hp;
    public Text hptext;
    public Sprite attack_0;
    public Sprite dead;
    public float bossHP = 100;
    public float MaxbossHP = 100;
    public float movingspeed = 3f;
    public int skillcool = 3;
    public float cooltime = 0f;
    public static bool attackrange1 = false;
    public static bool attackrange2 = false;
    public static bool attackrange3 = false;
    public bool attackable = false;
    public bool Isattackable1 = true;
    public bool Isattackable2 = true;
    public bool Isattackable3 = true;
    public bool Isattackable4 = true;
    public bool Isattackableskill = false;
    public bool Isattackableskill2 = false;
    public int random;
    public bool attacking = false;
    public int skillnum;
    public bool skillchasing = false;
    public float deadtime = 3f;
    [Header("SFX")]
    public AudioClip skill_SFX;
    public AudioClip dead_SFX;




    private Dictionary<Boss1state, Bossstate> states = new Dictionary<Boss1state, Bossstate>();
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        BossCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Bossstate idle = new StateIdle();
        Bossstate follow = new Statefollow();
        Bossstate attack1 = new Stateattack1();
        Bossstate attack2 = new Stateattack2();
        Bossstate attack3 = new Stateattack3();
        Bossstate skill1 = new Stateskill1();
        Bossstate dead = new Statedead();
        states.Add(Boss1state.Idle, idle);
        states.Add(Boss1state.Follow, follow);
        states.Add(Boss1state.Attack1, attack1);
        states.Add(Boss1state.Attack2, attack2);
        states.Add(Boss1state.Attack3, attack3);
        states.Add(Boss1state.Skill1, skill1);
        states.Add(Boss1state.Dead, dead);
        statemachine = new Boss1statemachine(idle,this);
        random = -1;
        skillnum = -1;
        hpui();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        target = playerObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (statemachine.currentstate == states[Boss1state.Dead])
        {
           
            return;
        }
        if (cooltime > 0)
        {
            cooltime -= Time.deltaTime;
        }
        
        if (bossHP > 0)
        {
            cycle();
            float x = target.transform.position.x - rigid.position.x;
            if (x < 0)
            {
               // sprite.flipX = true;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                sprite.flipX = false;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            
            sprite.sprite = dead;
        }
        
        statemachine.Updated();
    }
    void cycle()
    {
       
        if (cooltime > 0)
        {
           
            statemachine.SetState(states[Boss1state.Idle]);
        }
         else
        {
            if (random == -1)
            {
               
                if (skillcool <= 0)
                {
                    
                    statemachine.SetState(states[Boss1state.Skill1]);
                    
                }
                else
                {
                    random = UnityEngine.Random.Range(0, 3);
                    statemachine.SetState(states[Boss1state.Idle]);
                }
            }
            if (random == 0)
            {
                if (attackrange1 == false)
                {
                    if (attacking == false)
                    {
                        statemachine.SetState(states[Boss1state.Follow]);
                        collider1.tag = "Untagged";
                    }
                }
                else
                {
                    statemachine.SetState(states[Boss1state.Attack1]);
                    attacking = true;
                    
                }

            }
            else if (random == 1)
            {
                if (attackrange2 == false)
                {
                    if (attacking == false)
                    {
                        statemachine.SetState(states[Boss1state.Follow]);
                        collider2.tag = "Untagged";
                    }
                }
                else
                {
                        attacking = true;
                        statemachine.SetState(states[Boss1state.Attack2]);
                    
                }

            }else if (random == 2)
            {
                if (attackrange3 == false)
                {
                    if (attacking == false)
                    {
                        statemachine.SetState(states[Boss1state.Follow]);
                        collider3.tag = "Untagged";
                    }
                }
                else
                {
                    attacking = true;
                    statemachine.SetState(states[Boss1state.Attack3]);
                }
            }
        }
    }

    public void TakeDamage(int damage, Transform other)
    {
       
            bossHP -= damage;
            StartCoroutine("AlphaBlink");
            hpui();

        if (bossHP <= 0)
        {
            bossHP = 0;
            statemachine.SetState(states[Boss1state.Dead]);
        }
    }
    
    // public void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col.gameObject.CompareTag("Projectile"))
    //     {
    //         bossHP--;
    //         if (bossHP <= 0)
    //         {
    //             statemachine.SetState(states[Boss1state.Dead]);
    //         }
    //     }
    // }
    public void Chase()
    {
        Vector2 dirvec = target.position - rigid.position;
        Vector2 nextVec = dirvec.normalized * movingspeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
   public  void skillchase()
    {
        skillchasing = true;
        
    }
    
    
    public void Skillchase()
    {
        Vector3 dirVec = (target.position - rigid.position).normalized;
        Vector2 nextVec = dirVec * 8 * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    public void attackon()
    {
      
        attackable = true;
        if (random == 0)
        {
            collider1.tag = "skill";
           
        }
        else if (random == 1)
        {
            collider2.tag = "skill";
            
        }
        else if(random==2)
        {
            collider3.tag = "skill";
           
        }

    }
    public void attackoff()
    {
       attackable = false;
        collider1.tag = "Untagged";
        collider2.tag = "Untagged";
        collider3.tag = "Untagged";
        

    }
    public void attackend()
    {
        random = -1;
    }
    public void TakeDamage(float damage)
    {
        bossHP -= damage;
    }
    public void skillon()
    {
        attackable = true;
        if (skillnum == 0)
        {
            collider1.tag = "skill";

        }
        else if (skillnum== 1)
        {
            collider2.tag = "skill";

        }
        else if (skillnum == 2)
        {
            collider3.tag = "skill";

        }
    }
    public void skilloff()
    {
        attackable = false;
        collider1.tag = "Untagged";
        collider2.tag = "Untagged";
        collider3.tag = "Untagged";
        skillchasing = false;


    }
    public void skillend0()
    {
        skillnum = 0;
        stun();
    }
    public void skillend1()
    {
        skillnum=1;
    }
    public void skillend2()
    {
        skillnum = 2;
    }
    public void skillend3()
    {
        skillcool = 3;
        random = -1;
    }
    public void stun()
    {
        player.SetStun(3f);
        player.rigid.velocity = Vector2.zero;
    }
    public IEnumerator AlphaBlink()
    {
        
            yield return new WaitForSecondsRealtime(0.1f);
            sprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSecondsRealtime(0.1f);
            sprite.color = new Color(1, 1, 1, 1);
        
    }
    void hpui()
    {
        hp.fillAmount = Mathf.Lerp(hp.fillAmount, bossHP / MaxbossHP, 1);
        hptext.text = bossHP.ToString();
    }
    public void anistop()
    {
        Destroy(anim);
    }
    public void skillsound()
    {
        SoundManager.instance.SFXPlay("boss_skill", skill_SFX);
    }
}
