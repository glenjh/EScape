using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class InterAction : MonoBehaviour
{
    public SpriteRenderer img;
    public int weaponNum; // 떨어져있는 무기번호

    public void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }
}
