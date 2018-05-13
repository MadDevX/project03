using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int sceneIndex)
    {
        PersistentObject.Instance.levelLoader.LoadLevel(sceneIndex);
        transform.parent.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
