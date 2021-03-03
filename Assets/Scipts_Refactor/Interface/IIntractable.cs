using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Units.Interfaces
{
    public interface IIntractable 
    {
        bool IsInteractable {get; set;}
        bool IsInteracting {get; set;}
        bool DidEnterInteract {get; set;}
        void StayInteract(RaycastHit2D target);
        void EnterInteract(RaycastHit2D target);
        void ExitInteract();
    }
}
