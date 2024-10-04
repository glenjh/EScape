using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : MonoBehaviour
{
    public Player player;
    public float time = 2f;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.SetStun(time);
        }
    }
}
