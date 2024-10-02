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
            playerObject = other.gameObject; // other.gameObject�� ����Ͽ� �÷��̾� ���� ������Ʈ�� ����
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
            if (playerObject != null && target != null) // playerObject�� null�� �ƴ��� Ȯ��
            {
                playerObject.transform.position = target.transform.position;
            }
        }
    }
}
