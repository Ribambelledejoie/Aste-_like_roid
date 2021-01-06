using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBehavior : MonoBehaviour
{

    public int healthPoint = 10;
    [SerializeField] string collisionTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!collision.CompareTag(collisionTag))
        {
            return;
        }

       //healthPoint -= 1; pareil avec ++
        healthPoint--;

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

}
