using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private PolygonCollider2D playGround;
    [SerializeField] private GameObject enemy;
    private int enemyCount;

    private float timeRemaining;
    [SerializeField] private float spawnDelay;
    private float totalTime;

    private int waveNumber;

    [SerializeField] private Wave[] waves;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
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

        enemyCount++;

        Instantiate(enemy, positionToSpawn, Quaternion.identity);
        
    }

    IEnumerator Timer ()
    {
       
        while (enemyCount < waves[waveNumber].enemiesPerWave)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(waves[waveNumber].spawnDelay);
        }

    }

    public void EnemyDestroyed ()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            waveNumber++;
            StartCoroutine(Timer());
        }
    }

}
