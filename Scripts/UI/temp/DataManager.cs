using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PlayerData
{
    public string name;
    public int coin;
    public int weapon;
    public int equip;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();

    string path;
    string filename = "save";

    public static Action save;
    public static Action load;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);

        save = () => { SaveData(); };
        save = () => { LoadData(); };

        path = Application.persistentDataPath + "/";
    }


    void Start()
    {
        print(path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + filename, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + filename);
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
}
