using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dungeon1LevelTP : MonoBehaviour
{
    public int targetscene;
    public string TARGETSCENE;
    private GameObject playerObject;
    public Image dark;
    public scenefade fader;
    public Player player;
    public WeaponManager weapon;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !DoorRenderControl.isOpen)
        {
            if (playerObject != null) // playerObject�� null�� �ƴ��� Ȯ��
            {
                //playerObject.transform.position = target.transform.position;
                Datamanager.instance.nowPlayer.hp = player.playerHP;
                Datamanager.instance.nowPlayer.potion = player.potionCnt;
                Datamanager.instance.nowPlayer.weapon = weapon.currWeaponIdx;
                Datamanager.instance.SaveData();
                GoGameScene(TARGETSCENE);
            }
        }
    }
}
