using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : Singleton<SceneControl>
{

    bool isAsyncLoading = false;

    public event Action OnComplete;

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadScene(SceneType scene)
    {
        LoadScene((int)scene);
    }

    public void LoadSceneAsync(int scene)
    {
        if(isAsyncLoading)
        {
            return;
        }

        StartCoroutine(LoadAsync(scene));
    }

    private IEnumerator LoadAsync(int sceneIndex)
    {
        isAsyncLoading = true;
        int prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Logger.Log($"이전 씬 인덱스 = {prevSceneIndex}");

        var handle = SceneManager.LoadSceneAsync(sceneIndex,LoadSceneMode.Additive);
        handle.allowSceneActivation = false;

        while(true)
        {
            Logger.Log($"씬 로딩 진행도 = {handle.progress}");
            if(handle.progress >= 0.9f)
            {
                break;
            }

            yield return null;
        }

        var prevScene = SceneManager.GetSceneByBuildIndex(prevSceneIndex);
        var curScene = SceneManager.GetSceneByBuildIndex(sceneIndex);

        handle.allowSceneActivation = true;

        while(handle.isDone == false)
        {
            yield return null;
        }

        handle = SceneManager.UnloadSceneAsync(prevScene);

        SceneManager.SetActiveScene(curScene);
        OnComplete?.Invoke();   
        isAsyncLoading = false;
    }
}