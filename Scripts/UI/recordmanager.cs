using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

public class recordmanager : MonoBehaviour
{
    private GameObject playerObject;
    public GameObject record;
    public Player player;
    public TextMeshProUGUI sign;
    public SpriteRenderer image;
    public CircleCollider2D coll;
    public int n;
    Sequence endani;
    int trial = 0;
    public AudioClip interaction;
    private void Start()
    {
        endani = DOTween.Sequence().SetAutoKill(false).Pause()
       .Append(sign.DOColor(new Color(255, 255, 255, 255), 1.5f))
       .Append(sign.DOColor(new Color(255, 255, 255, 0), 1.5f).OnComplete(() => Destroy(record)));
        switchdestroy(n,0);
        
    }
    // Start is called before the first frame update
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

        if (Input.GetKeyDown(KeyCode.E)&& trial==0)
        {
            if (playerObject != null)
            {
                trial = 1;
                SoundManager.instance.SFXPlay("interaction",interaction);
                Destroy(image);
                Destroy(coll);
                endani.Play();
                switchdestroy(n, 1);
               

                
               
            }
                

            
        }
    }
    void switchdestroy(int n,int a)
    {
        switch (n)
        {
            case 1:
                if (Datamanager.instance.nowPlayer.record1 == true && a==0)
                {
                    Destroy(record);

                }
                else if(Datamanager.instance.nowPlayer.record1 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record1 = true;
                }
                break;
            case 2:
                if (Datamanager.instance.nowPlayer.record2 == true && a == 0)
                {

                    Destroy(record);
                }
                else if(Datamanager.instance.nowPlayer.record2 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record2 = true;
                }
                
                break;
            case 3:
                if (Datamanager.instance.nowPlayer.record3 == true && a==0)
                {
                    
                        Destroy(record);
                }
                else if (Datamanager.instance.nowPlayer.record3 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record3 = true;
                }
                break;
            case 4:
                if (Datamanager.instance.nowPlayer.record4 == true && a==0)
                {
                    
                        Destroy(record);
                   
                }
                else if (Datamanager.instance.nowPlayer.record4 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record4 = true;
                }
                break;
            case 5:
                if (Datamanager.instance.nowPlayer.record5 == true && a==0)
                {
                   
                        Destroy(record);
                  
                }
                else if (Datamanager.instance.nowPlayer.record5 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record5 = true;
                }
                break;
            case 6:
                if (Datamanager.instance.nowPlayer.record6 == true && a==0)
                {
                    
                        Destroy(record);
                }
                else if (Datamanager.instance.nowPlayer.record6 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record6 = true;
                }
                break;
            case 7:
                if (Datamanager.instance.nowPlayer.record7 == true && a==0)
                {
                    
                        Destroy(record);
                  
                }
                else if (Datamanager.instance.nowPlayer.record7 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record7 = true;
                }
                break;
            case 8:
                if (Datamanager.instance.nowPlayer.record8 == true && a==0)
                {
                    
                        Destroy(record);
                }
                else if (Datamanager.instance.nowPlayer.record8 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record8 = true;
                }
                break;
            case 9:
                if (Datamanager.instance.nowPlayer.record9 == true && a==0)
                {
                   
                        Destroy(record);
                }
                else if (Datamanager.instance.nowPlayer.record9 == false && a == 1)
                {
                    Datamanager.instance.nowPlayer.record9 = true;
                }
                break;

        }
    }
}
