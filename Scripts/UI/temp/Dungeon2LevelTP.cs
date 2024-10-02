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
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
    }
    private void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Boss").Length;
        mob2 = enemyCount;
        if (currentSceneIndex == 10) // 2���� �����濡���� Ȱ��ȭ
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
            if (playerObject != null) // playerObject�� null�� �ƴ��� Ȯ��
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
