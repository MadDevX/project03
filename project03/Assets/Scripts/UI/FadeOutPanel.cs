using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutPanel : MonoBehaviour {

    public float fadeOutFrames = 200;
    private Image img;
	// Use this for initialization
	void Start ()
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
        float frequency = 1 / fadeOutFrames;
        Color c = img.color;
        for (float f = c.a; f >= -frequency; f -= frequency)
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
        Destroy(gameObject);
    }
}
