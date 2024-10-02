using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    public float damage;
    public float shootingDelay, reloadingTime, projectileSpeed;
    public int maxAmmo, currAmmo;
    public bool repeater;
}
