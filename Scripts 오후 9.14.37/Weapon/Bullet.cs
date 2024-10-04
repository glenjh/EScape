using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    public GunData data;
    public Rigidbody2D rigid;
    public float lifeTime;

    public void Start()
    {
        lifeTime = 0f;
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= 4f)
        {
            lifeTime = 0f;
            ReturnToPool();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.GetComponent<IDamageAble>()?.TakeDamage((int)data.damage, null);

        lifeTime = 0f;
        var effect = ObjectPoolManager.instance.GetObject("HitEffect");
        effect.transform.position = this.transform.position;
        
        ReturnToPool();
    }
    
    public void ReturnToPool()
    {
        rigid.velocity = Vector2.zero;
        ObjectPoolManager.instance.ReturnObject("PistolBullet", this.gameObject);
    }
}
