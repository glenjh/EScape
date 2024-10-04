using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightmanager : MonoBehaviour
{
    public float time;
    public bool status;
    public static int count;
    public static bool upstatus;
    bool initial;
    public Player player;
    public Text timer;
    public Text daynight;
    public Text becoming;
    public Text counttext;
    public Boss2 boss;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        time = 40;
        count = 2;
        status = true;
        upstatus = true;
        initial = false;
        daynight.text = "(≥∑)";
        becoming.text = "(π„)¿Ã µ…∂ß±Ó¡ˆ:";
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.bossHP <= 0)
        {
            Destroy(manager);
        }
        timer.text = ((int)time).ToString();
        counttext.text = count.ToString();
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if (time <= 0)
        {
            time = 40;
            if (status == true)
            {
                daynight.text = "(π„)";
                becoming.text = "(≥∑)¿Ã µ…∂ß±Ó¡ˆ:";
                status = false;
            }
            else
            {
                daynight.text = "(≥∑)";
                becoming.text = "(π„)¿Ã µ…∂ß±Ó¡ˆ:";
                status = true;
            }
        }
        if (status == true && count == 0)
        {
            upstatus = false;
            if (initial == false)
            {
                player.markCnt--;
                initial = true;
            }

        }
        else if (status == false && count == 4)
        {
            upstatus = false;
            if (initial == false)
            {
                player.markCnt--;
                initial = true;
            }

        }
        else
        {
            upstatus = true;
            initial = false;
        }
    }
}
