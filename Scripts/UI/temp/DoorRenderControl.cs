using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorRenderControl : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    public static bool isOpen = false; // true면 문 닫힘 / false면 문 열림
    public static int enemyCount = 0;
    public static int bossCount = 0;
    public static int nearcount = 0;
    //float detectionRadius = 25f;
    //bool isCloseToEnemy = false;

    bool isPlayerInSameRoomAsEnemy = false;
    private GameObject player; // Player 오브젝트 참조
    private GameObject[] enemies; // Enemy 오브젝트들의 배열
    private GameObject currentRoom; // Player와 가장 가까운 방의 오브젝트

    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        InvokeRepeating("Check", 0, 1);
    }
    void Check()
    {
        // A_ROOM 태그를 가진 모든 오브젝트 찾기
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("A_ROOM");

        // 가장 가까운 방과의 거리 초기화
        float closestRoomDistance = Mathf.Infinity;

        // 모든 방에 대한 루프
        foreach (GameObject room in rooms)
        {
            // player와 room 간의 거리 계산
            float distanceToRoom = Vector2.Distance(player.transform.position, room.transform.position);

            // 현재까지의 최소 거리보다 더 가까운 room이 있으면 업데이트
            if (distanceToRoom < closestRoomDistance)
            {
                closestRoomDistance = distanceToRoom;
                currentRoom = room;
            }
        }
        // Debug.Log("가장 가까운 방:" + currentRoom); // log로 현재 방 확인
        // Player와 가장 가까운 방에 적이 있는지 검사
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
        // 가장 가까운 enemy 찾기
        /*
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestEnemyDistance = Mathf.Infinity;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = playerObject.transform;
        // 모든 enemy에 대한 루프
        foreach (GameObject enemy in enemies)
        {
            // enemy에서 Transform 컴포넌트 가져오기
            Transform enemyTransform = enemy.transform;

            // player와 enemy들간의 거리 계산
            float distanceToEnemy = Vector2.Distance(playerTransform.position, enemyTransform.position);

            // 현재까지의 최소 거리보다 더 가까운 enemy가 있으면 업데이트
            if (distanceToEnemy < closestEnemyDistance)
            {
                closestEnemyDistance = distanceToEnemy;
            }
        }
        Debug.Log("적 최소거리" + closestEnemyDistance + "감지거리" + detectionRadius);
        if (closestEnemyDistance <= detectionRadius)
        {
            isCloseToEnemy = true;
        }
        else
        {
            isCloseToEnemy = false;
        }

        if (isCloseToEnemy) isOpen = true; // 가까운 적 있으면 문 닫기
        else isOpen = false; // 가까운 적 없으면 문 열기

        */

        //if (enemyCount == 0) isOpen = false;
        //else isOpen = true;
    }
    bool CheckForEnemyInSameRoom()
    {
        if (enemies == null || currentRoom == null) return false;


        // 현재 방의 하위에 있는 모든 Enemy를 찾기
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;
            // enemy의 상위 오브젝트가 currentRoom인지 확인
            Transform parentTransform = enemy.transform.parent;

            // parentTransform이 null이 아니고 currentRoom과 같으면 같은 방에 있는 Enemy가 있다는 것
            if (parentTransform != null && parentTransform.gameObject == currentRoom)
                return true;

            // 현재 플레이어가 위치한 방의 적의 숫자 log로 출력
            
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
                else nearcount = 0;//Debug.Log("찾을 수 없음");
            }
            
        }
        return false;
    }
}