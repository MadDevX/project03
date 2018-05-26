using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;
    public GameObject inventoryUI;
    public Image buttonImage;
    public Color hideColor;
    public Color activeColor;
    private PlayerInventory inventory;

    InventorySlot[] slots;

    // Use this for initialization
    void Start ()
    {
        inventory = PlayerInventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Inventory"))
        {
            TurnInventory();
        }
	}

    void UpdateUI()
    {
        for(int i = 0; i< slots.Length;i++)
        {
            if(i<inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        DescriptionPanel.Instance.HidePanel();
    }

    public void TurnInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        if (inventoryUI.activeSelf)
        {
            buttonImage.color = hideColor;
        }
        else
        {
            buttonImage.color = activeColor;
        }
        DescriptionPanel.Instance.HidePanel();
    }
}
