using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Unit : MonoBehaviour
{
    [Header("Unit proprties")]
    public Unit_Inventory self_UnitInventory;
    public bool self_isInteractable = true;
    public bool self_isInteracting = false;
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
