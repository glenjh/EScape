using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class textmanager : MonoBehaviour
{
    public TextAsset txt;
    public Text recordtext;
    public Text maintext;
    public Text subtext;
    public Text record1;
    public Text record2;
    public Text record3;
    public Text record4;
    public Text record5;
    public Text record6;
    public Text record7;
    public Text record8;
    public Text record9;
    public Text story;
    public Image recordboard;
    public Sprite[] images;
    public scenefade fader;
    public TextMeshProUGUI enter;
    bool r1;
    bool r2;
    bool r3;
    bool r4;
    bool r5;
    bool r6;
    bool r7;
    bool r8;
    bool r9;
    bool start;
    bool playing;
    int state = -1;
    string[,] Sentence;
    int lineSize, rowSize;
    Sequence startani;
    Sequence endani;
    Sequence sel1;
    Sequence sel2;
    Sequence sel3;
    Sequence sel4;
    Sequence sel5;
    Sequence sel6;
    Sequence sel7;
    Sequence sel8;
    Sequence sel9;
    public int page;
    string storywhole;
    void Start()
    {
        Cursor.visible = true;
        recordboard.sprite = images[9];
        // 엔터단위와 탭으로 나눠서 배열의 크기 조정
        string currentText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];
        start = false;
        playing = false;
        // 한 줄에서 탭으로 나눔
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int ii = 0; ii < rowSize; ii++)
            {
                if (ii == rowSize - 1) Sentence[i, ii] = row[ii].Substring(0, row[ii].Length - 1);
                else Sentence[i, ii] = row[ii];
            }
        }
        
        r1 = Datamanager.instance.nowPlayer.record1;
        r2 = Datamanager.instance.nowPlayer.record2;
        r3 = Datamanager.instance.nowPlayer.record3;
        r4 = Datamanager.instance.nowPlayer.record4;
        r5 = Datamanager.instance.nowPlayer.record5;
        r6 = Datamanager.instance.nowPlayer.record6;
        r7 = Datamanager.instance.nowPlayer.record7;
        r8 = Datamanager.instance.nowPlayer.record8;
        r9 = Datamanager.instance.nowPlayer.record9;
        /*
         r1 = true;
         r2 = true;
         r3 = true;
         r4 = true;
         r5 = true;
         r6 = true;
         r7 = true;
         r8 = true;
         r9 = true;
        */
        if (r1 == true)
            record1.DOColor(Color.white, 2f);
        if (r2 == true)
            record2.DOColor(Color.white, 2f);
        if (r3 == true)
            record3.DOColor(Color.white, 2f);
        if (r4 == true)
            record4.DOColor(Color.white, 2f);
        if (r5 == true)
            record5.DOColor(Color.white, 2f);
        if (r6 == true)
            record6.DOColor(Color.white, 2f);
        if (r7 == true)
            record7.DOColor(Color.white, 2f);
        if (r8 == true)
            record8.DOColor(Color.white, 2f);
        if (r9 == true)
            record9.DOColor(Color.white, 2f);

        startani = DOTween.Sequence().SetAutoKill(false).Pause()
           .Append(recordtext.DOColor(Color.white, 1f))
            .Append(maintext.DOColor(Color.white, 1f))
            .Join(subtext.DOColor(Color.white, 1f))
            .Join(recordboard.rectTransform.DOAnchorPosY(-36, 3f).OnComplete(() => start = true));
        endani = DOTween.Sequence().SetAutoKill(false).Pause()
       .Append(recordtext.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(maintext.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(subtext.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record1.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record2.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record3.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record4.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record5.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record6.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record7.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record8.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(record9.DOColor(new Color(255, 255, 255, 0), 1f))
          .Join(recordboard.rectTransform.DOAnchorPosY(-800, 3f)).OnComplete(() => startani.Rewind());
        sel1 = DOTween.Sequence().SetAutoKill(false).Pause()
          .Join(record1.rectTransform.DOAnchorPosX(-400, 1f));
        sel2 = DOTween.Sequence().SetAutoKill(false).Pause()
         .Join(record2.rectTransform.DOAnchorPosX(-400, 1f));
        sel3 = DOTween.Sequence().SetAutoKill(false).Pause()
         .Join(record3.rectTransform.DOAnchorPosX(-400, 1f));
        sel4 = DOTween.Sequence().SetAutoKill(false).Pause()
         .Join(record4.rectTransform.DOAnchorPosX(-400, 1f));
        sel5 = DOTween.Sequence().SetAutoKill(false).Pause()
         .Join(record5.rectTransform.DOAnchorPosX(-400, 1f));
        sel6 = DOTween.Sequence().SetAutoKill(false).Pause()
         .Join(record6.rectTransform.DOAnchorPosX((float)-390, 1f));
        sel7 = DOTween.Sequence().SetAutoKill(false).Pause()
         .Join(record7.rectTransform.DOAnchorPosX((float)-390, 1f));
        sel8 = DOTween.Sequence().SetAutoKill(false).Pause()
         .Join(record8.rectTransform.DOAnchorPosX((float)-390, 1f));
        sel9 = DOTween.Sequence().SetAutoKill(false).Pause()
         .Join(record9.rectTransform.DOAnchorPosX((float)-390, 1f));
        startani.Play();

    }
    private void Update()
    {

        if (start == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                fader.FadeTo("desertmap");
            }
            Vector3 point = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x,
              Input.mousePosition.y, -Camera.main.transform.position.z));
            if (Input.GetMouseButtonDown(0))
            {
                if (point.x >= 0.01539645 && point.x <= 0.2409546 && point.y <= 0.744563 && point.y >= 0.7195438)
                {
                    if (r1 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 0;
                        page = 0;
                        sel2.Rewind();
                        sel3.Rewind();
                        sel4.Rewind();
                        sel5.Rewind();
                        sel6.Rewind();
                        sel7.Rewind();
                        sel8.Rewind();
                        sel9.Rewind();
                        sel1.Play();
                        recordboard.sprite = images[0];
                    }
                    



                }
                if (point.x >= 0.0138568 && point.x <= 0.2401848 && point.y <= 0.6782621 && point.y >= 0.6532429)
                {
                    if (r2 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 1;
                        page = 21;
                        sel1.Rewind();
                        sel3.Rewind();
                        sel4.Rewind();
                        sel5.Rewind();
                        sel6.Rewind();
                        sel7.Rewind();
                        sel8.Rewind();
                        sel9.Rewind();
                        sel2.Play();
                        recordboard.sprite = images[1];
                    }
                    
                }
                if (point.x >= 0.01539645 && point.x <= 0.2386451 && point.y <= 0.6069573 && point.y >= 0.5856909)
                {
                    if (r3 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 2;
                        page = 44;
                        sel2.Rewind();
                        sel1.Rewind();
                        sel4.Rewind();
                        sel5.Rewind();
                        sel6.Rewind();
                        sel7.Rewind();
                        sel8.Rewind();
                        sel9.Rewind();
                        sel3.Play();
                        recordboard.sprite = images[2];
                    }
                   
                }
                if (point.x >= 0.0138568 && point.x <= 0.2409546 && point.y <= 0.5406563 && point.y >= 0.516888)
                {
                    if (r4 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 3;
                        page = 82;
                        sel2.Rewind();
                        sel3.Rewind();
                        sel1.Rewind();
                        sel5.Rewind();
                        sel6.Rewind();
                        sel7.Rewind();
                        sel8.Rewind();
                        sel9.Rewind();
                        sel4.Play();
                        recordboard.sprite = images[3];
                    }
                  
                }
                if (point.x >= 0.01539645 && point.x <= 0.08775981 && point.y <= 0.4718533 && point.y >= 0.450587)
                {
                    if (r5 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 4;
                        page = 109;
                        sel2.Rewind();
                        sel3.Rewind();
                        sel4.Rewind();
                        sel1.Rewind();
                        sel6.Rewind();
                        sel7.Rewind();
                        sel8.Rewind();
                        sel9.Rewind();
                        sel5.Play();
                        recordboard.sprite = images[4];
                    }
                    
                }
                if (point.x >= 0.01924556 && point.x <= 0.3140877 && point.y <= 0.2942167 && point.y >= 0.2754523)
                {
                    if (r6 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 5;
                        page = 124;
                        sel2.Rewind();
                        sel3.Rewind();
                        sel4.Rewind();
                        sel5.Rewind();
                        sel1.Rewind();
                        sel7.Rewind();
                        sel8.Rewind();
                        sel9.Rewind();
                        sel6.Play();
                        recordboard.sprite = images[5];
                    }
                   
                }
                if (point.x >= 0.01924556 && point.x <= 0.1501155 && point.y <= 0.2291667 && point.y >= 0.2079003)
                {
                    if (r7 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 6;
                        page = 129;
                        sel2.Rewind();
                        sel3.Rewind();
                        sel4.Rewind();
                        sel5.Rewind();
                        sel6.Rewind();
                        sel1.Rewind();
                        sel8.Rewind();
                        sel9.Rewind();
                        sel7.Play();
                        recordboard.sprite = images[6];
                    }
                   
                }
                if (point.x >= 0.02155503 && point.x <= 0.1993841 && point.y <= 0.1653676 && point.y >= 0.1428503)
                {
                    if (r8 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 7;
                        page = 136;
                        sel2.Rewind();
                        sel3.Rewind();
                        sel4.Rewind();
                        sel5.Rewind();
                        sel6.Rewind();
                        sel7.Rewind();
                        sel1.Rewind();
                        sel9.Rewind();
                        sel8.Play();
                        recordboard.sprite = images[7];
                    }
                  
                }
                if (point.x >= 0.02001538 && point.x <= 0.147806 && point.y <= 0.1003175 && point.y >= 0.08030213)
                {
                    if (r9 == true)
                    {
                        enter.DOFade(1, 1f);
                        state = 8;
                        page = 144;
                        sel2.Rewind();
                        sel3.Rewind();
                        sel4.Rewind();
                        sel5.Rewind();
                        sel6.Rewind();
                        sel7.Rewind();
                        sel8.Rewind();
                        sel1.Rewind();
                        sel9.Play();
                        recordboard.sprite = images[8];
                    }
                }



            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (state != -1)
                {

                    endani.Play();
                    start = false;
                    playing = true;
                }
            }
        }

        else if (playing == true)
        {

            check(0,10,19,-1,21);
            check(1, 31,40,-1, 44);
            check(2, 54,63,72, 82);
            check(3, 92,101,-1, 109);
            check(4, 118,-1,-1, 124);
            check1(5, 129);
            check1(6, 136);
            check1(7, 144);
            check1(8, 147);
        }


    }
    void check(int stat,int mid1,int mid2,int mid3,int end)
    {
      
        if (state == stat)
        {

            if ((Input.GetKeyDown(KeyCode.Return)))
            {
                maintext.color = new Color(255, 255, 255, 0);
                subtext.color = new Color(255, 255, 255, 0);
                if (page == mid1 ||page==mid2 ||page==mid3)
                {
                    storywhole = "";
                }
                
                if (page >= end)
                {
                    story.text = "";
                    storywhole = "";
                    playing = false;
                    start = true;
                    state = -1;
                    recordboard.sprite = images[9];
                    enter.DOFade(0, 1f);
                    endani.Rewind();
                    sel2.Rewind();
                    sel1.Rewind();
                    sel4.Rewind();
                    sel5.Rewind();
                    sel6.Rewind();
                    sel7.Rewind();
                    sel8.Rewind();
                    sel9.Rewind();
                    sel3.Rewind();

                }
                else
                {
                    if (Sentence[page, 0] == ".")
                    {
                        storywhole += "      " + Sentence[page, 1] + "\n\n";
                    }
                    else storywhole += Sentence[page, 0] + " : " + Sentence[page, 1] + "\n\n";
                    story.DOText(storywhole, 1f);
                    page++;
                }
            }
        }
    }
    void check1(int stat, int end)
    {

        if (state == stat)
        {

            if ((Input.GetKeyDown(KeyCode.Return)))
            {
                maintext.color = new Color(255, 255, 255, 0);
                subtext.color = new Color(255, 255, 255, 0);
                
                
                if (page >= end)
                {
                    story.text = "";
                    storywhole = "";
                    playing = false;
                    start = true;
                    recordboard.sprite = images[9];
                    endani.Rewind();
                    sel2.Rewind();
                    sel1.Rewind();
                    sel4.Rewind();
                    sel5.Rewind();
                    sel6.Rewind();
                    sel7.Rewind();
                    sel8.Rewind();
                    sel9.Rewind();
                    sel3.Rewind();

                }
                else
                {
                    storywhole +=  Sentence[page, 1] + "\n\n";
                    story.DOText(storywhole, 1f);
                    page++;
                }
            }
        }
    }
}

