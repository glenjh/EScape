using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    public Player player;

    public void Update()
    {
        Vector2 direction = (player.mousePos - transform.position).normalized;
        transform.right = direction;

        Vector2 local = transform.localScale;
        if (direction.x < 0)
        {
            local.y = -1;
        }
        else if(direction.x > 0)
        {
            local.y = 1;
        }
        transform.localScale = local;
    }
}
