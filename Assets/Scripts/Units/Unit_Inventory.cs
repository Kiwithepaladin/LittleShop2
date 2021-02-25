using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Empty_Inventory", menuName = "ScriptableObjects/Empty_Inventory", order = 1)]
public class Unit_Inventory : ScriptableObject
{
    [SerializeField] AssetReference self_Ref;
    [SerializeField] private int inventoryMaxSize = 6;
    private int inventoryCurrentSize;
    public List<Unit_Item> inventoryStracture = new List<Unit_Item>();
    public List<Unit_Item> inventory = new List<Unit_Item>();
    public string inventory_Path;
    public int currencyAmount = 100;
    public float buySellFactor;
    public void InitializeInventory()
    {
        #if UNITY_EDITOR
        inventory_Path = AssetDatabase.GetAssetPath(this);
        AssetDatabase.ImportAsset(inventory_Path);
        #endif
    }
    public void EqualLists()
    {
        //bool equals = inventoryStracture.Count >= inventory.Count;
        // for (int i = inventory.Count - 1; i >= 0; i--)
        // {
        //     if(!equals)
        //     {   
        //         CreateNewItemInInventoryStracture(i);
        //         equals = inventoryStracture.Count >= inventory.Count;
        //     }
        //     else {break;}
        // }
        bool equals = inventoryStracture.Count <= inventory.Count;
        for (int i = inventoryStracture.Count - 1; i >= 0; i--)
        {
            if(!equals)
            {
                CreateNewItemInInventory(i);
                equals = inventoryStracture.Count <= inventory.Count;
            }
            else {break;}
        }
    }
    public void RemoveItemFromInventories(Unit_Item item)
    {
        if(inventory.Contains(item))
        {
            int itemPointer = inventoryStracture.FindIndex(i => i.name == item.name);
            inventoryStracture.RemoveAt(itemPointer);
            #if UNITY_EDITOR
            AssetDatabase.RemoveObjectFromAsset(item);
            #endif
            inventory.Remove(item);
        }
    }
    private void CreateNewItemInInventoryStracture(int i)
    {
        Unit_Item tempItem = (Unit_Item)Resources.Load("Items/" + inventory[i].name);
        inventoryStracture.Add(tempItem);
    }
    private void CreateNewItemInInventory(int i)
    {
        Unit_Item newItem = ScriptableObject.CreateInstance<Unit_Item>();
        newItem.name = inventoryStracture[i].name;
        newItem.Initialize(inventoryStracture[i].item_Icon, inventoryStracture[i].item_Text, inventoryStracture[i].isWearable,inventoryStracture[i].basePrice);
        // #if UNITY_EDITOR
        // AssetDatabase.AddObjectToAsset(newItem, inventory_Path);
        // #endif
        inventory.Add(newItem);
    }
    public void StractureIntoSubAssets()
    {
        bool equals = inventoryStracture.Count != inventory.Count;
        foreach (Unit_Item item in inventoryStracture)
        {
            if(equals)
            {
                Unit_Item newItem = ScriptableObject.CreateInstance<Unit_Item>();
                newItem.name = item.name;
                newItem.Initialize(item.item_Icon,item.item_Text,item.isWearable,item.basePrice);
                #if UNITY_EDITOR
                AssetDatabase.AddObjectToAsset(newItem,inventory_Path);
                #endif
                inventory.Add(newItem);
                equals = inventoryStracture.Count != inventory.Count;      
            }
        }
    }
    public void RemoveAllSubAssetsFromInventory()
    {
        #if UNITY_EDITOR
        inventory.Clear();
        //Object[] allsubAssetsResrouces = Resources.LoadAll(inventory_Path,typeof(Unit_Item));
        Object[] allSubAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(inventory_Path);

        foreach(Unit_Item subAsset in allSubAssets)
        {
            if(subAsset != null && AssetDatabase.IsSubAsset(subAsset))
            {
                AssetDatabase.RemoveObjectFromAsset(subAsset);
            }
        }
        inventory.TrimExcess();
        #endif
    }
    public void LoadAllItems(Transform parent)
    {   
        //Object[] allSubAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(this.inventory_Path);
        Object[] allsubAssetsResrouces = Resources.LoadAll(inventory_Path,typeof(Unit_Item));
        var AddressableSubAssets = self_Ref.SubObjectName;
        foreach(Unit_Item item in allsubAssetsResrouces)
        {
            if(item != null && inventoryCurrentSize <= inventoryMaxSize)
            {
                Unit_ItemUI instance = GameObject.Instantiate(Resources.Load("Item") as GameObject,parent.position,Quaternion.identity,parent.transform).GetComponent<Unit_ItemUI>();
                instance.self_UnitItem = item;
                instance.Initialize();
                inventoryCurrentSize++;
            }  
        }
    }
    public static void TradeItem(Unit_Item item, Unit_Inventory removeFromInv,Unit_Inventory addToInv)
    {
        Unit_Item tempItem = (Unit_Item)Resources.Load("Items/" + item.name);
        tempItem.name = item.name;
        tempItem.Initialize(item.item_Icon,item.item_Text,item.isWearable,item.basePrice);
        removeFromInv.RemoveItemFromInventories(item);
        removeFromInv.EqualLists();
        
        addToInv.inventoryStracture.Add(tempItem);
        addToInv.EqualLists();
        #if UNITY_EDITOR
        AssetDatabase.SaveAssets();
        #endif
        
    }
    public void DestoryAllLoadedItems(Transform parent)
    {
        foreach (Transform child in parent) 
        {
            GameObject.Destroy(child.gameObject);
        }
        inventoryCurrentSize = 0;
    }
    
}
