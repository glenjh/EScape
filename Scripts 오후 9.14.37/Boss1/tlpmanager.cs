using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tlpmanager : MonoBehaviour
{

    public GameObject Object;
    public Boss1 Boss;
    public GameObject Bosspos;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (Boss.bossHP <= 0)
        {

            Object.SetActive(true);
            Object.transform.position = Bosspos.transform.position;
            Destroy(this);
        }
    }
}
