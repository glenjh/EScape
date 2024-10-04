using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSystem : MonoBehaviour
{
    [SerializeField] 
    public MeleeData data;
    [SerializeField] 
    public Transform origin;
    public AudioClip swingSFX;
    public float radius;
    public Player player;
    public Animator anim;
    public bool attackAble;

    void Awake()
    {
        attackAble = true;
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (!player.isPlaying || player._stateType == PlayerStateType.Dead || player._stateType == PlayerStateType.Dash || player._stateType == PlayerStateType.Stunned)
        {
            return;
        }
        InputAction();
    }

    public void InputAction()
    {
        if (Input.GetMouseButtonDown(0) && attackAble)
        {
            attackAble = false;
            anim.SetTrigger("attack");
            StartCoroutine("Swing");
        }
    }

    public IEnumerator Swing()
    {
        SoundManager.instance.SFXPlay("swingSFX", swingSFX);
        yield return new WaitForSecondsRealtime(data.attackDelay);

        attackAble = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin.position, radius);
    }

    public void DoAttack()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(origin.position, radius))
        {
            if(col.gameObject.CompareTag("Player")){ return; }
            
            Debug.Log(col.gameObject.name);
            Vector2 currentHitPos = col.ClosestPoint(origin.position);
            col.gameObject.GetComponent<IDamageAble>()?.TakeDamage((int)data.damage, null);

            if (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Boss"))
            {
                var effect = ObjectPoolManager.instance.GetObject("HitEffect");
                effect.transform.position = currentHitPos;
            }
        }
    }
}
