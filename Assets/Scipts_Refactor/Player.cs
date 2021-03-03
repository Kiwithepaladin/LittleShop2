using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Units.Interfaces;

namespace Inventory.Units
{
    public class Player : Unit
    {
        Rigidbody2D rb;
        Vector2 moveInput;
        PlayerInteractions playerI;

        [SerializeField] private IIntractable target;

        public new void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerI = new PlayerInteractions();
            base.Awake();
        }

        public new void Update()
        {
            base.Update();
            moveInput = playerI.Player.Move.ReadValue<Vector2>();
            Debug.Log(IsInteracting);
        }

        public void FixedUpdate()
        {
            rb.velocity = moveInput * 5f;
        }
        public new void OnCollisionExit2D(Collision2D other)
        {
            base.OnCollisionExit2D(other);
            IsInteracting = false;
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
                        target = collision.collider.GetComponent<IIntractable>();
                    }
                }
            }
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
