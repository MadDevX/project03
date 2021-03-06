﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutPanel : MonoBehaviour {

    public float fadeOutFrames = 200;
    private Image img;

	void Awake ()
    {
        img = GetComponent<Image>();
        if(img!=null)
        {
            img.enabled = true;
            StartCoroutine("FadePanel");
        }
	}

    IEnumerator FadePanel()
    {
        img.enabled = true;
        float frequency = 1 / fadeOutFrames;
        Color c = img.color;
        for (float f = 1; f >= -frequency; f -= frequency)
        {
            if (f <= 0)
            {
                c.a = 0;
                img.color = c;
                break;
            }
            c.a = f;
            img.color = c;
            yield return null;
        }
        img.enabled = false;
    }
}
