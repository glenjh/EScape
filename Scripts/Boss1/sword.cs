using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour, IDamageAble
{
    public Rigidbody2D rigid;
    public GameObject sword_2;
    [SerializeField]public ParticleSystem effect;
    public float swordhp = 10000;
    public float time = 6;
    public void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(sword_2);
        }
    }
    public void TakeDamage(int damage, Transform other)
    {

        swordhp -= damage;
      
        

        if (swordhp <= 0)
        {
            swordhp = 0;
           
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
     {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(anistart());
        }
       
    }
    public IEnumerator anistart()
    {
        effect.Play();
        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(sword_2);
    }
}
