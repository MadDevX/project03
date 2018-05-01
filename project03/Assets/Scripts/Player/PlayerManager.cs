using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class PlayerManager : MonoBehaviour, ICharacterManager
{
    public GameObject gameOverScreen;
    public GameObject gameUI;

    public void OnDeath()
    {
        
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}
