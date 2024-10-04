using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MBulletControll : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject arrowPrefab = null;
    [SerializeField] Transform firePoint = null;
    //public Rigidbody2D target;
    public Transform rigid;
    public Enemy enemy;
    //float time;
    //public GameObject instantiatedArrow;
    

    public float arrowSpeed = 20f;

    public UnityEvent childEvent;

    void Start() 
    {
        //time = 0;
    }
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //GameObject player = GameObject.FindGameObjectWithTag("Player");

        Rotatation();
        //time += Time.deltaTime;
        //if (time >= 7 && enemy.Health > 0)
        //{
            //fireon();
            //time = 0;
        //}

    }

    void Rotatation()
    {
        Vector2 dir = new Vector2(player.transform.position.x - rigid.position.x, player.transform.position.y - rigid.position.y);
        firePoint.right = dir;
    }

    public void fireon()
    {
        GameObject Mbullet = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Mbullet.GetComponent<Rigidbody2D>().velocity = Mbullet.transform.right * 10f;
        childEvent.Invoke();
       
    }
}
