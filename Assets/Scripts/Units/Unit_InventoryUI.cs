using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using System.Threading.Tasks;

[System.Serializable]
public class Unit_InventoryUI
{
    [HideInInspector] public Unit_InventoryReader Invetnroy;
    public List<Unit_ItemUI> self_listItemUi = new List<Unit_ItemUI>();
    public GameObject UnitsInventoryPanel;
    [SerializeField] private TMP_Text shopNameText;
    [SerializeField] private TMP_Text currenyAmountText;
    public Transform parentToSpawnAt;
    [HideInInspector] public bool isUiRefreshNeeded = true;
    public void ToggleUnitsInventory()
    {
        SetShopNameText(Invetnroy.name);
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
        Debug.Log(self_listItemUi.Count + " SIZE!");
        foreach (Unit_ItemUI itemUI in self_listItemUi)
        {
            // //Buy
            if(cacheInventory.interactingWithUnit.self_UnitInventory.inventory.Contains(itemUI.self_UnitItem) && 
            itemUI.self_UnitItem.basePrice * cacheInventory.interactingWithUnit.self_UnitInventory.buySellFactor < cacheInventory.self_UnitInventory.currencyAmount)
            {
                itemUI.LitOutItem(); 
                continue;
            }
            //Sell
            if(cacheInventory.self_UnitInventory.inventory.Contains(itemUI.self_UnitItem) && 
            itemUI.self_UnitItem.basePrice / cacheInventory.interactingWithUnit.self_UnitInventory.buySellFactor < cacheInventory.interactingWithUnit.self_UnitInventory.currencyAmount)
            {
                itemUI.LitOutItem();
                continue;
            }
            itemUI.GreyOutItem();
        }
        return;
    }
    public void RefreshUI()
    {
        SetCurrentAmountText(Invetnroy.currencyAmount);
        Invetnroy.DestoryAllLoadedItems(parentToSpawnAt);
        Invetnroy.LoadAllItems(parentToSpawnAt);
        isUiRefreshNeeded = false;
    }
}
