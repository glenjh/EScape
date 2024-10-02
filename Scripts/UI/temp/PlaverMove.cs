using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaverMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    private Rigidbody2D rigid;
    private Animator anim;
    private float horizontal;
    private Vector2 direction;
    private bool isFacingRight = true;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        horizontal = direction.x;

        if (Input.GetButtonDown("clog"))
        {
            print("Coin:" + Datamanager.instance.nowPlayer.coin);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Datamanager.instance.nowPlayer.coin++;
            print("Coin:" + Datamanager.instance.nowPlayer.coin);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            print("saved");
            Datamanager.instance.SaveData();
        }
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            print("loaded");
            Datamanager.instance.LoadData();
        }*/
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            
            //transform.position = new Vector3(-1.5f, 3.5f, 0f);
        }

        anim.SetFloat("speed", direction.sqrMagnitude);

        Flip();
    }

    void FixedUpdate()
    {
        //rigid.velocity = direction * moveSpeed;
        rigid.MovePosition(rigid.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
