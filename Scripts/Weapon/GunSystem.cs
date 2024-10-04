using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [SerializeField] 
    public GunData data;
    [SerializeField] 
    public Transform muzzlePos;
    [SerializeField] 
    public GameObject bullet;
    [SerializeField] 
    public ParticleSystem muzzleFlash;
    public AudioClip gunSFX;
    public AudioClip reloadSFX;
    public bool isReloading, shooting, shootAble;
    public Player player;

    public void Awake()
    {
        data.currAmmo = data.maxAmmo;
        shootAble = true;
    }

    public void Update()
    {
        if (!player.isPlaying || player._stateType == PlayerStateType.Dead || player._stateType == PlayerStateType.Dash || player._stateType == PlayerStateType.Stunned)
        {
            return;
        }
        InputAction();
    }

    public void InputAction()
    {
        if (data.repeater)
        {
            shooting = Input.GetMouseButton(0);
        }
        else
        {
            shooting = Input.GetMouseButtonDown(0);
        }

        if (Input.GetKeyDown(KeyCode.R) && data.currAmmo < data.maxAmmo && !isReloading)
        {
            StartCoroutine("Reload");
        }

        if (shootAble && shooting && !isReloading && data.currAmmo > 0)
        {
            StartCoroutine("Shoot");
        }
    }

    public IEnumerator Reload()
    {
        SoundManager.instance.SFXPlay("reloadSFX", reloadSFX);
        isReloading = true;

        yield return new WaitForSecondsRealtime(data.reloadingTime);

        isReloading = false;
        data.currAmmo = data.maxAmmo;
    }

    public IEnumerator Shoot()
    {
        SoundManager.instance.SFXPlay("gunSFX", gunSFX);
        shootAble = false;

        var myBullet = ObjectPoolManager.instance.GetObject("PistolBullet");
        myBullet.transform.position = muzzlePos.position;
        myBullet.transform.rotation = muzzlePos.rotation;
        
        myBullet.GetComponent<Rigidbody2D>().AddForce(muzzlePos.right * data.projectileSpeed, ForceMode2D.Impulse);
        muzzleFlash.Play();
        
        data.currAmmo--;
        
        yield return new WaitForSecondsRealtime(data.shootingDelay);

        shootAble = true;
    }
}
