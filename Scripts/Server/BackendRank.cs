using System.Collections.Generic;
using System.Text;
using UnityEngine;

// �ڳ� SDK namespace �߰�
using BackEnd;

public class BackendRank
{

    private static BackendRank _instance = null;

    public static BackendRank Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendRank();
            }

            return _instance;
        }
    }

    public void RankInsert(int score)
    {
        string rankUUID = "0d679a00-8887-11ee-acce-7fbb598f7ba2"; // Clear Day ��ũ ���̺� uuid

        string tableName = "USER_DATA";
        string rowInDate = string.Empty;

        // ��ŷ�� �����ϱ� ���ؼ��� ���� �����Ϳ��� ����ϴ� �������� inDate���� �ʿ��մϴ�.  
        // ���� �����͸� �ҷ��� ��, �ش� �������� inDate���� �����ϴ� �۾��� �ؾ��մϴ�.  
        Debug.Log("������ ��ȸ�� �õ��մϴ�.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("������ ��ȸ �� ������ �߻��߽��ϴ� : " + bro);
            return;
        }

        Debug.Log("������ ��ȸ�� �����߽��ϴ� : " + bro);

        if (bro.FlattenRows().Count > 0)
        {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else
        {
            Debug.Log("�����Ͱ� �������� �ʽ��ϴ�. ������ ������ �õ��մϴ�.");
            var bro2 = Backend.GameData.Insert(tableName);

            if (bro2.IsSuccess() == false)
            {
                Debug.LogError("������ ���� �� ������ �߻��߽��ϴ� : " + bro2);
                return;
            }

            Debug.Log("������ ���Կ� �����߽��ϴ� : " + bro2);

            rowInDate = bro2.GetInDate();
        }

        Debug.Log("�� ���� ������ rowInDate : " + rowInDate); // ����� rowIndate�� ���� ������ �����ϴ�.  

        if (score == 20250000)
        {
            Debug.Log("Ŭ���� ���� 20250000(�ʱⰪ)���� ��ŷ ��� ����");
            return;
        } // clearday(score) ���� �ʱⰪ�� ��� ����

        Param param = new Param();
        param.Add("clearday", score);

        // ����� rowIndate�� ���� �����Ϳ� param������ ������ �����ϰ� ��ŷ�� �����͸� ������Ʈ�մϴ�.  
        Debug.Log("��ŷ ������ �õ��մϴ�.");
        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if (rankBro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��� �� ������ �߻��߽��ϴ�. : " + rankBro);
            return;
        }

        Debug.Log("��ŷ ���Կ� �����߽��ϴ�. : " + rankBro);
    }

    public void RankGet()
    {
        string rankUUID = "0d679a00-8887-11ee-acce-7fbb598f7ba2"; // Clear Day ��ũ ���̺� uuid
        var bro = Backend.URank.User.GetRankList(rankUUID);

        if (bro.IsSuccess() == false)
        {
            Debug.LogError("��ŷ ��ȸ�� ������ �߻��߽��ϴ�. : " + bro);
            return;
        }
        Debug.Log("��ŷ ��ȸ�� �����߽��ϴ�. : " + bro);

        Debug.Log("�� ��ŷ ��� ���� �� : " + bro.GetFlattenJSON()["totalCount"].ToString());

        foreach (LitJson.JsonData jsonData in bro.FlattenRows())
        {
            StringBuilder info = new StringBuilder();

            info.AppendLine("���� : " + jsonData["rank"].ToString());
            info.AppendLine("�г��� : " + jsonData["nickname"].ToString());
            info.AppendLine("Ŭ���� ��¥ : " + jsonData["score"].ToString());
            info.AppendLine("gamerInDate : " + jsonData["gamerInDate"].ToString());
            info.AppendLine("���Ĺ�ȣ : " + jsonData["index"].ToString());
            info.AppendLine();
            Debug.Log(info);
        }
    }
}