using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private PolygonCollider2D playGround;
    [SerializeField] private GameObject enemy;
    private int enemyCount;

    private int waveNumber;
    private Wave actualWave;

    // Start is called before the first frame update
    void Start()
    {
        LaunchNewWave();
        //SpawnEnemy();

    }

    void SpawnEnemy ()
    {
        playGround = GetComponent<PolygonCollider2D>();
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

    IEnumerator Timer ()
    {

        // while (enemyCount < numberEnemies.Evaluate(waveNumber))

        var enemySpawned = 0;

        Debug.Log(actualWave.enemiesPerWave);
        while (enemySpawned < actualWave.enemiesPerWave)
        {
            SpawnEnemy();
            enemySpawned++;
            yield return new WaitForSeconds(actualWave.spawnDelay);
        }

    }

    private void LaunchNewWave()
    {

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
            Debug.Log("new wave");
        }
    }

}
