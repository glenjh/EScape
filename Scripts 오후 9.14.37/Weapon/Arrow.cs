using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] public BowData data;
    public Rigidbody2D rigid;
    public BoxCollider2D arrowCol;
    public GameObject img;

    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var arrowImg = Instantiate(img, this.transform.position, this.transform.rotation);
        arrowImg.transform.SetParent(col.transform, true);
        
        col.gameObject.GetComponent<IDamageAble>()?.TakeDamage((int)data.actualDamage, null);
        
        var effect = ObjectPoolManager.instance.GetObject("HitEffect");
        effect.transform.position = this.transform.position;
        
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        rigid.velocity = Vector2.zero;
        ObjectPoolManager.instance.ReturnObject("Arrow", this.gameObject);
    }
}
