using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Units.Interfaces;
using UnityEngine.UI;
using TMPro;

namespace Inventory.Units
{
    public abstract class Unit : MonoBehaviour, IIntractable, IIntractableUI
    {
        //General interaction
        public bool IsInteractable {get; set;}
        public bool IsInteracting {get; set;}
        public bool DidEnterInteract {get; set;}

        //UI STUFF
        [Header("UI")]
        [SerializeField] private GameObject unitInventoryPanel;
        public GameObject UnitsInventoryPanel {get{return unitInventoryPanel;} set{unitInventoryPanel = value;}}
        [SerializeField] private TMP_Text shopNameText;
        public TMP_Text ShopNameText {get{return shopNameText;} set{shopNameText = value;}}
        [SerializeField] private TMP_Text currenyAmountText;
        public TMP_Text CurrenyAmountText {get{return currenyAmountText;} set{currenyAmountText = value;}}
        [SerializeField] private Transform parentToSpawnAt;
        public Transform ParentToSpawnAt {get{return parentToSpawnAt;} set{parentToSpawnAt = value;}}
        private bool isUIRefreshNeeded;
        public bool IsUIRefreshNeeded {get{return isUIRefreshNeeded;} set{IsUIRefreshNeeded = value;}}


        public virtual void Awake()
        {

        }


        public virtual void Update()
        {
            RaycastHit2D[] hitColliders = Physics2D.BoxCastAll(this.transform.position, new Vector2(1.5f,1.5f),0f,Vector2.zero);
            if (hitColliders.Length == 0)
            {
                ExitInteract();
                Debug.Log("Exit!");
            }
            else if(hitColliders.Length > 1)
            {
                if(!DidEnterInteract)
                {
                    EnterInteract(hitColliders[1]);
                }
                else
                {
                    StayInteract(hitColliders[1]);
                }
            }
            if(IsUIRefreshNeeded)
            {
                RefreshUI();
            }
        }
        
        //General Interaction
        public virtual void StayInteract(RaycastHit2D target)
        {
            //Currently do nothing! put loop here
        }
        public virtual void EnterInteract(RaycastHit2D target)
        {
            DidEnterInteract = true;
        }
        public virtual void ExitInteract()
        {
            ToggleUnitInventory(false);
            IsUIRefreshNeeded = false;
            DidEnterInteract = false;
        }

        //General ui stuff
        public void RefreshUI()
        {
            //Place Holder variable!
            SetShopNameText(this.transform.name);
            IsUIRefreshNeeded = false;
            Debug.Log("UI BEEN REFRSHED!");
        }
        public void ToggleUnitInventory(bool value)
        {
            UnitsInventoryPanel.SetActive(value);
        }
        public void SetShopNameText(string shopName)
        {
            ShopNameText.text = shopName;
        }
        public void SetCurrentAmountText(int amount)
        {
            CurrenyAmountText.text = amount.ToString();
        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position,new Vector2(1.5f,1.5f));
        }
    }
}
