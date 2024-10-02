using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class cheatprogress : MonoBehaviour
{
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //Datamanager.instance.nowPlayer.progress = 4;
            //Datamanager.instance.SaveData();
            SceneManager.LoadScene("Dungeon2-4");
        }
    }
}
