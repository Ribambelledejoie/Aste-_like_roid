using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private PolygonCollider2D playGround;
    private GameObject enemy;
    [SerializeField] private int enemyCount;
    

    // Start is called before the first frame update
    void Start()
    {
        /*Ici on Get le PolygonCollider
        ensuite on crée une variable permettant de connaitre les différents points du polygoncollider
        on crée une variable qui va nous permettre de randomiser le point choisi, entre 0 et le nombre de points du polygoncollider
        le chosenPoint est une variable 


        */
        playGround = GetComponent<PolygonCollider2D>();
        var points = playGround.points;
        var randomIndex = Random.Range(0, points.Length);
        var chosenPoint = points[randomIndex];
        var otherPoint = Vector2.zero;
        var positionToSpawn = Vector2.Lerp(chosenPoint, otherPoint, Random.value);

        if (randomIndex == 0)
        {
            otherPoint = points[points.Length - 1];
        }
        else
        {
            otherPoint = points[randomIndex - 1];
        }

        /*
        while (enemyCount < 10)
        {
            Instantiate(enemy, chosenPoint, otherPoint); 
        }
        */
    }

}
