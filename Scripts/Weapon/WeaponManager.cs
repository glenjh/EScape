using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Player player;
    public float radius;
    public int currWeaponIdx, tempIdx;
    public GameObject[] weapons;

    void Init()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        
        //weapons[0].SetActive(true);
        //currWeaponIdx = 0;
        
        currWeaponIdx = Datamanager.instance.nowPlayer.weapon;
        weapons[currWeaponIdx].SetActive(true);
        
    }
    
    void Awake()
    {
        Init();
    }

    void Update()
    {
        InterAct();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(player.transform.position, radius);
    }

    public void InterAct()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(player.transform.position, radius))
        {
            if (col.gameObject.CompareTag("InterAction"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SoundManager.instance.SFXPlay("changeSFX", player.weaponChangeSFX);
                    var dropped = col.GetComponent<InterAction>();
                    tempIdx = currWeaponIdx;
                    weapons[currWeaponIdx].SetActive(false); // 현재 무기 끄기
                    dropped.img.sprite = weapons[currWeaponIdx].GetComponent<SpriteRenderer>().sprite;
                    
                    weapons[dropped.weaponNum].SetActive(true); // 떨어진 무기 켜지
                    currWeaponIdx = dropped.weaponNum;
                    
                    dropped.weaponNum = tempIdx;
                }
            }else if (col.gameObject.CompareTag("Potion"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SoundManager.instance.SFXPlay("interactSFX", player.interactSFX);
                    player.potionCnt++;
                    Destroy(col.gameObject);
                }
            }
        }
    }
}
