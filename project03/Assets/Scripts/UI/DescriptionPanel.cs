using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour
{
    public Vector3 offset;
    public GameObject panel;
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
        //rectTransform.position = Input.mousePosition + offset;
        text.SetText(s);
        yield return new WaitForSeconds(0.05f);
        Vector2 height = rectTransform.sizeDelta;
        height.y = text.renderedHeight+30;
        rectTransform.sizeDelta = height;
        panel.SetActive(true);
        text.alpha = 1;
        yield break;
    }

    IEnumerator ShowPanel(ItemDetails item)
    {
        yield break;
    }

    IEnumerator ShowPanel(InteractableDetails item)
    {
        yield break;
    }

    public void HidePanel()
    {
        panel.SetActive(false);
        text.alpha = 0;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
