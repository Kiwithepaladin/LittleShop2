using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Unit, IInteractable
{
    [Header("NPC UI Elements")]
    public NPC_UI self_NPCUI; 
    public new void Awake() 
    {
        base.Awake();
        self_NPCUI.myInvetnroy = self_UnitInventory;
    }
    private void Update() 
    {
        if(self_NPCUI.isUiRefreshNeeded)
        {
            self_NPCUI.RefreshUI();
        }
    }
    public void FixedUpdate()
    {
         RaycastHit2D[] hitColliders = Physics2D.BoxCastAll(this.transform.position, new Vector2(1.5f,1.5f),0f,Vector2.zero);
         //IgnoresSelf
         if(self_isInteractable && hitColliders.Length > 1)
         {
             EnterInteract(hitColliders[1]);
             foreach(RaycastHit2D collision in hitColliders)
             {
                StayInteract(collision);
             }
         }
         if(hitColliders.Length <= 1 || !self_isInteractable)
         {
             ExitInteract();
         }
    }
    public void StayInteract(RaycastHit2D target)
    {
        if(target.collider.CompareTag("Player") && target.collider.GetComponent<Player>().self_isInteracting && !self_NPCUI.UnitsInventoryPanel.activeInHierarchy)
        {
            self_NPCUI.ToggleUnitsInventory();
            self_NPCUI.RefreshUI();
        }
        else if(target.collider.CompareTag("Player") && !target.collider.GetComponent<Player>().self_isInteracting && self_NPCUI.UnitsInventoryPanel.activeInHierarchy)
        {
            self_UnitInventory.DestoryAllLoadedItems(self_NPCUI.parentToSpawnAt);
            self_NPCUI.DisableUnitInventory();
        }
        if(target.collider.CompareTag("Player") && target.collider.GetComponent<Player>().self_isInteracting)
        {
            self_NPCUI.InteractText.gameObject.SetActive(false);
        }
    }
    public override void UpdateListOfItemUI()
    {
        self_NPCUI.self_listItemUi.Clear();
        foreach (Transform child in self_NPCUI.parentToSpawnAt)
        {
            self_NPCUI.self_listItemUi.Add(child.GetComponent<Unit_ItemUI>());
        }
    }
    public void EnterInteract(RaycastHit2D target)
    {
        UpdateListOfItemUI();
        self_isInteracting = true;
        self_NPCUI.InteractText.gameObject.SetActive(self_isInteracting);
    }
    public void ExitInteract()
    {
        self_isInteracting = false;
        self_NPCUI.self_listItemUi.Clear();
        self_NPCUI.InteractText.gameObject.SetActive(self_isInteracting);
        self_UnitInventory.DestoryAllLoadedItems(self_NPCUI.parentToSpawnAt);
        self_NPCUI.DisableUnitInventory();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position,new Vector2(1.5f,1.5f));
    }
    
}
