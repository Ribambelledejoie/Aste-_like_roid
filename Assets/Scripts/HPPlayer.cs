using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPlayer : MonoBehaviour
{

    public int healthPoint = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //healthPoint -= 1; pareil avec ++
        healthPoint--;
        Debug.Log(healthPoint);
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

}
