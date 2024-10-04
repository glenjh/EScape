using UnityEngine;
using System.Threading.Tasks; // [����] async ����� �̿��ϱ� ���ؼ��� �ش� namepsace�� �ʿ��մϴ�.  

// �ڳ� SDK namespace �߰�
using BackEnd;

public class BackendManager : MonoBehaviour
{
    void Start()
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

        //Test();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            BackendRank.Instance.RankInsert(BackendGameData.userData.clearday); // [�߰�] ��ŷ ����ϱ� �Լ�
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            BackendGameData.userData.clearday = 20231124;
            Debug.Log("Clearday -> 20231123 for test");
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            BackendRank.Instance.RankGet(); // [�߰�] ��ŷ �ҷ����� �Լ�
        }
    }

    // =======================================================
    // [�߰�] ���� �Լ��� �񵿱⿡�� ȣ���ϰ� ���ִ� �Լ�(����Ƽ UI ���� �Ұ�)
    // =======================================================
    async void Test()
    {
        await Task.Run(() => {
            //BackendLogin.Instance.CustomSignUp("user2", "1234"); // �ڳ� ȸ������ �Լ�

            BackendLogin.Instance.CustomLogin("user2", "1234"); // �ڳ� �α���

            BackendLogin.Instance.UpdateNickname("dasusamida"); // �г��� ����

            //BackendGameData.Instance.GameDataInsert();// ������ ���� �Լ�

            BackendGameData.Instance.GameDataGet(); // ������ �ҷ����� �Լ�

            if (BackendGameData.userData == null)
            {
                BackendGameData.Instance.GameDataInsert();
            } // ������ �ҷ��� �����Ͱ� �������� ���� ���, �����͸� ���� �����Ͽ� ����

            //BackendGameData.Instance.LevelUp(); // ���ÿ� ����� �����͸� ����

            //BackendGameData.Instance.GameDataUpdate(); // ������ ����� �����͸� �����(����� �κи�)


            Debug.Log("�׽�Ʈ�� �����մϴ�.");
        });
    }
}