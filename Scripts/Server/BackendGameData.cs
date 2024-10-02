using System.Collections.Generic;
using System.Text;
using UnityEngine;

// �ڳ� SDK namespace �߰�
using BackEnd;
using System;

public class UserData
{
    public string name = "username";
    public float time = 0;
    public int progress = 0;
    public int coin = 0;
    public int weapon = 0;
    public int potion = 0;
    public float hp = 10f;
    public bool record1 = false;
    public bool record2 = false;
    public bool record3 = false;
    public bool record4 = false;
    public bool record5 = false;
    public bool record6 = false;
    public bool record7 = false;
    public bool record8 = false;
    public bool record9 = false;
    public int clearday = 20250000;

    // �����͸� ������ϱ� ���� �Լ��Դϴ�.(Debug.Log(UserData);)
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine($"name: {name}");
        result.AppendLine($"time: {time}");
        result.AppendLine($"progress: {progress}");
        result.AppendLine($"coin: {coin}");
        result.AppendLine($"weapon: {weapon}");
        result.AppendLine($"potion: {potion}");
        result.AppendLine($"hp: {hp}");
        result.AppendLine($"record1: {record1}");
        result.AppendLine($"record2: {record2}");
        result.AppendLine($"record3: {record3}");
        result.AppendLine($"record4: {record4}");
        result.AppendLine($"record5: {record5}");
        result.AppendLine($"record6: {record6}");
        result.AppendLine($"record7: {record7}");
        result.AppendLine($"record8: {record8}");
        result.AppendLine($"record9: {record9}");
        result.AppendLine($"record9: {clearday}");
        return result.ToString();
    }
}

public class BackendGameData
{
    private static BackendGameData _instance = null;

    public static BackendGameData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendGameData();
            }

            return _instance;
        }
    }

    public static UserData userData;

    private string gameDataRowInDate = string.Empty;

    public void GameDataInsert()
    {
        if (userData == null)
        {
            userData = new UserData();
        }

        Debug.Log("�����͸� �ʱ�ȭ�մϴ�.");

        //userData.record5 = true;
        //userData.record7 = true;
        // �ʱ�ȭ �� insert

        Debug.Log("�ڳ� ������Ʈ ��Ͽ� �ش� �����͵��� �߰��մϴ�.");
        Param param = new Param();
        param.Add("name", userData.name);
        param.Add("time", userData.time);
        param.Add("progress", userData.progress);
        param.Add("coin", userData.coin);
        param.Add("weapon", userData.weapon);
        param.Add("potion", userData.potion);
        param.Add("hp", userData.hp);
        param.Add("record1", userData.record1);
        param.Add("record2", userData.record2);
        param.Add("record3", userData.record3);
        param.Add("record4", userData.record4);
        param.Add("record5", userData.record5);
        param.Add("record6", userData.record6);
        param.Add("record7", userData.record7);
        param.Add("record8", userData.record8);
        param.Add("record9", userData.record9);
        param.Add("clearday", userData.clearday);

        Debug.Log("�������� ������ ������ ��û�մϴ�.");
        var bro = Backend.GameData.Insert("USER_DATA", param);

        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ���Կ� �����߽��ϴ�. : " + bro);

            //������ ���������� �������Դϴ�.  
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            Debug.LogError("�������� ������ ���Կ� �����߽��ϴ�. : " + bro);
        }
    }

    public void GameDataGet()
    {
        Debug.Log("���� ���� ��ȸ �Լ��� ȣ���մϴ�.");
        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("���� ���� ��ȸ�� �����߽��ϴ�. : " + bro);


            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json���� ���ϵ� �����͸� �޾ƿɴϴ�.  

            // �޾ƿ� �������� ������ 0�̶�� �����Ͱ� �������� �ʴ� ���Դϴ�.  
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("�����Ͱ� �������� �ʽ��ϴ�.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //�ҷ��� ���������� �������Դϴ�.  

                userData = new UserData();

                userData.name = gameDataJson[0]["name"].ToString();
                userData.time = float.Parse(gameDataJson[0]["time"].ToString());
                userData.progress = int.Parse(gameDataJson[0]["progress"].ToString());
                userData.coin = int.Parse(gameDataJson[0]["coin"].ToString());
                userData.weapon = int.Parse(gameDataJson[0]["weapon"].ToString());
                userData.potion = int.Parse(gameDataJson[0]["potion"].ToString());
                userData.hp = float.Parse(gameDataJson[0]["hp"].ToString());
                userData.record1 = bool.Parse(gameDataJson[0]["record1"].ToString());
                userData.record2 = bool.Parse(gameDataJson[0]["record2"].ToString());
                userData.record3 = bool.Parse(gameDataJson[0]["record3"].ToString());
                userData.record4 = bool.Parse(gameDataJson[0]["record4"].ToString());
                userData.record5 = bool.Parse(gameDataJson[0]["record5"].ToString());
                userData.record6 = bool.Parse(gameDataJson[0]["record6"].ToString());
                userData.record7 = bool.Parse(gameDataJson[0]["record7"].ToString());
                userData.record8 = bool.Parse(gameDataJson[0]["record8"].ToString());
                userData.record9 = bool.Parse(gameDataJson[0]["record9"].ToString());
                userData.potion = int.Parse(gameDataJson[0]["clearday"].ToString());

                Debug.Log(userData.ToString());
            }
        }
        else
        {
            Debug.LogError("���� ���� ��ȸ�� �����߽��ϴ�. : " + bro);
        }
    }

    public void LevelUp()
    {
        Debug.Log("���� ���� ����");
        userData.potion++;
    }
    public void GameClearInteraction()
    {
        DateTime currentDate = DateTime.Now;
        int dateAsInt = int.Parse(currentDate.ToString("yyyyMMdd"));
        userData.clearday = dateAsInt;
        Debug.Log("���� Ŭ���� ���� / ��¥: " + dateAsInt);
    }

    // �������� �����ϱ�
    public void GameDataUpdate()
    {
        if (userData == null)
        {
            Debug.LogError("�������� �ٿ�ްų� ���� ������ �����Ͱ� �������� �ʽ��ϴ�. Insert Ȥ�� Get�� ���� �����͸� �������ּ���.");
            return;
        }

        Param param = new Param();
        param.Add("name", userData.name);
        param.Add("time", userData.time);
        param.Add("progress", userData.progress);
        param.Add("coin", userData.coin);
        param.Add("weapon", userData.weapon);
        param.Add("potion", userData.potion);
        param.Add("hp", userData.hp);
        param.Add("record1", userData.record1);
        param.Add("record2", userData.record2);
        param.Add("record3", userData.record3);
        param.Add("record4", userData.record4);
        param.Add("record5", userData.record5);
        param.Add("record6", userData.record6);
        param.Add("record7", userData.record7);
        param.Add("record8", userData.record8);
        param.Add("record9", userData.record9);

        BackendReturnObject bro = null;

        if (string.IsNullOrEmpty(gameDataRowInDate))
        {
            Debug.Log("�� ���� �ֽ� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}�� �������� ������ ������ ��û�մϴ�.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�������� ������ ������ �����߽��ϴ�. : " + bro);
        }
    }
}