using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Empty_Inventory", menuName = "ScriptableObjects/Empty_Inventory", order = 1)]
public class Unit_Inventory : ScriptableObject
{
    [SerializeField] private readonly int inventoryMaxSize = 6;
    private int inventoryCurrentSize;
    public List<Unit_Item> inventoryStracture = new List<Unit_Item>();
    public List<Unit_Item> inventory = new List<Unit_Item>();
    public string inventory_Path;
    public int currencyAmount = 100;
    public float buySellFactor;

    public static void CloneInventory(Unit_Inventory original,out List<Unit_Item> _inventoryStracture, out List<Unit_Item> _inventory,out string _path, out int _currencyAmount, out float _factor)
    {
        _inventoryStracture = original.inventoryStracture;
        _inventory = original.inventory;
        _path = original.inventory_Path;
        _currencyAmount = original.currencyAmount;
        _factor = original.buySellFactor;
    }
    public void EditorStractureIntoSubAssets()
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
    public void EditorRemoveAllSubAssetsFromInventory()
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
    public void EditorGenerateEmptyInventory()
    {
        inventory.Clear();
        inventory.TrimExcess();
        for (int i = 0; i < inventoryMaxSize; i++)
        {
            Unit_Item newItem = ScriptableObject.CreateInstance<Unit_Item>();
            newItem.Initialize();
            #if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(newItem, inventory_Path);
            #endif
            inventory.Add(newItem);
        }
    }
}
