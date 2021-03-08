using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public int enemyCount;

    public void ChangeScene()
    {
        if(enemyCount <= 0)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
