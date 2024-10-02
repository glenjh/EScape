using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon1TP : MonoBehaviour
{
    public GameObject target;
    private GameObject playerObject;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObject = other.gameObject; // other.gameObject을 사용하여 플레이어 게임 오브젝트를 저장
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(playerObject != null)
        {
            playerObject = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !DoorRenderControl.isOpen)
        {
            if (playerObject != null && target != null) // playerObject이 null이 아닌지 확인
            {
                playerObject.transform.position = target.transform.position;
            }
        }
    }
}
