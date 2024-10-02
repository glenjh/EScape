using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SimpleMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    private Rigidbody2D rigid;
    private float horizontal;
    private Vector2 direction;
    private bool isFacingRight = true;
    private TilemapRenderer door;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        GameObject doorControl = GameObject.FindGameObjectWithTag("DoorControl");
        if (doorControl != null)
            door = doorControl.GetComponent<TilemapRenderer>();
    }

    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        horizontal = direction.x;
        
        Flip();

        if (Input.GetKeyDown(KeyCode.Insert))
        {
            door.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            door.enabled = false;
        }

    }

    void FixedUpdate()
    {
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
