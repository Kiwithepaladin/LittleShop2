using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Unit,IInteractable
{
    Rigidbody2D rb;
    Vector2 moveInput;
    PlayerInteractions playerI;
    public NPC interactingWithUnit;
    [Header("Player UI Elements")]
    public Player_UI self_PlayerUI;
    
    public new void Awake() 
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        playerI = new PlayerInteractions();
        self_PlayerUI.myInvetnroy = self_UnitInventory;
    }
    void Update()
    {
        moveInput = playerI.Player.Move.ReadValue<Vector2>();
        RaycastHit2D[] hitColliders = Physics2D.BoxCastAll(this.transform.position, new Vector2(1.5f,1.5f),0f,Vector2.zero);
        if (hitColliders.Length <= 1 || !self_PlayerUI.UnitsInventoryPanel.gameObject.activeInHierarchy)
        {
            ExitInteract();
            self_PlayerUI.DisableUnitInventory();
            self_UnitInventory.DestoryAllLoadedItems(self_PlayerUI.parentToSpawnAt);
        }
        else if(hitColliders.Length > 1)
        {
            StayInteract(hitColliders[1]);
        }
        if(self_PlayerUI.isUiRefreshNeeded)
        {
            self_PlayerUI.RefreshUI();
        }
        if(self_PlayerUI.self_listItemUi != null)
        {
            self_PlayerUI.ChangeItemUIInteractable();
        }

    }
    public override void UpdateListOfItemUI() 
    {
        self_PlayerUI.self_listItemUi.Clear();
        foreach (Transform child in self_PlayerUI.parentToSpawnAt)
        {
            self_PlayerUI.self_listItemUi.Add(child.GetComponent<Unit_ItemUI>());
        }
    }
    public void Interact()
    {
        RaycastHit2D[] hitColliders = Physics2D.BoxCastAll(this.transform.position, new Vector2(1.5f,1.5f),0f,Vector2.zero);
        if(hitColliders.Length > 1 && !self_isInteracting)
        {
            foreach(RaycastHit2D collision in hitColliders)
            {
                if(collision.collider != null)
                {
                    if(collision.collider.CompareTag("ShopKeepers"))
                    {
                        self_isInteracting = true;
                        interactingWithUnit = collision.collider.GetComponent<NPC>();
                        //interactingWithUnit.self_NPCUI.self_NPCUI
                    }
                }
            }
        }
        else if(hitColliders.Length <= 1)
        {
            ExitInteract();
        }
        if(self_isInteracting)
        {
            //PlaceHolder Variable
            EnterInteract(hitColliders[0]);
        }
    }
    void FixedUpdate()
    {
        rb.velocity = moveInput * 5f;
    }
    private void OnEnable() 
    {
        playerI.Enable();
    }
    private void OnDisable()
    {
        playerI.Disable();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position,new Vector2(1.5f,1.5f));
    }

    public void StayInteract(RaycastHit2D target)
    {
        UpdateListOfItemUI();
    }

    public void EnterInteract(RaycastHit2D target)
    {
        self_PlayerUI.RefreshUI();
        self_PlayerUI.ToggleUnitsInventory();
    }

    public void ExitInteract()
    {
        self_isInteracting = false;
        self_PlayerUI.self_listItemUi.Clear();
        interactingWithUnit = null;
    }
}
