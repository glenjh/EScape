using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;
using UnityEngine.UI;
using Unity.Collections;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;
using Application = UnityEngine.Application;

public class menuani : MonoBehaviour
{
    public static int mainmenustate = 5;
    public static int gamestate = 0;
    public static bool end;
    public TextMeshProUGUI gametitletext;
    public TextMeshProUGUI newgame;
    public TextMeshProUGUI gameload;
    public TextMeshProUGUI ranking;
    public TextMeshProUGUI exit;
    public GameObject saveslotUI;
    public GameObject rankingslotUI;
    public TextMeshProUGUI saveslotstatetext;
    public Image Line1;
    public Image Line2;
    public Image Line3;
    public Image Line4;
    public GameObject scenefader;
    public GameObject endpicture;
    public AudioClip interaction;

    Sequence startani;
    Sequence line1;
    Sequence line2;
    Sequence line3;
    Sequence line4;
    private void Start()
    {
        startani = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(gametitletext.DOColor(Color.white, 2f))
            .Append(newgame.DOColor(Color.white, 2f))
            .Join(gameload.DOColor(Color.white, 2f))
            .Join(ranking.DOColor(Color.white, 2f))
            .Join(exit.DOColor(Color.white, 2f)).OnComplete(()=> mainmenustate = 6);
        line1 = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(Line1.rectTransform.DOAnchorPosX(370.54f, 1f))
            .Join(Line1.rectTransform.DOScaleX(1f, 1f));
        //.Join(Line1.rectTransform.DOScaleX(340f, 2f));

        line2 = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(Line2.rectTransform.DOAnchorPosX(326f, 1f))
            .Join(Line2.rectTransform.DOScaleX(1f, 1f));
        //.Join(Line2.rectTransform.DOScaleX(228f, 2f));

        line3 = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(Line3.rectTransform.DOAnchorPosX(346f, 1f))
            .Join(Line3.rectTransform.DOScaleX(1f, 1f));
        //.Join(Line3.rectTransform.DOScaleX(274f, 2f));

        line4 = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(Line4.rectTransform.DOAnchorPosX(283f, 1f))
            .Join(Line4.rectTransform.DOScaleX(1f, 1f));
        //.Join(Line4.rectTransform.DOScaleX(121f, 2f));

        saveslotUI.SetActive(false);
        rankingslotUI.SetActive(false);
        scenefader.SetActive(false);
        if (end == true)
        {
            endpicture.SetActive(true);
        }
        else endpicture.SetActive(false);

        //애니메이션 효과

    }
    // Update is called once per frame
    void Update()
    {
        //초기 상태mainmenustate를 5로 가정->다른 씬에서 메인 메뉴로 돌아올 때, 초기상태는 5여야함
        if (mainmenustate == 5)
        {
            startani.Play();
        }
        if (mainmenustate == 6)
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y, -Camera.main.transform.position.z));
                
                if (point.y <= -0.10 && point.y >= -0.68)
                {

                    line1.Play();
                    if (Input.GetMouseButtonDown(0))
                    {
                        SoundManager.instance.SFXPlay("interaction", interaction);
                        mainmenustate = 0;
                        saveslotstatetext.text = "초기화하실 세이브 파일을 선택해주세요.";
                        line1.Rewind();
                        saveslotUI.SetActive(true);
                    }
                }
                else if (point.y <= -1.2 && point.y >= -1.96)
                {

                    line2.Play();
                    if (Input.GetMouseButtonDown(0))
                    {
                        SoundManager.instance.SFXPlay("interaction", interaction);
                        mainmenustate = 1;
                        saveslotstatetext.text = "이어서 하실 세이브 파일을 선택해주세요.";
                        line2.Rewind();
                        saveslotUI.SetActive(true);
                    }
                }
                else if (point.y <= -2.1 && point.y >= -2.5)
                {

                    line3.Play();
                    if (Input.GetMouseButtonDown(0))
                    {
                        SoundManager.instance.SFXPlay("interaction", interaction);
                        mainmenustate = 2;
                        line3.Rewind();
                        //clear_scrollview.CreateContent();
                        GameObject otherObject = GameObject.Find("Scrollview");
                        clear_scrollview otherScript = otherObject.GetComponent<clear_scrollview>();
                        otherScript.CreateContent();
                        rankingslotUI.SetActive(true);

                    }
                }
                else if (point.y <= -3.13 && point.y >= -3.3)
                {

                    line4.Play();
                    if (Input.GetMouseButtonDown(0))
                    {
                        SoundManager.instance.SFXPlay("interaction", interaction);
                        mainmenustate = 3;
                        line4.Rewind();
                        Application.Quit();

                    }
                }
                else
                {
                    line1.Rewind();
                    line2.Rewind();
                    line3.Rewind();
                    line4.Rewind();
                }
            }
            else
            {
                line1.Rewind();
                line2.Rewind();
                line3.Rewind();
                line4.Rewind();
            }


        }
        if (mainmenustate == 0 ||mainmenustate==1 || mainmenustate == 2)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                saveslotUI.SetActive(false);
                rankingslotUI.SetActive(false);
                mainmenustate = 6;
            }
        }
    }
}
