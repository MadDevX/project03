using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class PlayerManager : MonoBehaviour, ICharacterManager
{
    public static GameObject playerInstance;
    public GameObject gameOverScreen;
    public GameObject gameUI;
    public string spawnPoint;

    private void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = gameObject;
            DontDestroyOnLoad(gameObject);
            if (LevelLoader.Instance != null)
            {
                LevelLoader.Instance.loadStarted += OnLevelExit;
                LevelLoader.Instance.loadFinished += OnLevelEnter;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnDeath()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);
        playerInstance = null;
    }

    public void OnLevelExit()
    {
        gameObject.SetActive(false);
    }

    public void OnLevelEnter()
    {
        if (spawnPoint != "")
        {
            GameObject area = GameObject.FindGameObjectWithTag(spawnPoint);
            if (area != null)
            {
                Vector3 pos = area.GetComponent<ExitArea>().spawnPoint.position;
                pos.y = 1;
                transform.position = pos;
                gameObject.SetActive(true);
            }
        }
    }
}
