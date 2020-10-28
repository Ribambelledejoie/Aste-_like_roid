using UnityEngine;
[System.Serializable]

public struct Wave
{
    public float spawnDelay;
    public int enemiesPerWave;

    public Wave(int waveNumber) 
    {
        var waveNumberTenMod = waveNumber % 10;
        spawnDelay = 0.5f; 
        enemiesPerWave = waveNumber;

       switch(waveNumberTenMod)
        {
            case 0: 
                enemiesPerWave = 30;
                spawnDelay = 0.08f;
                break;

        }
    }

}