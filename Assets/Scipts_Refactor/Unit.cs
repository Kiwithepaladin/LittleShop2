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
        public bool IsInteractable {get; set;} = true;
        public bool IsInteracting {get; set;} = false;

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
        public bool IsUIRefreshNeeded {get{return isUIRefreshNeeded;} set{isUIRefreshNeeded = value;}}


        public virtual void Awake()
        {

        }


        public virtual void Update()
        {
            if(IsUIRefreshNeeded)
            {
                RefreshUI();
            }
        }
        public virtual void OnCollisionEnter2D(Collision2D other) 
        {
            IsUIRefreshNeeded = true;
        }
        public virtual void OnCollisionStay2D(Collision2D other)
        {
            if(this.IsInteracting && other.collider.GetComponent<IIntractable>().IsInteractable)
            {
                ToggleUnitInventory(true);
            }
        }
        public virtual void OnCollisionExit2D(Collision2D other)
        {
            ToggleUnitInventory(false);
            IsUIRefreshNeeded = false;
        }
        public void RefreshUI()
        {
            //Place Holder variable!
            SetShopNameText(this.transform.name);
            IsUIRefreshNeeded = false;
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
