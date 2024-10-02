using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;
public class desertletter : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI first;
    public TextMeshProUGUI second;
    public Image dark;
    Sequence seq;


    void Start()
    {


        seq = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(dark.DOFade(0f, 0.5f))
            .Append(first.DOFade(1f, 2f))
            .Append(first.DOFade(0f, 2f))
            .Append(second.DOFade(1f, 2f));
        seq.Play();
    }


}
