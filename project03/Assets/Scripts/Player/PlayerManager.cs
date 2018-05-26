using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class PlayerManager : MonoBehaviour, ICharacterManager
{
    public static GameObject playerInstance;
    public GameObject gameOverScreen;
    public GameObject gameUI;

    private void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = gameObject;
        }
    }

    public void OnDeath()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);
        PlayerInventory.Instance = null;
        PlayerManager.playerInstance = null;
    }
}
