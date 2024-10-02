using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class pieceofTLP1 : MonoBehaviour
{
    private GameObject playerObject;
    public GameObject TLP1;
    public GameObject Boss;
    public SpriteRenderer image;
    public Player player;
    public TextMeshProUGUI sign;
    Sequence endani;
    public AudioClip interaction;
    int trial = 0;
    private void Start()
    {
        endani = DOTween.Sequence().SetAutoKill(false).Pause()
        .Append(sign.DOColor(new Color(255,255,255,255), 1.5f))
        .Append(sign.DOColor(new Color(255, 255, 255,0), 1.5f));
        TLP1.SetActive(false);
        
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerObject != null && trial==0 )
            {
                if (Datamanager.instance.nowPlayer.progress != 4)
                {
                    Datamanager.instance.nowPlayer.progress += 1;
                }
                SoundManager.instance.SFXPlay("interaction", interaction);
                player.playerHP = player.playerMaxHP;
                Datamanager.instance.SaveData();
                Destroy(Boss);
                Destroy(image);
                endani.Play();
                trial = 1;

            }
        }
    }
}
