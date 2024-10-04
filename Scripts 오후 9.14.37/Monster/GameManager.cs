using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update

    //public Player player;  :need player

    void Awake()
    {
        Instance = this;
    }


}
