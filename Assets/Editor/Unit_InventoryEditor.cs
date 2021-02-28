using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Unit_Inventory))]
public class Unit_InventoryEditor : Editor 
{
    private Unit_Inventory unit_Inv;
    private SerializedObject soUnit_Inv;

    private void  OnEnable() 
    {
        unit_Inv = (Unit_Inventory)target;
        soUnit_Inv = new SerializedObject(unit_Inv);
    }
    public override void OnInspectorGUI()
    {
        soUnit_Inv.Update();
        CreateEditor(unit_Inv);
        DrawDefaultInspector();
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Generate SubAssets"))
        {
            unit_Inv.EditorStractureIntoSubAssets();
        }
        if(GUILayout.Button("Clear All Subassets"))
        {
            unit_Inv.EditorRemoveAllSubAssetsFromInventory();
        }
        GUILayout.EndHorizontal();
        if(GUILayout.Button("Generate Empty Inventory"))
        {
            unit_Inv.EditorGenerateEmptyInventory();
        }

    }
}