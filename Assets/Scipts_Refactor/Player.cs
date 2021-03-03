using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Units
{
    public class Player : Unit
    {

        Rigidbody2D rb;
        [SerializeField] Vector2 moveInput;
        PlayerInteractions playerI;


        public new void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerI = new PlayerInteractions();
            base.Awake();
        }

        public void Update()
        {
            moveInput = playerI.Player.Move.ReadValue<Vector2>();
            Debug.Log(moveInput);
            base.Update();
        }

        public void FixedUpdate()
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
    }
}
