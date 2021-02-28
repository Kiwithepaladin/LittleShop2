using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class Unit : MonoBehaviour
{
    [SerializeField] protected AssetReference self_InventoryReference;
    [Header("Unit proprties")]
    public Unit_InventoryReader self_UnitInventory;
    public bool self_isInteractable = true;
    public bool self_isInteracting = false;
    public virtual void Awake()
    {
      self_InventoryReference.LoadAssetAsync<IList<Unit_Item>>().Completed += InitList;
    } 
    public void InitList(AsyncOperationHandle<IList<Unit_Item>> items)
    {
      List<Unit_Item> tempList = new List<Unit_Item>();

      foreach (Unit_Item i in items.Result)
      {
        if(string.IsNullOrWhiteSpace(i.name))
        {
           int index = items.Result.IndexOf(i);
           if(self_UnitInventory.inventoryStracture.Count > index)
           {
              Debug.Log(items.Result.Count);
              Unit_Item itemStracture = self_UnitInventory.inventoryStracture[index];
              i.Initialize(itemStracture.item_Icon,itemStracture.item_Text,itemStracture.isWearable,itemStracture.basePrice);
              tempList.Add(i);
           }
        }
      }
      self_UnitInventory.inventory = tempList;
    }
    public virtual void UpdateListOfItemUI(){}
    public void OnDisable() 
    {
      self_InventoryReference.ReleaseAsset();
      self_UnitInventory.ResetAllSubAssets();
    }

}
