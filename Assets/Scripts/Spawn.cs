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

    [SerializeField] private AnimationCurve numberEnemies;
    [SerializeField] private AnimationCurve spawnDelay;

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

        enemyCount++;

        Instantiate(enemy, positionToSpawn, Quaternion.identity);
        
    }

    IEnumerator Timer ()
    {

        // while (enemyCount < numberEnemies.Evaluate(waveNumber))
        Debug.Log(actualWave.enemiesPerWave);
        while (enemyCount < actualWave.enemiesPerWave)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(actualWave.spawnDelay);
        }

    }

    private void LaunchNewWave()
    {

        waveNumber++;
        //Si notre waveNumber correspond au Time (x) de la dernière clé de la courbe numberEnemies
        if (waveNumber == numberEnemies.keys[numberEnemies.keys.Length - 1].time)
            ModifyCurves();

        //RoundToInt : approcher le plus possible de la valeur
        //FloorToInt : supprime la décimale

        actualWave.enemiesPerWave = Mathf.RoundToInt(numberEnemies.Evaluate(waveNumber));
        actualWave.spawnDelay = Mathf.RoundToInt(spawnDelay.Evaluate(waveNumber));

        StartCoroutine(Timer());

    }

    private void ModifyCurves()
    {

        var lastKey = numberEnemies.keys[numberEnemies.keys.Length - 1];

        //i pour index / tant que l'index est inférieur au nombre de clé, à chaque fin de boucle on va augmenter l'index de 1
        for (var i = 0; i < numberEnemies.keys.Length; i++) 
        {
            numberEnemies.keys[i].time += lastKey.time;
            numberEnemies.keys[i].value += lastKey.value;
        }

        lastKey = spawnDelay.keys[spawnDelay.keys.Length - 1];

        for (var i = 0; i < spawnDelay.keys.Length; i++)
        {
            spawnDelay.keys[i].time += lastKey.time;
            spawnDelay.keys[i].value += lastKey.value;
        }

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
