using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }

}
