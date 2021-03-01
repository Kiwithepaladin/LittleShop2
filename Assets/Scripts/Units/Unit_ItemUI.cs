using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit_ItemUI : MonoBehaviour
{
    public Unit_Item self_UnitItem;
    [SerializeField] private Image self_Icon;
    [SerializeField] private TMP_Text self_Text;
    [SerializeField] private Button self_tradingButton;
    [SerializeField] private TMP_Text self_ItemPrice;
    
    public void Initialize(Unit_Item item)
    {
        Unit_InventoryReader cacheInventory = GameObject.FindWithTag("Player").GetComponent<Player>().self_UnitInventory;
        Unit_InventoryReader tempMainAsset;
        self_UnitItem = item;
        self_Icon.sprite = self_UnitItem.item_Icon;
        self_Text.text = self_UnitItem.item_Text;
        if(transform.root.GetComponentInChildren<NPC>() == null)
        {
            tempMainAsset = transform.root.GetComponentInChildren<Unit>().self_UnitInventory;
            if(cacheInventory == tempMainAsset)
            {
                self_ItemPrice.text = "+" + (item.basePrice / tempMainAsset.buySellFactor).ToString();
                self_tradingButton.onClick.AddListener(self_UnitItem.SellItem);
                return;
            }
        }
        else
        {
            self_ItemPrice.text = "-" + (item.basePrice * cacheInventory.buySellFactor).ToString();
            self_tradingButton.onClick.AddListener(self_UnitItem.BuyItem);
        }
    }
    public void GreyOutItem()
    {
        self_tradingButton.interactable = false;
        self_tradingButton.name = "TEST!!!!!!!!!!";
    }
    public void LitOutItem()
    {
        self_tradingButton.interactable = true;
    }
}
