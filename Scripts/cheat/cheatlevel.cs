using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class cheatlevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Datamanager.instance.nowSlot = 0;
        if (File.Exists(Datamanager.instance.path + $"{0}"))
        {
            Datamanager.instance.LoadData();
            menuani.gamestate = 0;
        }

    }

    // Update is called once per frame
   
}
