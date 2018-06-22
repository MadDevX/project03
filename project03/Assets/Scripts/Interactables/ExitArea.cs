using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour
{
    public int sceneIndex;
    public string otherSpawnPoint;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().spawnPoint = otherSpawnPoint;
            LevelLoader.Instance.LoadLevel(sceneIndex);
        }
    }
}
