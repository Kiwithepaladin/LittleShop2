using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[System.Serializable]
public class Unit_InventoryUI
{  
    [HideInInspector] public Unit_Inventory myInvetnroy;
    [HideInInspector] public List<Unit_ItemUI> self_listItemUi = new List<Unit_ItemUI>();
    public GameObject UnitsInventoryPanel;
    public TMP_Text shopNameText;
    public TMP_Text currenyAmountText;
    public Transform parentToSpawnAt;
    [HideInInspector] public bool isUiRefreshNeeded = false;
    public void ToggleUnitsInventory()
    {
        SetShopNameText(myInvetnroy.name);
        UnitsInventoryPanel.SetActive(!UnitsInventoryPanel.activeInHierarchy);
    }
    public void DisableUnitInventory()
    {
        UnitsInventoryPanel.SetActive(false);
    }
    public void SetShopNameText(string shopName)
    {
        shopNameText.text = shopName;
    }
    public void SetCurrentAmountText(int amount)
    {
        currenyAmountText.text = "Money: " + amount.ToString();
    }

    public void ChangeItemUIInteractable()
    {
        Player cacheInventory = GameObject.FindWithTag("Player").GetComponent<Player>();
        foreach (Unit_ItemUI itemUI in self_listItemUi)
        {
            //Buy
            if(cacheInventory.interactingWithUnit.self_UnitInventory.inventory.Contains(itemUI.self_UnitItem) && 
            itemUI.self_UnitItem.basePrice * cacheInventory.interactingWithUnit.self_UnitInventory.buySellFactor < cacheInventory.self_UnitInventory.currencyAmount)
            {
                itemUI.LitOutItem();
                return;
            }
            //Sell
            if(cacheInventory.self_UnitInventory.inventory.Contains(itemUI.self_UnitItem) && 
            itemUI.self_UnitItem.basePrice / cacheInventory.interactingWithUnit.self_UnitInventory.buySellFactor < cacheInventory.interactingWithUnit.self_UnitInventory.currencyAmount)
            {
                itemUI.LitOutItem();
                return;
            }
            itemUI.GreyOutItem();
        }
    }
    public void RefreshUI()
    {
        SetCurrentAmountText(myInvetnroy.currencyAmount);
        myInvetnroy.DestoryAllLoadedItems(parentToSpawnAt);
        myInvetnroy.LoadAllItems(parentToSpawnAt);
        isUiRefreshNeeded = false;
    }
}
