using BackEnd;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;
using Sequence = DG.Tweening.Sequence;

public class endingmanager : MonoBehaviour
{
    public TextAsset txt;
    public Image picture1;
    public Image picture2;
    public Image picture3;
    public Text mytext;
    public Text foxtext;
    public Text ending1;
    public Text ending2;
    public TextMeshProUGUI enter;
    public int page;
    string[,] Sentence;
    int lineSize, rowSize;
    Sequence[] number=new Sequence[14];
    int currentDay;

    private void Start()
    {
        DateTime currentDate = DateTime.Now;
        currentDay = currentDate.Year * 10000 + currentDate.Month * 100 + currentDate.Day;
    
    Cursor.visible = true;
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
        number[0] = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(picture1.rectTransform.DOAnchorPosY(52, 4f).SetEase(Ease.OutQuad))
            .Append(mytext.DOText(Sentence[0,0], 2f))
            .Append(enter.DOFade(1, 1f));
        number[1]= DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0,1f))
            .Append(picture2.rectTransform.DOAnchorPosY(60, 4f).SetEase(Ease.OutQuad))
            .Append(mytext.DOText("",1f))
            .Append(mytext.DOText(Sentence[1, 0], 2f))
            .Append(enter.DOFade(1, 1f)); 
        number[2]= DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
            .Append(mytext.DOText("",1f))
            .Append(picture1.rectTransform.DOAnchorPosY(-800, 1f).SetEase(Ease.OutQuad))
            .Join(picture2.rectTransform.DOAnchorPosY(-800, 1f).SetEase(Ease.OutQuad))
            .Append(foxtext.DOText(Sentence[2, 0], 2f))
            .Append(enter.DOFade(1, 1f));

        number[3]= DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
            .Append(foxtext.DOText("",1f))
            .Append(mytext.DOText(Sentence[3, 0], 2f))
            .Append(enter.DOFade(1, 1f));
        number[4]= DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
            .Append(picture3.rectTransform.DOAnchorPosY(55, 1f).SetEase(Ease.OutQuad))
            .Append(mytext.DOText("",1f))
            .Append(mytext.DOText(Sentence[4, 0], 2f))
            .Append(enter.DOFade(1, 1f));
        number[5] = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
            .Append(mytext.DOText("",1f))
            .Append(mytext.DOText(Sentence[5, 0], 2f).OnStart(() => mytext.color = Color.blue))
            .Append(enter.DOFade(1, 1f));
        number[6] = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
            .Append(picture3.rectTransform.DOAnchorPosY(-800, 1f).SetEase(Ease.OutQuad))
            .Append(foxtext.DOText(Sentence[6, 0], 2f))
            .Append(enter.DOFade(1, 1f)); 
        number[7]= DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
            .Append(foxtext.DOText("", 1f))
            .Append(mytext.DOText("",1f))
            .Append(mytext.DOText(Sentence[7, 0], 2f).OnStart(() => mytext.color = Color.white))
            .Append(enter.DOFade(1, 1f));
        number[8] = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
            .Append(mytext.DOText("", 1f))
            .Append(mytext.DOText(Sentence[8, 0], 2f))
            .Append(enter.DOFade(1, 1f));
        number[9] = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
           .Append(mytext.DOText("", 1f))
           .Append(mytext.DOText(Sentence[9, 0], 2f))
           .Append(enter.DOFade(1, 1f));
        number[10] = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
            .Append(mytext.DOText("", 1f))
           .Append(mytext.DOText(Sentence[10, 0], 2f))
           .Append(enter.DOFade(1, 1f));
        number[11] = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
          .Append(mytext.DOText("", 1f))
          .Append(mytext.DOText(Sentence[11, 0], 2f))
          .Append(enter.DOFade(1, 1f));
        number[12] = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(enter.DOFade(0, 1f))
          .Append(mytext.DOText("", 1f))
          .Append(ending1.DOText(Sentence[12, 0], 2f))
        .Append(ending2.DOText(Sentence[13, 0], 2f))
        .Append(enter.DOFade(1, 1f));


        number[0].Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (number[page].IsComplete() == true)
            {
                page++;
                if (page == 13)
                {
                    menuani.end = true;
                    menuani.mainmenustate = 5;
                    Datainsert();
                    SceneManager.LoadScene("gamemenu");
                   


                }
                else 
                number[page].Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuani.end = true;
            menuani.mainmenustate = 5;
            SceneManager.LoadScene("gamemenu");
        }
    }
    public void Datainsert()
    {
        Param data = new Param();
        data.Add("nickname", Datamanager.instance.nowPlayer.name);
        data.Add("clearday", currentDay);

        Backend.PlayerData.InsertData("Clearlog", data);
        Debug.Log("clearday 데이터 입력");
    }
}
