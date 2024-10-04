using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Sequence = DG.Tweening.Sequence;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class initializename : MonoBehaviour
{

    // Start is called before the first frame update
    public TextMeshProUGUI newPlayername;
    public TextMeshProUGUI nameinfo;
    public Text nameinfo1;
    public TextMeshProUGUI placeholder;
    public Text convname;
    public Text convertext;
    public Image conversationUI;
    public scenefade scenefader;
    public GameObject scenefade;
    public Image enterui;
    public Text entertext;
    string Convname;
    int state;
   // private string playername = "traveler";
    Sequence texting;
    Sequence texting1;
    Sequence texting2;
    void Start()
    {
        state = 0;
        scenefade.SetActive(false);
        texting = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(nameinfo.DOColor(Color.white, 3f))
            .Join(placeholder.DOColor(Color.white, 4f));
        texting1 = DOTween.Sequence().SetAutoKill(false).Pause()
           .Append(nameinfo.DOColor(Color.black, 3f))
           .Join(newPlayername.DOColor(Color.black, 3f))
           .Append(nameinfo1.DOColor(Color.white, 3f))
           .Append(conversationUI.DOColor(Color.white, 1f))
           .Join(enterui.DOColor(Color.white,1f))
           .Join(entertext.DOColor(Color.white,1f))
           .Append(convname.DOText("비행사",1f))
           .Append(convertext.DOText("벗어나고 싶다.", 1f)).OnComplete(() => state = 1);
        
        texting.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (state == 0)
            {
                Datamanager.instance.nowPlayer.name = newPlayername.text;
                Datamanager.instance.SaveData();
                Convname ="사용자"+" "+ newPlayername.text+"님 접속을 환영합니다.";
                texting2 = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(conversationUI.DOColor(Color.black, 1f))
            .Join(enterui.DOColor(Color.black, 1f))
            .Join(entertext.DOColor(Color.black, 1f))
            .Append(nameinfo1.DOText("소망의 불완전성을 확인...", 2f))
            .Append(nameinfo1.DOText(Convname, 1f))
            .Append(enterui.DOColor(Color.white, 1f))
            .Join(entertext.DOColor(Color.white, 1f)).OnComplete(() => state = 2);
                texting1.Play();
            }
            else if (state == 1)
            {
                convertext.text = "";
                convname.text = "";
                texting2.Play();

            }
            else if (state==2)
            {
                scenefade.SetActive(true);
                state = 0;
                scenefader.FadeTo("desertmap");
            }
        }
    }
   
}
