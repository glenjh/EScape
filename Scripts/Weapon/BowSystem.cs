using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSystem : MonoBehaviour
{
    [SerializeField] 
    public BowData data;
    [SerializeField] 
    public Transform muzzlePos;
    [SerializeField] 
    public GameObject arrow;
    [SerializeField] 
    public GameObject arrowImg;
    public AudioClip bowSFX;
    public Player player;
    
    public float arrowDamage;
    public float arrowMaxDamage;
    public float arrowSpeed;
    public Animator anim;
    public float playerSpeedInCharge, originSpeed;
    public bool isCharging, shootAble;

    public void Awake()
    {
        anim = GetComponent<Animator>();
        arrowDamage = data.defaultDamage;
        arrowMaxDamage = data.maxDamage;
        arrowSpeed = data.projectileSpeed;
        shootAble = true;
        originSpeed = player.movingSpeed;
        playerSpeedInCharge = player.movingSpeed * 0.5f;
    }

    public void Update()
    {
        if (!player.isPlaying || player._stateType == PlayerStateType.Dead || player._stateType == PlayerStateType.Dash || player._stateType == PlayerStateType.Stunned)
        {
            return;
        }
        InputAction();
        // Debug.Log(arrowDamage);
        // Debug.Log(arrowSpeed);
    }

    public void InputAction()
    {
        if (Input.GetMouseButtonDown(0) && shootAble)
        {
            isCharging = true;
        }

        if (isCharging)
        {
            anim.SetBool("isCharging", true);
            player.movingSpeed = playerSpeedInCharge;
            if (arrowDamage < arrowMaxDamage)
            {
                arrowDamage += Time.deltaTime * data.increaseRate;
            }

            if (arrowSpeed < data.maxProjectileSpeed)
            {
                arrowSpeed += Time.deltaTime * data.increaseRate;
            }
        }

        if (Input.GetMouseButtonUp(0) && shootAble)
        {
            data.actualDamage = arrowDamage;
            player.movingSpeed = originSpeed;
            arrowImg.SetActive(false);
            isCharging = false;
            anim.SetBool("isCharging", false);
            StartCoroutine("Shoot");
        }
    }

    public IEnumerator Shoot()
    {
        SoundManager.instance.SFXPlay("bowSFX", bowSFX);
        shootAble = false;
        
        var myArrow = ObjectPoolManager.instance.GetObject("Arrow");
        myArrow.transform.position = muzzlePos.position;
        myArrow.transform.rotation = muzzlePos.rotation;
        
        myArrow.GetComponent<Rigidbody2D>().AddForce(muzzlePos.right * arrowSpeed, ForceMode2D.Impulse);

        yield return new WaitForSecondsRealtime(data.shootingDelay);
        
        arrowDamage = data.defaultDamage;
        arrowSpeed = data.projectileSpeed;
        shootAble = true;
        arrowImg.SetActive(true);
    }
}
