using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testscript : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {//메인메뉴 탈출 임시용 스크립트
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuani.mainmenustate = 5;
            SceneManager.LoadScene("gamemenu");
        }
    }
}
