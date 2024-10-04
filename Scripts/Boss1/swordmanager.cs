using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordmanager : MonoBehaviour
{
    [SerializeField] GameObject swordprefab=null;
    [SerializeField] Transform direct = null;
    public Rigidbody2D target;
    public Transform rigid;
    public Boss1 boss;
    float time;
    // Start is called before the first frame update
    void Start()
    {
      
        time = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        Rotatation();
        time += Time.deltaTime;
        if (time >= 10 && boss.bossHP>0)
        {
            fireon();
            time = 0;
        }
            
        
    }
    void Rotatation()
    {
        Vector2 dir= new Vector2(target.transform.position.x - rigid.position.x, target.transform.position.y - rigid.position.y) ;
        direct.right = dir;
    }
   void fireon()
    {
        GameObject sword_1 = Instantiate(swordprefab, direct.position, direct.rotation);
        sword_1.GetComponent<Rigidbody2D>().velocity = sword_1.transform.right * 10f;
    }

}
