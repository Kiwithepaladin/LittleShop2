using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Units
{
    public class Shopkeeper : Unit
    {
        public new void OnCollisionEnter2D(Collision2D other) 
        {
            base.OnCollisionEnter2D(other);
            IsInteracting = true;
        }
        public virtual void OnCollisionExit2D(Collision2D other)
        {
           base.OnCollisionExit2D(other);
           IsInteracting = false;
        }
    }
}
