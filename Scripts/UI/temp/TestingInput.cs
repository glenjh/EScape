using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class TestingInput : MonoBehaviour
{
    int currentDay;
    private void Start()
    {
        DateTime currentDate = DateTime.Now;
        currentDay = currentDate.Year * 10000 + currentDate.Month * 100 + currentDate.Day;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            Debug.Log("HOMEŰ�Է�");
            GetInDate();
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            Debug.Log("PGUPŰ�Է�");
            Datainsert();
        }

        if(Input.GetKeyDown(KeyCode.Delete))
        {
            Debug.Log("DELETEŰ�Է�");
            PlayerPrefs.DeleteAll();
        }
    }
    public void GetInDate()
    {
        var bro = Backend.PlayerData.GetAllData("Clearlog");

        if (!bro.IsSuccess())
        {
            Debug.LogError(bro.ToString());
            return;
        }

        LitJson.JsonData gameDataListJson = bro.FlattenRows();

        if (gameDataListJson.Count <= 0)
        {
            Debug.Log("�����Ͱ� �������� �ʽ��ϴ�");
            return;
        }
        string[] selectList = new string[] { "clearday", "nickname" };

        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            StringBuilder info = new StringBuilder();
            info.AppendLine("�г��� : " + jsonData["nickname"].ToString());
            info.AppendLine("��¥ : " + jsonData["clearday"].ToString());
            Debug.Log(info);
        }

    }

    public void Datainsert()
    {
        Param data = new Param();
        data.Add("nickname", Datamanager.instance.nowPlayer.name);
        data.Add("clearday", currentDay);

        Backend.PlayerData.InsertData("Clearlog", data);
        Debug.Log("clearday ������ �Է�");
    }
}
