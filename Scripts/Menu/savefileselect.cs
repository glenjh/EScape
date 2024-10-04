using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening.Plugins.Core.PathCore;

public class savefileselect : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI[] slottext;
   // public Text initialname;
    public GameObject saveslotUI;
    public GameObject scenefader;
    bool[] savefile=new bool[3];
    private void Start()
    {
        for (int i = 0; i <3; i++)
        {
            if (File.Exists(Datamanager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                Datamanager.instance.nowSlot = i;
                Datamanager.instance.LoadData();
                if(Datamanager.instance.nowPlayer.progress==2)
                slottext[i].text = Datamanager.instance.nowPlayer.name +" "+"���൵:"+" "+"������ ����";
                else if(Datamanager.instance.nowPlayer.progress == 3)
                    slottext[i].text = Datamanager.instance.nowPlayer.name + " " + "���൵:" + " " + "���ε��� ����";
                else if (Datamanager.instance.nowPlayer.progress == 4)
                    slottext[i].text = Datamanager.instance.nowPlayer.name + " " + "���൵:" + " " + "������ ����, ���ε��� ����";
                else
                    slottext[i].text = Datamanager.instance.nowPlayer.name + " " + "���൵:" + " " + "X";
            }
            else
            {
                slottext[i].text = "������ ���輱";
            }
        }
        Datamanager.instance.DataClear();
    }
    public void GameStart(int n)
    {
        if (menuani.mainmenustate == 0)
        {
            saveslotUI.SetActive(false);
            scenefader.SetActive(true);
            menuani.gamestate = 1;
        }
        else if (menuani.mainmenustate == 1)
        {
            if (File.Exists(Datamanager.instance.path + $"{n}"))
            {
                saveslotUI.SetActive(false);
                scenefader.SetActive(true);
                menuani.gamestate = 2;
            }
        }
    }
     public void fileselect(int n)
    {
        Datamanager.instance.nowSlot = n;
        GameStart(n);
    }
    
   
}
