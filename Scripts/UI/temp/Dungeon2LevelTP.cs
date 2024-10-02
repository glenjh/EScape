using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dungeon2LevelTP : MonoBehaviour
{
    public int targetscene;
    public string TARGETSCENE;
    public int mob2;
    public Image dark;
    public scenefade fader;
    private GameObject playerObject;
    public GameObject GateClosed;
    public GameObject GateOpened;
    public Player player;
    public WeaponManager weapon;

    private int currentSceneIndex;
    public void GoGameScene(string targetscene)
    {
        dark.enabled = true;
        fader.FadeTo(targetscene);
    }
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
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }
    private void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Boss").Length;
        mob2 = enemyCount;
        if (currentSceneIndex == 10) // 2던전 보스방에서만 활성화
        {
            if (enemyCount == 0)
            {
                GateClosed.SetActive(false);
                GateOpened.SetActive(true);
            }
            else
            {
                GateClosed.SetActive(true);
                GateOpened.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && (enemyCount == 0))
        {
            if (playerObject != null) // playerObject이 null이 아닌지 확인
            {
                Datamanager.instance.nowPlayer.hp = player.playerHP;
                Datamanager.instance.nowPlayer.potion = player.potionCnt;
                Datamanager.instance.nowPlayer.weapon = weapon.currWeaponIdx;
                Datamanager.instance.SaveData();
                GoGameScene(TARGETSCENE);
            }
        }
    }
}
