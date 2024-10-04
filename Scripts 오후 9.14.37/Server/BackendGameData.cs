using System.Collections.Generic;
using System.Text;
using UnityEngine;

// 뒤끝 SDK namespace 추가
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

    // 데이터를 디버깅하기 위한 함수입니다.(Debug.Log(UserData);)
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

        Debug.Log("데이터를 초기화합니다.");

        //userData.record5 = true;
        //userData.record7 = true;
        // 초기화 후 insert

        Debug.Log("뒤끝 업데이트 목록에 해당 데이터들을 추가합니다.");
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

        Debug.Log("게임정보 데이터 삽입을 요청합니다.");
        var bro = Backend.GameData.Insert("USER_DATA", param);

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 삽입에 성공했습니다. : " + bro);

            //삽입한 게임정보의 고유값입니다.  
            gameDataRowInDate = bro.GetInDate();
        }
        else
        {
            Debug.LogError("게임정보 데이터 삽입에 실패했습니다. : " + bro);
        }
    }

    public void GameDataGet()
    {
        Debug.Log("게임 정보 조회 함수를 호출합니다.");
        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());
        if (bro.IsSuccess())
        {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);


            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json으로 리턴된 데이터를 받아옵니다.  

            // 받아온 데이터의 갯수가 0이라면 데이터가 존재하지 않는 것입니다.  
            if (gameDataJson.Count <= 0)
            {
                Debug.LogWarning("데이터가 존재하지 않습니다.");
            }
            else
            {
                gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //불러온 게임정보의 고유값입니다.  

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
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
        }
    }

    public void LevelUp()
    {
        Debug.Log("포션 개수 증가");
        userData.potion++;
    }
    public void GameClearInteraction()
    {
        DateTime currentDate = DateTime.Now;
        int dateAsInt = int.Parse(currentDate.ToString("yyyyMMdd"));
        userData.clearday = dateAsInt;
        Debug.Log("게임 클리어 저장 / 날짜: " + dateAsInt);
    }

    // 게임정보 수정하기
    public void GameDataUpdate()
    {
        if (userData == null)
        {
            Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다. Insert 혹은 Get을 통해 데이터를 생성해주세요.");
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
            Debug.Log("내 제일 최신 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.Update("USER_DATA", new Where(), param);
        }
        else
        {
            Debug.Log($"{gameDataRowInDate}의 게임정보 데이터 수정을 요청합니다.");

            bro = Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param);
        }

        if (bro.IsSuccess())
        {
            Debug.Log("게임정보 데이터 수정에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("게임정보 데이터 수정에 실패했습니다. : " + bro);
        }
    }
}