using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;
using UnityEngine.SceneManagement;
using System.IO;

public class gamestart : MonoBehaviour
{

    public Animator anim;
    float time;
    Sequence endani;
    public TextMeshProUGUI gametitletext;
    public TextMeshProUGUI newgame;
    public TextMeshProUGUI gameload;
    public TextMeshProUGUI rank;
    public TextMeshProUGUI exit;
    public scenefade scenefader;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        time = 0;
        endani = DOTween.Sequence().SetAutoKill(false).Pause()
        .Append(gametitletext.DOColor(Color.black, 1f))
           .Join(newgame.DOColor(Color.black, 1f))
           .Join(gameload.DOColor(Color.black, 1f))
           .Join(rank.DOColor(Color.black, 1f))
           .Join(exit.DOColor(Color.black, 1f));
    }

    //각각 버튼마다 세이브 파일을 저장하는 코드 작성 요망
    public void Gamestart()
    {
        anim.SetBool("new game", true);

    } // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (menuani.gamestate == 1)
        {
            time += Time.deltaTime;
            if (time >= 2.0f)
            {
                endani.Play();
                
            }
            if (time >= 5.0f)
            {
                Gamestart();
                
            }
            if (time >= 7.0f)
            {
                anim.SetBool("new game", false);
                menuani.gamestate = 0;
                Datamanager.instance.SaveData();
                Datamanager.instance.nowPlayer.name = "travler";
                Datamanager.instance.nowPlayer.time = 0;
                Datamanager.instance.nowPlayer.progress = 0;
                Datamanager.instance.nowPlayer.coin = 0;
                Datamanager.instance.nowPlayer.weapon = 0;
                Datamanager.instance.nowPlayer.potion= 0;
                Datamanager.instance.nowPlayer.hp = 10f;
                Datamanager.instance.nowPlayer.record1 = false;
                Datamanager.instance.nowPlayer.record2 = false;
                Datamanager.instance.nowPlayer.record3 = false;
                Datamanager.instance.nowPlayer.record4 = false;
                Datamanager.instance.nowPlayer.record5 = false;
                Datamanager.instance.nowPlayer.record6 = false;
                Datamanager.instance.nowPlayer.record7 = false;
                Datamanager.instance.nowPlayer.record8 = false;
                Datamanager.instance.nowPlayer.record9 = false;
    Datamanager.instance.SaveData();
                scenefader.FadeTo("initializesave");
            }
            
        }else if (menuani.gamestate == 2)
        {
            Datamanager.instance.LoadData();
           menuani. gamestate = 0;
            scenefader.FadeTo("desertmap");
        }
    }
}
