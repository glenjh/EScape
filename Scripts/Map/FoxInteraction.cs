using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

public class FoxInteraction : MonoBehaviour
{
    public TextAsset txt;
    private GameObject playerObject;
    public Player player;
    public WeaponManager weapon;
    public scenefade fader;
    public UI ui;
    public GameObject conversationblock;
    public Text txt1;
    Sequence openlimit;
    public int page;
    string[,] Sentence;
    string storywhole;
    int lineSize, rowSize;
    public Image fadeimage;
    private void Start()
    {
        openlimit = DOTween.Sequence().SetAutoKill(false).Pause()
           .Append(txt1.DOText("�ٽ� �� ���� ����", 2f)).OnComplete(() => offconv());
        string currentText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];
        page = 0;
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int ii = 0; ii < rowSize; ii++)
            {
                if (ii == rowSize - 1) Sentence[i, ii] = row[ii].Substring(0, row[ii].Length - 1);
                else Sentence[i, ii] = row[ii];
            }
        }
        conversationblock.SetActive(false);
        txt1.text = "";
    }
    public void GoGameScene(int input)
    {
        SceneManager.LoadScene(input);
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

    private void Update()
    {
        if (ui.isconversation == true && conversationblock.activeSelf == true)
        {
            Vector3 point = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,
                 Input.mousePosition.y, -Camera.main.transform.position.z));
            if (Input.GetKeyDown(KeyCode.Return)&& Datamanager.instance.nowPlayer.progress == 0)
            {
                page++;
                if (page >= 23)
                {
                    player.isPlaying = true;
                    conversationblock.SetActive(false);
                    ui.isconversation = false;
                    Datamanager.instance.nowPlayer.progress = 1;
                    Datamanager.instance.SaveData();

                }
                else
                {
                    storywhole = "";
                    txt1.text = "";
                    if (Sentence[page, 0].ToString() == ".")
                    {
                        storywhole = "\n\n" + Sentence[page, 1];
                    }
                    else if(Sentence[page, 0].ToString() =="A")
                        storywhole=Datamanager.instance.nowPlayer.name+"\n\n"+ Sentence[page, 1];
                    else storywhole = Sentence[page, 0].ToString() + "\n\n" + Sentence[page, 1];
                    txt1.DOText(storywhole, 1f);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (Datamanager.instance.nowPlayer.progress!=0)
                {
                    if (point.x >= 0.2511539 && point.x <= 0.3911538 && point.y <= 0.166875 && point.y >= 0.134375)
                    {
                        if (Datamanager.instance.nowPlayer.progress == 2)
                        {
                            openlimit.Restart();
                        }
                        else
                        {
                            ui.isconversation = false;
                            conversationblock.SetActive(false);
                            fadeimage.enabled = true;
                            Datamanager.instance.nowPlayer.weapon = weapon.currWeaponIdx;
                            Datamanager.instance.nowPlayer.hp = player.playerHP;
                            Datamanager.instance.nowPlayer.potion = player.potionCnt;
                            Datamanager.instance.SaveData();
                            fader.FadeTo("Dungeon1-1");
                        }
                    }
                    if (point.x >= 0.5396154 && point.x <= 0.7103846 && point.y <= 0.164375 && point.y >= 0.135625)
                    {
                        if (Datamanager.instance.nowPlayer.progress == 3)
                        {
                            openlimit.Restart();
                        }
                        else
                        {
                            ui.isconversation = false;
                            conversationblock.SetActive(false);
                            fadeimage.enabled = true;
                            Datamanager.instance.nowPlayer.weapon = weapon.currWeaponIdx;
                            Datamanager.instance.nowPlayer.hp = player.playerHP;
                            Datamanager.instance.nowPlayer.potion = player.potionCnt;
                            fader.FadeTo("Dungeon2-1");
                        }
                    }
                }
                
            }

            }
        
            if (Input.GetKeyDown(KeyCode.E))
        { 
            if (playerObject != null&& ui.isconversation == false)
            {
                Cursor.visible = true;
                player.isPlaying = false;
                player.rigid.velocity = Vector2.zero;
                ui.isconversation = true;
                conversationblock.SetActive(true);
                if (Datamanager.instance.nowPlayer.progress == 0)
                {
                    
                    storywhole = Sentence[0, 0].ToString() + "\n\n" + Sentence[0, 1];
                    txt1.DOText(storywhole, 1f);
                }
                else
                {
                    storywhole = "���� ������?" + "\n\n" + "               ������ ����          ���ε��� ����";
                    txt1.DOText(storywhole, 1f);

                }
                

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (player.isPlaying == false && ui.isconversation == true&&conversationblock.activeSelf == true)
            {
                player.isPlaying = true;
                conversationblock.SetActive(false);
                ui.isconversation = false;
                

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
    

