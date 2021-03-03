using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory.Units.Interfaces
{
    public interface IIntractableUI
    {
        GameObject UnitsInventoryPanel {get; set;}
        TMP_Text ShopNameText {get; set;}
        TMP_Text CurrenyAmountText {get; set;}
        Transform ParentToSpawnAt {get; set;}
        bool IsUIRefreshNeeded {get; set;}

        void RefreshUI();
        void ToggleUnitInventory(bool value);
        void SetShopNameText(string shopName);
        void SetCurrentAmountText(int amount);
    }
}
