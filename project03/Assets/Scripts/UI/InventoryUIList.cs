using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIList : MonoBehaviour
{
    public GameObject inventoryUI;
    public ItemsScrollList itemList;
    public Image buttonImage;
    public Animator buttonAnimator;
    public Color hideColor;
    public Color activeColor;

    private void Start()
    {
        CharacterInventory pInv = PlayerManager.playerInstance.GetComponent<CharacterInventory>();
        pInv.onItemChangedCallback += ButtonAnimation;
        itemList.Container = pInv;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            TurnInventory();
        }
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

    void ButtonAnimation(CharacterInventory inv)
    {
        buttonAnimator.SetTrigger("WiggleTrigger");
    }
}
