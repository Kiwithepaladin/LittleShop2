using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "Empty_Item", menuName = "ScriptableObjects/Unit_Item", order = 1)]
public class Unit_Item : ScriptableObject
{
    public Sprite item_Icon;
    public string item_Text;
    public int basePrice;
    public bool isWearable;
    public void Initialize(Sprite _icon = null, string _text = null, bool _wearable = false, int _basePrice = 0)
    {
        this.name = _text;
        item_Icon = _icon;
        item_Text = _text;
        isWearable = _wearable;
        basePrice = _basePrice;
    }
    public void SellItem()
    {
        //Horribly Written function!
        Player cache = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(cache.interactingWithUnit != null)
        {
            cache.self_UnitInventory.currencyAmount += (int)(basePrice / cache.self_UnitInventory.buySellFactor);
            cache.interactingWithUnit.self_UnitInventory.currencyAmount -= (int)(basePrice / cache.interactingWithUnit.self_UnitInventory.buySellFactor);
            Unit_InventoryReader.TradeItem(this,cache.self_UnitInventory,cache.interactingWithUnit.self_UnitInventory);
            cache.interactingWithUnit.self_NPCUI.isUiRefreshNeeded = true;
            cache.self_PlayerUI.isUiRefreshNeeded = true;
            Debug.Log("Selling!");
            return;
        }
    }
    public void BuyItem()
    {
        //Bad Practise I know!
        Player cache = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(cache.interactingWithUnit != null)
        {
            cache.self_UnitInventory.currencyAmount -=  (int)(basePrice * cache.self_UnitInventory.buySellFactor);
            cache.interactingWithUnit.self_UnitInventory.currencyAmount += (int)(basePrice * cache.interactingWithUnit.self_UnitInventory.buySellFactor);
            Unit_InventoryReader.TradeItem(this,cache.interactingWithUnit.self_UnitInventory,cache.self_UnitInventory);
            cache.interactingWithUnit.self_NPCUI.isUiRefreshNeeded = true;
            cache.self_PlayerUI.isUiRefreshNeeded = true;
            Debug.Log("Buying!");
            return;
        }
    }
    public void ResetItem()
    {
        this.name = null;
        item_Icon = null;
        item_Text = null;
        isWearable = false;
        basePrice = 0;
    }
}
