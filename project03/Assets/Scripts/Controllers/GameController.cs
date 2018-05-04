using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Helper;

public class GameController : MonoBehaviour {

    
    public GameObject enemyPrefab;
    public Transform[] spawnPositions;
    public GameObject UICanvas;
    public GameObject gameOverScreen;
    public GameObject gameUI;
    private int enemiesLeft;
    // Use this for initialization
    void Start()
    {
        foreach (Transform t in spawnPositions)
        {
            Instantiate(enemyPrefab, t.position, t.rotation);
        }
        enemiesLeft = spawnPositions.Length;
    }

    public void EnemyKilled()
    {
        enemiesLeft--;
        if(enemiesLeft==0)
        {
            GameWon();
        }
    }

    void GameWon()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        gameOverScreen.GetComponentInChildren<TextMeshProUGUI>().SetText("u won");
        gameUI.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(1);
    }
}
