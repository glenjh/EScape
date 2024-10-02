using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;
public class dungeonenemy : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI first;
    public TextMeshProUGUI second;
    public Image dark;
    Sequence seq;
    public TextMeshProUGUI txt;
    public TextMeshProUGUI win;
    public int enemynum;
    Sequence win1;
    int initial;
    [Header("SFX")]
    public AudioClip win_SFX;
    void Start()
    {
        initial = 0;
        enemynum = DoorRenderControl.enemyCount;
        win1 = DOTween.Sequence().SetAutoKill(false).Pause()
           .Append(win.DOFade(1f, 1f))
           .Append(win.DOFade(0f, 1f));
        seq = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(dark.DOFade(0f, 0.5f))
            .Append(first.DOFade(1f, 2f))
            .Append(first.DOFade(0f, 2f))
            .Append(second.DOFade(1f, 2f));
        seq.Play();
    }
    void Update()
    {
      
        if (DoorRenderControl.isOpen == false)
        {
            winsound();
            win1.Play();
        }
        else
        {
            win1.Rewind();
            initial = 0;
        }
    }
    // Update is called once per frame
    void winsound()
    {
        if (initial == 0)
        {
            SoundManager.instance.SFXPlay("win", win_SFX);
            initial = 1;
        }
    }
}
