using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Unit_InventoryReader : MonoBehaviour
{
    [SerializeField] private Unit_Inventory originalInventory;
    [SerializeField] private int inventoryMaxSize = 6;
    private int inventoryCurrentSize = 0;
    public List<Unit_Item> inventoryStracture = new List<Unit_Item>();
    public List<Unit_Item> inventory = new List<Unit_Item>();
    public string inventory_Path;
    public int currencyAmount;
    public float buySellFactor;
    public AssetReference self_Ref;

    private void Awake() 
    {
        Unit_Inventory.CloneInventory(originalInventory,out inventoryStracture,out inventory,out inventory_Path,out currencyAmount,out buySellFactor,out self_Ref);
    }
    public void ResetAllSubAssets()
    {
      foreach (Unit_Item i in inventory)
      {
        if(!string.IsNullOrEmpty(i.name))
        {
            i.ResetItem();
        }
      }
    }
    public void LoadAllItems(Transform parent)
    {
        foreach(Unit_Item item in inventory)
        {
            if(!string.IsNullOrEmpty(item.name) && inventoryCurrentSize <= inventoryMaxSize)
            {
                Unit_ItemUI instance = Instantiate(Resources.Load("Item") as GameObject,parent.position,Quaternion.identity,parent.transform).GetComponent<Unit_ItemUI>();
                instance.Initialize(item);
                inventoryCurrentSize++;
            }  
        }
        return;
    }
    public void DestoryAllLoadedItems(Transform parent)
    {
        foreach (Transform child in parent) 
        {
            GameObject.Destroy(child.gameObject);
        }
        inventoryCurrentSize = 0;
    }
    public static void TradeItem(Unit_Item item, Unit_InventoryReader removeFromInv,Unit_InventoryReader addToInv)
    {
        Unit_Item tempItem = (Unit_Item)Resources.Load("Items/" + item.name);
        tempItem.name = item.name;
        tempItem.Initialize(item.item_Icon,item.item_Text,item.isWearable,item.basePrice);
        int invIndex = removeFromInv.inventory.FindIndex(i => i.name == item.name);
        int strIndex = removeFromInv.inventoryStracture.FindIndex(i => i.name == item.name);
        removeFromInv.inventory[invIndex].ResetItem();
        removeFromInv.inventoryStracture.RemoveAt(strIndex);

        // foreach (Unit_Item i  in addToInv.inventory)
        // {
        //     if(string.IsNullOrWhiteSpace(i.name))
        //     {
        //         i.Initialize(tempItem.item_Icon,tempItem.item_Text,tempItem.isWearable,tempItem.basePrice);
        //         break;
        //     }
        // }
        addToInv.inventory.Add(tempItem);
        addToInv.inventoryStracture.Add((Unit_Item)Resources.Load("Items/" + tempItem.name));
        #if UNITY_EDITOR
        AssetDatabase.SaveAssets();
        #endif
        
    }
}
