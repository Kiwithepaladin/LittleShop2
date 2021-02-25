using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class Unit : MonoBehaviour
{
    [SerializeField] public AssetReference self_InventoryReference;
    [Header("Unit proprties")]
    public Unit_Inventory self_UnitInventory;
    public bool self_isInteractable = true;
    public bool self_isInteracting = false;

    public virtual void Awake()
    {
      if(self_UnitInventory != null)
      {
        self_UnitInventory.InitializeInventory();
      }
      foreach (Unit_Item item in self_UnitInventory.inventory)
      {
          self_InventoryReference.SetEditorAsset(item);
      }
    }
    private void LateUpdate() 
    {
      //self_UnitInventory.EqualLists();
    }
    public virtual void UpdateListOfItemUI(){}
    private void OnDisable() 
    {
      #if UNITY_EDITOR
      self_UnitInventory.StractureIntoSubAssets();
      AssetDatabase.SaveAssets();
      #endif
    }

}
