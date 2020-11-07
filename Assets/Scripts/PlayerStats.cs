using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct PlayerStats 
{
    // on permet d'avoir des stats pour le joueur / et on viens multiplier chaque stats entre elles

    public float HP;
    public float fireRate;
    public float speed;
    public float maxSpeed;
    public float bulletForce;

    public static PlayerStats operator * (PlayerStats left, PlayerStats right)
    {
        PlayerStats result = new PlayerStats
        {
            HP = left.HP * right.HP,
            fireRate = left.fireRate * right.fireRate,
            speed = left.speed * right.speed,
            maxSpeed = left.maxSpeed * right.maxSpeed,
            bulletForce = left.bulletForce * right.maxSpeed
        };

        return result;

    }

}
