using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorRenderControl : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    public static bool isOpen = false; // true�� �� ���� / false�� �� ����
    public static int enemyCount = 0;
    public static int bossCount = 0;
    public static int nearcount = 0;
    //float detectionRadius = 25f;
    //bool isCloseToEnemy = false;

    bool isPlayerInSameRoomAsEnemy = false;
    private GameObject player; // Player ������Ʈ ����
    private GameObject[] enemies; // Enemy ������Ʈ���� �迭
    private GameObject currentRoom; // Player�� ���� ����� ���� ������Ʈ

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        InvokeRepeating("Check", 0, 1);
    }
    void Check()
    {
        // A_ROOM �±׸� ���� ��� ������Ʈ ã��
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("A_ROOM");

        // ���� ����� ����� �Ÿ� �ʱ�ȭ
        float closestRoomDistance = Mathf.Infinity;

        // ��� �濡 ���� ����
        foreach (GameObject room in rooms)
        {
            // player�� room ���� �Ÿ� ���
            float distanceToRoom = Vector2.Distance(player.transform.position, room.transform.position);

            // ��������� �ּ� �Ÿ����� �� ����� room�� ������ ������Ʈ
            if (distanceToRoom < closestRoomDistance)
            {
                closestRoomDistance = distanceToRoom;
                currentRoom = room;
            }
        }
        // Debug.Log("���� ����� ��:" + currentRoom); // log�� ���� �� Ȯ��
        // Player�� ���� ����� �濡 ���� �ִ��� �˻�
        isPlayerInSameRoomAsEnemy = CheckForEnemyInSameRoom();

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bossCount = GameObject.FindGameObjectsWithTag("Boss").Length;


        if (isPlayerInSameRoomAsEnemy || (bossCount != 0)) isOpen = true;
        else isOpen = false;

        if (isOpen)
        {
            tilemapRenderer.enabled = true;
        }
        else
        {
            tilemapRenderer.enabled = false;
        }
    }
    void Update()
    {
        // ���� ����� enemy ã��
        /*
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestEnemyDistance = Mathf.Infinity;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = playerObject.transform;
        // ��� enemy�� ���� ����
        foreach (GameObject enemy in enemies)
        {
            // enemy���� Transform ������Ʈ ��������
            Transform enemyTransform = enemy.transform;

            // player�� enemy�鰣�� �Ÿ� ���
            float distanceToEnemy = Vector2.Distance(playerTransform.position, enemyTransform.position);

            // ��������� �ּ� �Ÿ����� �� ����� enemy�� ������ ������Ʈ
            if (distanceToEnemy < closestEnemyDistance)
            {
                closestEnemyDistance = distanceToEnemy;
            }
        }
        Debug.Log("�� �ּҰŸ�" + closestEnemyDistance + "�����Ÿ�" + detectionRadius);
        if (closestEnemyDistance <= detectionRadius)
        {
            isCloseToEnemy = true;
        }
        else
        {
            isCloseToEnemy = false;
        }

        if (isCloseToEnemy) isOpen = true; // ����� �� ������ �� �ݱ�
        else isOpen = false; // ����� �� ������ �� ����

        */

        //if (enemyCount == 0) isOpen = false;
        //else isOpen = true;
    }
    bool CheckForEnemyInSameRoom()
    {
        if (enemies == null || currentRoom == null) return false;


        // ���� ���� ������ �ִ� ��� Enemy�� ã��
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;
            // enemy�� ���� ������Ʈ�� currentRoom���� Ȯ��
            Transform parentTransform = enemy.transform.parent;

            // parentTransform�� null�� �ƴϰ� currentRoom�� ������ ���� �濡 �ִ� Enemy�� �ִٴ� ��
            if (parentTransform != null && parentTransform.gameObject == currentRoom)
                return true;

            // ���� �÷��̾ ��ġ�� ���� ���� ���� log�� ���
            
            Transform ess = currentRoom.transform.Find("Enemys1_1");
            if(ess != null)
            {
                //Debug.Log(ess.transform.childCount);
                nearcount = ess.transform.childCount;
            }
            else
            {
                ess = currentRoom.transform.Find("Enemys1_2");
                if (ess != null)
                {
                    // Debug.Log(ess.transform.childCount);
                    nearcount = ess.transform.childCount;
                }
                else nearcount = 0;//Debug.Log("ã�� �� ����");
            }
            
        }
        return false;
    }
}