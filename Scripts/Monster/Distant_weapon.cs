using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distant_weapon : MonoBehaviour
{
    public int id;
    public SpriteRenderer spriter;
    public float damage=3;
    public float speed=5;
    

    float timer;
    

    SpriteRenderer enemy;

    public Transform player;

    Vector3 weaponPos = new Vector3(0.77f, -0.22f, 0f);
    Vector3 weaponPosReverse = new Vector3(-0.77f, -0.22f, 0f);
    Quaternion weaponRot = Quaternion.Euler(0, 0, 0);
    Quaternion weaponRotReverse = Quaternion.Euler(0, 0, 0);

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;
        
      
        
    }
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
