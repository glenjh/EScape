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
        var bro = Backend.Initialize(true); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }

        // 게임을 처음 시작한 경우에만 랜덤 ID와 비밀번호 생성 후 저장
        if (!PlayerPrefs.HasKey(PlayerIDKey) || !PlayerPrefs.HasKey(PlayerPassKey))
        {
            int randomID = GenerateRandomInt(IDLength);
            int randomPass = GenerateRandomInt(PassLength);

            PlayerPrefs.SetInt(PlayerIDKey, randomID);
            PlayerPrefs.SetInt(PlayerPassKey, randomPass);

            Debug.Log("새로운 ID와 비밀번호가 생성되었습니다!");
            Debug.Log("ID: " + randomID + ", Password: " + randomPass);

            BackendLogin.Instance.CustomSignUp(randomID.ToString(), randomPass.ToString());
        }
        else // 이미 ID와 비밀번호가 존재하는 경우
        {
            int existingID = PlayerPrefs.GetInt(PlayerIDKey);
            int existingPass = PlayerPrefs.GetInt(PlayerPassKey);

            Debug.Log("저장된 ID와 비밀번호를 찾았습니다!");
            Debug.Log("ID: " + existingID + ", Password: " + existingPass);

            BackendLogin.Instance.CustomLogin(existingID.ToString(), existingPass.ToString());
        }
    }

    // 랜덤 숫자(int) 생성 함수
    private int GenerateRandomInt(int length)
    {
        System.Random random = new System.Random();
        int min = (int)Mathf.Pow(10, length - 1);
        int max = (int)Mathf.Pow(10, length) - 1;

        return random.Next(min, max);
    }

}
