using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(Animator))]

public class HPBehavior : MonoBehaviour
{


    [SerializeField] int healthPoint;
    [SerializeField] string[] collisionTags;

    public UnityEvent onHit;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionTag = collision.tag;

        if(!System.Array.Exists(collisionTags, el => el == collisionTag))
        {
            return;
        }

        onHit.Invoke();    

        healthPoint--;

        if (healthPoint <= 0)
        {                                                  
            Destroy(gameObject);
        }
    }

}
