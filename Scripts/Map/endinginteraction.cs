using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Globalization;
using JetBrains.Annotations;

public class endinginteraction : MonoBehaviour
{
    private GameObject playerObject;
    public Player player;
    public scenefade fader;
    public UI ui;
    public GameObject conversationblock;
    public Text txt;
    public bool id;
    Sequence close;
    private void Start()
    {
        id =false;
        conversationblock.SetActive(false);
        txt.text = "";
        close = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(txt.DOText("",0.5f))
            .Append(txt.DOText("TLP�� ������ �����մϴ�.", 2f).OnComplete(()=>offconv()));

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObject = other.gameObject;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (playerObject != null)
        {
            playerObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ui.isconversation == true&&  conversationblock.activeSelf == true)
        {
            Vector3 point = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,
                  Input.mousePosition.y, -Camera.main.transform.position.z));
            if (Input.GetMouseButtonDown(0))
            {
                if(point.x>= 0.3699732 && point.x<= 0.3887399 && point.y<=0.1652703 && point.y >= 0.1376787)
                {
                    if (Datamanager.instance.nowPlayer.progress == 4)
                    {
                        conversationblock.SetActive(false);
                        fader.FadeTo("ending");
                    }
                    else
                    {
                        close.Restart();
                    }
                }
                if (point.x >= 0.5495978 && point.x <= 0.6246648 && point.y <= 0.1681747 && point.y >= 0.1405831)
                {
                    player.isPlaying = true;
                    conversationblock.SetActive(false);
                    ui.isconversation = false;

                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.E) )
        {
            if (playerObject != null)
            {
                id = true;
                player.isPlaying = false;
                ui.isconversation = true;
                conversationblock.SetActive(true);
                Cursor.visible = true;
                txt.DOText("TLP�� ������ ��ġ�ðڽ��ϱ�? (TLP�� ���� 2�� �ʿ�)\n\n��           �ƴϿ�", 1f);

            }



        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (player.isPlaying == false && ui.isconversation==true && conversationblock.activeSelf==true)
            {
                player.isPlaying =true;
                conversationblock.SetActive(false);
                ui.isconversation =false;
                
            }
        }
    }
    void offconv()
    {
        player.isPlaying = true;
        conversationblock.SetActive(false);
        ui.isconversation = false;
    }
}
