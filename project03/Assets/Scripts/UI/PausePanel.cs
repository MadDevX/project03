using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;

    private void OnEnable()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
