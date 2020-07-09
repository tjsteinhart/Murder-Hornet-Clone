using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    int currentSceneIndex;
    public int GetCurrentSceneIndex() => currentSceneIndex;

    void Start()
    {
        currentSceneIndex = 0;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        currentSceneIndex += 1;
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentSceneIndex));
    }

    public void NextLevel()
    {
        currentSceneIndex += 1;
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(currentSceneIndex);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

}
