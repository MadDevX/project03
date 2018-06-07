using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button useButton;
    public Button removeButton;
    ItemDetails item;

    private static CharacterInventory playerInv;

    private void Start()
    {
        if(playerInv==null)
        {
            playerInv = PlayerManager.playerInstance.GetComponent<CharacterInventory>();
        }
    }


    public void AddItem(ItemDetails newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        useButton.interactable = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        useButton.interactable = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        playerInv.Remove(item);
    }

    public void UseItem()
    {
        if(item!=null)
        {
            item.Use(PlayerManager.playerInstance);
        }
    }

    public void ShowDescription()
    {
        if (item == null) return;

        DescriptionPanel.Instance.SetPanel(item.ToString());
    }

    public void HideDescription()
    {
        DescriptionPanel.Instance.HidePanel();
    }
}
