using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bow", menuName = "Weapon/Bow")]
public class BowData : ScriptableObject
{
    public int increaseRate;
    public int defaultDamage, maxDamage;
    public float actualDamage;
    public float shootingDelay, projectileSpeed, maxProjectileSpeed;
}
