using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Units
{
    public class Player : Unit
    {

        Rigidbody2D rb;
        Vector2 moveInput;
        PlayerInteractions playerI;

        [SerializeField] private Unit target;

        public new void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerI = new PlayerInteractions();
            base.Awake();
        }

        public new void Update()
        {
            moveInput = playerI.Player.Move.ReadValue<Vector2>();
            base.Update();
            Debug.Log(IsInteracting);
        }

        public void FixedUpdate()
        {
            rb.velocity = moveInput * 5f;
        }

        public void Interact()
        {
            RaycastHit2D[] hitColliders = Physics2D.BoxCastAll(this.transform.position, new Vector2(1.5f,1.5f),0f,Vector2.zero);
            if(hitColliders.Length > 1 && !IsInteracting)
            {
                foreach(RaycastHit2D collision in hitColliders)
                {
                    if(collision.collider != null)
                    {
                        IsInteracting = true;
                        target = collision.collider.GetComponent<NPC>();
                    }
                }
            }
        }
        public new void ExitInteract()
        {
            base.ExitInteract();
            IsInteracting = false;
            target = null;
        }
        private void OnEnable() 
        {
            playerI.Enable();
        }
        private void OnDisable()
        {
            playerI.Disable();
        }
    }
}
