using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{

    public Transform itemsParent;
    public GameObject inventoryUI;
    public Image buttonImage;
    public Animator buttonAnimator;
    public Color hideColor;
    public Color activeColor;
    private CharacterInventory inventory;

    InventorySlot[] slots;

    // Use this for initialization
    void Start ()
    {
        inventory = PlayerManager.playerInstance.GetComponent<CharacterInventory>();
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        //Updating without animations
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
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
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Inventory"))
        {
            TurnInventory();
        }
	}

    void UpdateUI(CharacterInventory inv)
    {
        for(int i = 0; i< slots.Length;i++)
        {
            if(i<inv.items.Count)
            {
                slots[i].AddItem(inv.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        DescriptionPanel.Instance.HidePanel();
        buttonAnimator.SetTrigger("WiggleTrigger");
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
