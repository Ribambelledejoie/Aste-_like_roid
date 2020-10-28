using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPlayer : MonoBehaviour
{

    [SerializeField] private int healthPoint = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //healthPoint -= 1; pareil avec ++
        healthPoint--;

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

}
