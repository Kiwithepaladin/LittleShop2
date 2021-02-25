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
      self_UnitInventory = (Unit_Inventory)self_InventoryReference.editorAsset;
      if(self_UnitInventory != null)
      {
        self_UnitInventory.InitializeInventory();
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
