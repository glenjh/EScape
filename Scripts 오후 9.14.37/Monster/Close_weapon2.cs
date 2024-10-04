using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class close_weapon2 : MonoBehaviour
{
    public int id;
    public SpriteRenderer spriter;
    public float damage;
    public float speed;

    SpriteRenderer enemy;

    Vector3 weaponPos = new Vector3(0.069f, 0.004f, 0f);
    Vector3 weaponPosReverse = new Vector3(-0.069f, 0.004f,0f);
    Quaternion weaponRot = Quaternion.Euler(0, 0, -50);
    Quaternion weaponRotReverse = Quaternion.Euler(0, 0, 50);
    public void Awake()
    {
        enemy = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = enemy.flipX;

        transform.localPosition = isReverse ? weaponPosReverse : weaponPos;
        transform.localRotation = isReverse ? weaponRotReverse : weaponRot;
        spriter.flipX = isReverse;
    }
}