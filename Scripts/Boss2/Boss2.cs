using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public enum Boss2state
{
    Idle,
    Follow,
    Attack1,
    Attack2,
    Attack3,
    Skill1,
    Dead
}
public class Boss2 : MonoBehaviour, IDamageAble
{
    [SerializeField] public ParticleSystem effect;
    public Rigidbody2D rigid;
    public Rigidbody2D target;
    public CapsuleCollider2D BossCol;
    public SpriteRenderer sprite;
    public Animator anim;
    public Player player;
    private Boss2statemachine statemachine;
    public GameObject collider1;
    public GameObject collider2;
    public GameObject collider3;
    public Image hp;
    public Text hptext;
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
    [Header("SFX")]
    public AudioClip skill_SFX;
    public AudioClip dead_SFX;




    private Dictionary<Boss2state, Bossstate2> states = new Dictionary<Boss2state, Bossstate2>();
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        BossCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Bossstate2 idle = new State2Idle();
        Bossstate2 follow = new State2follow();
        Bossstate2 attack1 = new State2attack1();
        Bossstate2 attack2 = new State2attack2();
        Bossstate2 attack3 = new State2attack3();
        Bossstate2 skill1 = new State2skill1();
        Bossstate2 dead = new State2dead();
        states.Add(Boss2state.Idle, idle);
        states.Add(Boss2state.Follow, follow);
        states.Add(Boss2state.Attack1, attack1);
        states.Add(Boss2state.Attack2, attack2);
        states.Add(Boss2state.Attack3, attack3);
        states.Add(Boss2state.Skill1, skill1);
        states.Add(Boss2state.Dead, dead);
        statemachine = new Boss2statemachine(idle, this);
        random = -1;
        hpui();
    }

    // Update is called once per frame
    void Update()
    {
      
        if (statemachine.currentstate2 == states[Boss2state.Dead])
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
            statemachine.SetState(states[Boss2state.Idle]);
        }
        else
        {
            if (random == -1)
            {
                if (skillcool <= 0)
                {

                    statemachine.SetState(states[Boss2state.Skill1]);

                }
                else
                {

                    random = UnityEngine.Random.Range(0, 3);
                    statemachine.SetState(states[Boss2state.Idle]);
                }
                
            }
            if (random == 0)
            {
                if (attackrange1 == false)
                {
                    if (attacking == false)
                    {
                        statemachine.SetState(states[Boss2state.Follow]);
                        collider1.tag = "Untagged";
                    }
                }
                else
                {
                    statemachine.SetState(states[Boss2state.Attack1]);
                    attacking = true;

                }

            }
            else if (random == 1)
            {
                if (attackrange2 == false)
                {
                    if (attacking == false)
                    {
                        statemachine.SetState(states[Boss2state.Follow]);
                        collider2.tag = "Untagged";
                    }
                }
                else
                {
                    attacking = true;
                    statemachine.SetState(states[Boss2state.Attack2]);

                }

            }
            else if (random == 2)
            {
                if (attackrange3 == false)
                {
                    if (attacking == false)
                    {
                        statemachine.SetState(states[Boss2state.Follow]);
                        collider3.tag = "Untagged";
                    }
                }
                else
                {
                    attacking = true;
                    statemachine.SetState(states[Boss2state.Attack3]);
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
            statemachine.SetState(states[Boss2state.Dead]);
        }
    }
    public void Chase()
    {
        Vector2 dirvec = target.position - rigid.position;
        Vector2 nextVec = dirvec.normalized * movingspeed * Time.fixedDeltaTime;
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
        else if (random == 2)
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
    public void skillend()
    {
        skillcool = 3;
        random = -1;
    }
    public IEnumerator AlphaBlink()
    {

        yield return new WaitForSecondsRealtime(0.1f);
        sprite.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSecondsRealtime(0.1f);
        sprite.color = new Color(1, 1, 1, 1);

    }
    public void hpui()
    {
        hp.fillAmount = Mathf.Lerp(hp.fillAmount, bossHP / MaxbossHP, 1);
        hptext.text = bossHP.ToString();
    }
    public void mark()
    {
        player.markCnt++;
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
