using UnityEngine;
using System.Threading.Tasks; // [변경] async 기능을 이용하기 위해서는 해당 namepsace가 필요합니다.  

// 뒤끝 SDK namespace 추가
using BackEnd;

public class BackendManager : MonoBehaviour
{
    void Start()
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

        //Test();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            BackendRank.Instance.RankInsert(BackendGameData.userData.clearday); // [추가] 랭킹 등록하기 함수
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            BackendGameData.userData.clearday = 20231124;
            Debug.Log("Clearday -> 20231123 for test");
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            BackendRank.Instance.RankGet(); // [추가] 랭킹 불러오기 함수
        }
    }

    // =======================================================
    // [추가] 동기 함수를 비동기에서 호출하게 해주는 함수(유니티 UI 접근 불가)
    // =======================================================
    async void Test()
    {
        await Task.Run(() => {
            //BackendLogin.Instance.CustomSignUp("user2", "1234"); // 뒤끝 회원가입 함수

            BackendLogin.Instance.CustomLogin("user2", "1234"); // 뒤끝 로그인

            BackendLogin.Instance.UpdateNickname("dasusamida"); // 닉네임 변경

            //BackendGameData.Instance.GameDataInsert();// 데이터 삽입 함수

            BackendGameData.Instance.GameDataGet(); // 데이터 불러오기 함수

            if (BackendGameData.userData == null)
            {
                BackendGameData.Instance.GameDataInsert();
            } // 서버에 불러온 데이터가 존재하지 않을 경우, 데이터를 새로 생성하여 삽입

            //BackendGameData.Instance.LevelUp(); // 로컬에 저장된 데이터를 변경

            //BackendGameData.Instance.GameDataUpdate(); // 서버에 저장된 데이터를 덮어쓰기(변경된 부분만)


            Debug.Log("테스트를 종료합니다.");
        });
    }
}