using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    public Image panel;
    public TextMeshProUGUI text;
    private RectTransform rectTransform;

    #region Singleton
    public static DescriptionPanel Instance;
    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.LogError("DescriptionPanel already exists");
            return;
        }
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        HidePanel();
    }
    #endregion

    public void SetPanel(string s)
    {
        StartCoroutine(ShowPanel(s));
    }

    IEnumerator ShowPanel(string s)
    {
        rectTransform.position = Input.mousePosition;
        text.SetText(s);
        yield return new WaitForSeconds(0.05f);
        Vector2 height = rectTransform.sizeDelta;
        height.y = text.renderedHeight*1.08f;
        rectTransform.sizeDelta = height;
        panel.enabled = true;
        text.alpha = 1;
        yield break;
    }

    public void HidePanel()
    {
        panel.enabled = false;
        text.alpha = 0;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
