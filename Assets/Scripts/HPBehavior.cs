using UnityEngine;

public class HPBehavior : MonoBehaviour
{

    [SerializeField] int healthPoint;
    [SerializeField] string[] collisionTags;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionTag = collision.tag;

        if(!System.Array.Exists(collisionTags, el => el == collisionTag))
        {
            return;
        }

        healthPoint--;

        if (healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

}
