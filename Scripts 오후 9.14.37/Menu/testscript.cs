using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testscript : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {//���θ޴� Ż�� �ӽÿ� ��ũ��Ʈ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuani.mainmenustate = 5;
            SceneManager.LoadScene("gamemenu");
        }
    }
}
