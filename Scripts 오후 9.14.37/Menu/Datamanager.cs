using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using System.IO;
using TMPro;
using System.IO.Enumeration;
using System;

public class Playerdata
{
    //�̸�,�ð�,���൵,����,�������� ����, ������ �ִ� ������
    public string name;
    public float time=0;
    public int progress=0;
    public int coin=0;
    public int weapon=0;
    public int potion=0;
    public float hp = 10f;
    public bool record1=false;
    public bool record2 = false;
    public bool record3 = false;
    public bool record4 = false;
    public bool record5 = false;
    public bool record6 = false;
    public bool record7 = false;
    public bool record8 = false;
    public bool record9 = false;

}
public class Datamanager : MonoBehaviour
{
    public static Datamanager instance;
    
    public Playerdata nowPlayer = new Playerdata();
    public string path;
    public int nowSlot;
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        path = Application.persistentDataPath+"/save";
        
    }
   
    // Update is called once per frame
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path+nowSlot.ToString(), data);
    }
    public void LoadData()
    {
        string data = File.ReadAllText(path+nowSlot.ToString());
       nowPlayer= JsonUtility.FromJson<Playerdata>(data);
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new Playerdata();
    }
}
