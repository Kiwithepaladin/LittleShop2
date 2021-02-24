using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class Unit_ItemUI : MonoBehaviour
{
    public Unit_Item self_UnitItem;
    [SerializeField] private Image self_Icon;
    [SerializeField] private TMP_Text self_Text;
    [SerializeField] private Button tradingButton;
    public void Initialize()
    {
        self_Icon.sprite = self_UnitItem.item_Icon;
        self_Text.text = self_UnitItem.item_Text;
        Unit_Inventory cacheInventory = GameObject.FindWithTag("Player").GetComponent<Player>().self_UnitInventory;
        Unit_Inventory tempMainAsset;
        if(transform.root.GetComponentInChildren<NPC>() == null)
        {
            tempMainAsset = transform.root.GetComponentInChildren<Unit>().self_UnitInventory;
            if(cacheInventory == tempMainAsset)
            {
                tradingButton.onClick.AddListener(self_UnitItem.SellItem);
                return;
            }
        }
        else
        {
            tradingButton.onClick.AddListener(self_UnitItem.BuyItem);
        }
    }
    public void GreyOutItem()
    {
        tradingButton.interactable = false;
    }
    public void LitOutItem()
    {
        tradingButton.interactable = true;
    }
}
