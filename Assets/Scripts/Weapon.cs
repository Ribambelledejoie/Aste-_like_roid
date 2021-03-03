using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    public GameObject bullet;
    public float fireRate;
    public float bulletForce;

    public abstract void Shoot(Transform FirePoint);
}
