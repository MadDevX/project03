using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] Text progressText;
    public static int CurrentScene { get; private set; }
    public delegate void LoaderStateChanged();
    public LoaderStateChanged loadStarted;
    public LoaderStateChanged loadFinished;

    #region Singleton
    public static LevelLoader Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchonously(sceneIndex));
    }

    IEnumerator LoadAsynchonously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        if(loadStarted!=null)
        {
            loadStarted.Invoke();
        }
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";
            yield return null;
        }
        if(loadFinished!=null)
        {
            loadFinished.Invoke();
        }
        loadingScreen.SetActive(false);
        CurrentScene = sceneIndex;
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield break;
    }
}
