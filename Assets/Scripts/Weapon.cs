using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public GameObject bullet;
    public float fireRate;
    public float bulletForce;

    public abstract void Shoot(Transform FirePoint);
}
