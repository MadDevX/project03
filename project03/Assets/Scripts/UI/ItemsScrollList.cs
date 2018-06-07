using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsScrollList : MonoBehaviour
{
    public bool isStatic;
    public Transform contentPanel;
    public GameObject itemListUI;
    public TextMeshProUGUI containerLabel;

    private CharacterInventory container;

    public CharacterInventory Container
    {
        get
        {
            return container;
        }
        
        set
        {
            if (container != null)
            {
                container.onItemChangedCallback = null;
            }
            container = value;
            if(container!=null)
            {
                container.onItemChangedCallback += RefreshDisplay;
            }
        }
    }

    #region Singleton
    public static ItemsScrollList Instance;

    private void Awake()
    {
        if (isStatic)
        {
            if (Instance != null)
            {
                Debug.LogWarning("Another static ItemsScrollList is instantiated.");
                return;
            }
            Instance = this;
        }
    }
    #endregion
    // Use this for initialization
    void Start ()
    {
        RefreshDisplay(container);
        if (container == null)
        {
            HidePanel();
        }
	}

    private void RefreshDisplay(CharacterInventory inv)
    {
        RemoveButtons();
        if (inv == null) return;
        AddButtons(inv);
    }

    void AddButtons(CharacterInventory inv)
    {
        for(int i=0;i<inv.items.Count; i++)
        {
            GameObject newButton = ObjectPooler.Instance.SpawnFromPool("SampleButton", Vector3.zero, Quaternion.identity);
            newButton.transform.SetParent(contentPanel);
            newButton.transform.localScale = Vector3.one;
            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(inv.items[i], this);
        }
    }

    void RemoveButtons()
    {
        while(contentPanel.childCount>0)
        {
            GameObject toRemove = contentPanel.GetChild(0).gameObject;
            toRemove.transform.SetParent(null);
            toRemove.SetActive(false);
        }
    }

    public void ShowPanel(CharacterInventory cont)
    {
        Container = cont;
        itemListUI.SetActive(true);
    }

    public void ShowPanel(Container cont)
    {
        Container = cont.GetComponent<CharacterInventory>();
        containerLabel.SetText(cont.details.name);
        RefreshDisplay(Container);
        itemListUI.SetActive(true);
    }

    public void HidePanel()
    {
        Container = null;
        itemListUI.SetActive(false);
    }

}
