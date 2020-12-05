using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private PolygonCollider2D playGround;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject pickUp;
    private int enemyCount;

    private int waveNumber;
    private Wave actualWave;

    // Start is called before the first frame update
    void Start()
    {
        playGround = GetComponent<PolygonCollider2D>();
        LaunchNewWave();
        //SpawnEnemy();

    }

    void SpawnEnemy ()
    {
        var points = playGround.points;
        var randomIndex = Random.Range(0, points.Length);
        var chosenPoint = points[randomIndex];
        var otherPoint = Vector2.zero;

        if (randomIndex == 0)
        {
            otherPoint = points[points.Length - 1];
        }
        else
        {
            otherPoint = points[randomIndex - 1];
        }

        var positionToSpawn = Vector2.Lerp(chosenPoint, otherPoint, Random.value);

        Instantiate(enemy, positionToSpawn, Quaternion.identity);
        
    }


    // ici, on vient dire au jeu de spawn des pickups à l'intérieur de la zone de jeu playGround, et on instantiate les pickups après
    void SpawnPickUp ()
    {
        var points = playGround.points;
        var randomIndex = Random.Range(0, points.Length);
        var chosenPoint = points[randomIndex];
        var otherRandomIndex = Random.Range(0, points.Length);

        if(Mathf.Abs(otherRandomIndex - randomIndex) <= 1)
        {
            otherRandomIndex += 2;
        }

        if(otherRandomIndex > points.Length - 1)
        {
            otherRandomIndex -= points.Length - 1;
        }

        var otherPoint = points[otherRandomIndex];

        var positionToSpawn = Vector2.Lerp(chosenPoint, otherPoint, Random.value);

        Instantiate(pickUp, positionToSpawn, Quaternion.identity);

    }

    IEnumerator Timer ()
    {

        // while (enemyCount < numberEnemies.Evaluate(waveNumber))

        var enemySpawned = 0;

        while (enemySpawned < actualWave.enemiesPerWave)
        {
            SpawnEnemy();
            enemySpawned++;
            yield return new WaitForSeconds(actualWave.spawnDelay);
        }

    }

    private void LaunchNewWave()
    {
        // permet de faire spawn les pickups en début de chaque wave
        SpawnPickUp();

        waveNumber++;
        actualWave = new Wave(waveNumber);

        enemyCount = actualWave.enemiesPerWave;
        
        StartCoroutine(Timer());

    }


    public void EnemyDestroyed ()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            LaunchNewWave();
        }
    }

}
