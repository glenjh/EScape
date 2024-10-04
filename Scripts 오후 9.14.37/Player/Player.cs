using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType
{
    Idle,
    Move,
    Dash,
    Stunned,
    Dead
}

public class Player : MonoBehaviour, IDamageAble
{
    [Header("Player Components")]
    public Rigidbody2D rigid;
    public CapsuleCollider2D playerCol;
    public SpriteRenderer sprite;
    public Animator anim;
    public Camera mainCam;
    public Vector3 mousePos;
    public bool isPlaying;

    [Header("Player")] 
    [SerializeField] private GameObject holdingWeapon;
    public PlayerStateType _stateType;
    public PlayerStateMachine _stateMachine;
    public float horizontalMove;
    public float VerticalMove;
    public float movingSpeed = 2f;
    public float playerHP = 10f;
    public float playerMaxHP = 10f;
    public float invincibleTime = 0.5f;
    
    [HideInInspector]
    public bool isInvincible = false;
    
    public int markCnt = 0;
    [SerializeField] public ParticleSystem hitPS;

    [Header("Dash")] 
    public bool canDash = true;
    public bool isDashing;
    public float dashingTime = 0.1f;
    public float dashCoolDown = 1f;
    public float dashSpeed = 70f;
    
    [Header("Stun")] 
    public float knockBackTime = 0.3f;
    public float stunAmount;

    [Header("CrossHair")] 
    public GameObject crossHair;

    [Header("Potion")] 
    public int potionCnt = 0;
    public int healAmount = 1;
    [SerializeField] 
    public ParticleSystem healPS;

    [Header("SFX")] 
    public AudioClip damageSFX;
    public AudioClip deadSFX;
    public AudioClip potionSFX;
    public AudioClip interactSFX;
    public AudioClip weaponChangeSFX;

    void Init()
    {
        mainCam = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        _stateMachine = new PlayerStateMachine(PlayerStateType.Idle, this);
        isPlaying = true;
        
        playerHP = Datamanager.instance.nowPlayer.hp;
        potionCnt = Datamanager.instance.nowPlayer.potion;
        
    }
    

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }
        
        _stateMachine.Action();
    
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        CurserUpdate();
        MarkCheck();
    }

    void MarkCheck()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
           // markCnt++;
        }
        if (markCnt >= 3)
        {
            playerHP = 0;
            _stateMachine.ChangeState(PlayerStateType.Dead);
        }
    }
    
    void CurserUpdate()
    {
        if (isPlaying)
        {
            crossHair.SetActive(true);
            Cursor.visible = false;
        }
        else
        {
            crossHair.SetActive(false);
            Cursor.visible = true;
        }
        
        crossHair.transform.position = new Vector2(mousePos.x, mousePos.y);
        if (mousePos.x >= transform.position.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
    
    public void Idle()
    {
        if (_stateType != PlayerStateType.Idle)
        {
            _stateMachine.ChangeState(PlayerStateType.Idle);
        }
    }

    public void SetMove()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove != 0 || VerticalMove != 0)
        {
            _stateMachine.ChangeState(PlayerStateType.Move);
        }
    }
    
    public void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");
        
        if ((horizontalMove == 0 && VerticalMove == 0) && _stateType != PlayerStateType.Idle)
        { 
            Idle();
        }
        
        rigid.velocity = new Vector2(horizontalMove * movingSpeed, VerticalMove * movingSpeed);
    }

    public void SetDash()
    {
        if (Input.GetMouseButtonDown(1) && canDash)
        {
            _stateMachine.ChangeState(PlayerStateType.Dash);
        }
    }

    public IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        anim.SetBool("isDashing", true);
        Vector3 dashDir = (mousePos - transform.position).normalized;
        rigid.velocity = dashDir * dashSpeed;
        
        yield return new WaitForSecondsRealtime(dashingTime);
        anim.SetBool("isDashing", false);
        rigid.velocity = new Vector2(0, 0);
        _stateMachine.ChangeState(PlayerStateType.Idle);

        isDashing = false;
        yield return new WaitForSecondsRealtime(dashCoolDown);

        canDash = true;
    }
    
    public void OnTriggerStay2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("skill")&& !isInvincible) )
        {
            playerHP -= 1;
            if (playerHP <= 0)
            {
                _stateMachine.ChangeState(PlayerStateType.Dead);
            }
            else
            {
                anim.SetTrigger("isHurt");
                StartCoroutine("OnDamage");
                StartCoroutine("AlphaBlink");
            }
        }
    }

    public void TakeDamage(int damage, Transform other)
    {
        if (_stateType == PlayerStateType.Dead)
        {
            return; 
        }
        
        if (playerHP > damage)
        {
            SoundManager.instance.SFXPlay("DamagedSFX", damageSFX);
            hitPS.Play();
            playerHP -= damage;
            anim.SetTrigger("isHurt");
            StartCoroutine(Knockback(other));
            StartCoroutine("OnDamage");
            StartCoroutine("AlphaBlink");
        }
        else if(playerHP <= damage)
        {
            SoundManager.instance.SFXPlay("deadSFX", deadSFX);
            playerHP = 0;
            Dead();
        }
    }

    public IEnumerator Knockback(Transform enemyPos)
    {
        rigid.velocity = Vector2.zero;
        SetStun(knockBackTime);
        Vector3 dir = transform.position - enemyPos.position;
        rigid.AddForce(dir.normalized * 10, ForceMode2D.Impulse);

        yield return new WaitForSecondsRealtime(knockBackTime);
        
        rigid.velocity = Vector2.zero;
        Idle();
    }
    
    public void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Boss") || col.gameObject.CompareTag("enemy_bullet")) && !isInvincible)
        {
            TakeDamage(1, col.transform);
        }
    }
   
    public IEnumerator AlphaBlink()
    {
        while (isInvincible)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            sprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSecondsRealtime(0.1f);
            sprite.color = new Color(1, 1, 1, 1);
        }
    }

    public IEnumerator OnDamage()
    {
        isInvincible = true;
        
        yield return new WaitForSecondsRealtime(invincibleTime);

        isInvincible = false;
    }

    public void SetStun(float stunTime)
    {
        if (_stateType != PlayerStateType.Stunned)
        {
            stunAmount = stunTime;
            _stateMachine.ChangeState(PlayerStateType.Stunned);
        }
    }

    public IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunAmount);
        
        Idle();
    }

    public void PotionHeal()
    {
        if (playerHP == playerMaxHP)
        {
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1) && potionCnt > 0)
        {
            SoundManager.instance.SFXPlay("potionSFX", potionSFX);
            healPS.Play();
            potionCnt--;
            playerHP += healAmount;
            if (playerHP > playerMaxHP)
            {
                playerHP = playerMaxHP;
            }
        }
    }

    public void Dead()
    {
        _stateMachine.ChangeState(PlayerStateType.Dead);
    }
}

