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
    public void Initialize(Sprite _icon, string _text, bool _wearable, int _basePrice)
    {
        item_Icon = _icon;
        item_Text = _text;
        isWearable = _wearable;
        basePrice = _basePrice;
    }
    // public Unit_Inventory ReturnParentOfSubasset()
    // {
    //     string tempPath = AssetDatabase.GetAssetPath(this);

    //     return (Unit_Inventory)AssetDatabase.LoadAssetAtPath(tempPath,typeof(Unit_Inventory)); 
    // }
    public void SellItem()
    {
        //Bad Practise I know!
        Player cache = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(cache.interactingWithUnit != null)
        {
            Unit_Inventory.TradeItem(this,cache.self_UnitInventory,cache.interactingWithUnit.self_UnitInventory);
            cache.self_UnitInventory.currencyAmount += (int)(basePrice / cache.self_UnitInventory.buySellFactor);
            cache.interactingWithUnit.self_UnitInventory.currencyAmount -= (int)(basePrice / cache.interactingWithUnit.self_UnitInventory.buySellFactor);
            cache.interactingWithUnit.self_NPCUI.isUiRefreshNeeded = true;
            cache.self_PlayerUI.isUiRefreshNeeded = true;
            Debug.Log("Selling!");
        }
    }
    public void BuyItem()
    {
        //Bad Practise I know!
        Player cache = GameObject.FindWithTag("Player").GetComponent<Player>();
        if(cache.interactingWithUnit != null)
        {
            Unit_Inventory.TradeItem(this,cache.interactingWithUnit.self_UnitInventory,cache.self_UnitInventory);
            cache.self_UnitInventory.currencyAmount -= (int)(basePrice * cache.self_UnitInventory.buySellFactor);
            cache.interactingWithUnit.self_UnitInventory.currencyAmount += (int)(basePrice * cache.interactingWithUnit.self_UnitInventory.buySellFactor);
            cache.interactingWithUnit.self_NPCUI.isUiRefreshNeeded = true;
            cache.self_PlayerUI.isUiRefreshNeeded = true;
            Debug.Log("Buying!");
        }
    }
}
