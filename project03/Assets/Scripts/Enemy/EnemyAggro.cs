﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}