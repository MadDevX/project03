using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public static int CurrentScene { get; private set; }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchonously(sceneIndex));
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 1.0f/60.0f * Time.timeScale;
    }

    IEnumerator LoadAsynchonously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
        loadingScreen.SetActive(false);
        CurrentScene = sceneIndex;
        yield break;
    }
}
