using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private string sceneName;
    public int enemyCount;

    public void ChangeScene()
    {

        StartCoroutine(LoadScene());

        /*
        if(enemyCount <= 0)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        */
    }

    private IEnumerator LoadScene()
    {

        var loadingScreenInstance = Instantiate(loadingScreen);

        DontDestroyOnLoad(loadingScreenInstance);

        var animLoadingScreenInstance = loadingScreenInstance.GetComponent<Animator>();

        var animTime = animLoadingScreenInstance.GetCurrentAnimatorStateInfo(0).length;

        var loading = SceneManager.LoadSceneAsync(sceneName);

        loading.allowSceneActivation = false;

        while(!loading.isDone)
        {

            if(loading.progress >= 0.9f)
            {
                animLoadingScreenInstance.SetTrigger("Disappear");

                loading.allowSceneActivation = true;

            }

            yield return new WaitForSeconds(animTime);
        }

    }

}
