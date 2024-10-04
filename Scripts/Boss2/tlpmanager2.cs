using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tlpmanager2 : MonoBehaviour
{

    public GameObject Object;
    public Boss2 Boss;
    public GameObject Bosspos;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Boss.bossHP <= 0)
        {

            Object.SetActive(true);
            Vector3 temp = Bosspos.transform.position;
            temp.y += 3;
            Object.transform.position= temp;
            Destroy(this);
        }
    }
}
