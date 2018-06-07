using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SampleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Button button;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI priceLabel;
    public Image icon;

    [SerializeField] private ItemDetails item;
    private ItemsScrollList scrollList;

    public void Setup(ItemDetails currentItem, ItemsScrollList currentScrollList)
    {
        item = currentItem;
        nameLabel.SetText(item.name);
        priceLabel.SetText("Price: " + item.price);
        icon.sprite = item.icon;

        scrollList = currentScrollList;

    }

    public void LootItem()
    {
        CharacterInventory inv = PlayerManager.playerInstance.GetComponent<CharacterInventory>();
        if (inv == null) return;
        if(scrollList.Container==inv)
        {
            item.Use(PlayerManager.playerInstance);
            return;
        }
        if (inv.Add(item))
        {
            scrollList.Container.Remove(item);
            if (scrollList.Container.items.Count == 0)
            {
                scrollList.HidePanel();
            }
            DescriptionPanel.Instance.HidePanel();
        }
    }


    public void ShowDescription()
    {
        DescriptionPanel.Instance.SetPanel(item.ToString());
    }

    public void HideDescription()
    {
        DescriptionPanel.Instance.HidePanel();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideDescription();
    }
}
