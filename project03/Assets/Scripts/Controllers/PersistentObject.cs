using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{

    public static PersistentObject Instance;
    private LevelLoader levelLoader;

    void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.tag = "PersistentObject";
            Instance = this;
            if(levelLoader==null)levelLoader = GetComponentInChildren<LevelLoader>();
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(1);
        }
    }
}
