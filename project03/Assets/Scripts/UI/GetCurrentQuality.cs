using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCurrentQuality : MonoBehaviour {

    private void Awake()
    {
        GetComponent<Dropdown>().value = QualitySettings.GetQualityLevel();
    }
}
