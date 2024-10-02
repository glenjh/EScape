using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BackEnd.Backend;

public class autologin : MonoBehaviour
{
    private const string PlayerIDKey = "PlayerID";
    private const string PlayerPassKey = "PlayerPass";
    private const int IDLength = 8;
    private const int PassLength = 6;

    private void Start()
    {
        var bro = Backend.Initialize(true); // �ڳ� �ʱ�ȭ

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if (bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻�
        }

        // ������ ó�� ������ ��쿡�� ���� ID�� ��й�ȣ ���� �� ����
        if (!PlayerPrefs.HasKey(PlayerIDKey) || !PlayerPrefs.HasKey(PlayerPassKey))
        {
            int randomID = GenerateRandomInt(IDLength);
            int randomPass = GenerateRandomInt(PassLength);

            PlayerPrefs.SetInt(PlayerIDKey, randomID);
            PlayerPrefs.SetInt(PlayerPassKey, randomPass);

            Debug.Log("���ο� ID�� ��й�ȣ�� �����Ǿ����ϴ�!");
            Debug.Log("ID: " + randomID + ", Password: " + randomPass);

            BackendLogin.Instance.CustomSignUp(randomID.ToString(), randomPass.ToString());
        }
        else // �̹� ID�� ��й�ȣ�� �����ϴ� ���
        {
            int existingID = PlayerPrefs.GetInt(PlayerIDKey);
            int existingPass = PlayerPrefs.GetInt(PlayerPassKey);

            Debug.Log("����� ID�� ��й�ȣ�� ã�ҽ��ϴ�!");
            Debug.Log("ID: " + existingID + ", Password: " + existingPass);

            BackendLogin.Instance.CustomLogin(existingID.ToString(), existingPass.ToString());
        }
    }

    // ���� ����(int) ���� �Լ�
    private int GenerateRandomInt(int length)
    {
        System.Random random = new System.Random();
        int min = (int)Mathf.Pow(10, length - 1);
        int max = (int)Mathf.Pow(10, length) - 1;

        return random.Next(min, max);
    }

}
