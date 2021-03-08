using UnityEngine;

//[RequireComponent(typeof(Animator))]

public class HPBehavior : MonoBehaviour
{

    [SerializeField] int healthPoint;
    [SerializeField] string[] collisionTags;

    private Animator anim;
    private AsteroidsBehavior asteroidBehavior;
    private enum Type {projectile, player, ennemy};

    [SerializeField] private Type type;

    private static ChangeLevel levelChange;

    private void Awake()
    {
        switch (type)
        {
            case Type.ennemy:
                {
                    if (levelChange == null)
                    {
                        levelChange = FindObjectOfType<ChangeLevel>();
                    }
                    levelChange.enemyCount++;

                    if (GetComponent<AsteroidsBehavior>())
                    {
                        asteroidBehavior = GetComponent<AsteroidsBehavior>();
                    }
                    break;
                }
        }

       
       
        if (GetComponent<Animator>())
        {
            anim = GetComponent<Animator>();
        }
        
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionTag = collision.tag;

        if(!System.Array.Exists(collisionTags, el => el == collisionTag))
        {
            return;
        }

        if(anim != null)
        {
            anim.SetTrigger("hit");
        }

        if(asteroidBehavior != null)
        {
            asteroidBehavior.ChangeSpeed(0.1f);
        }

        healthPoint--;

        if (healthPoint <= 0)
        {
            switch (type)
            {
                case Type.ennemy:
                {
                        levelChange.enemyCount--;
                        levelChange.ChangeScene();
                        break;
                }
                    
            }
            
            Destroy(gameObject);
        }
    }

}
