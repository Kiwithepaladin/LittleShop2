using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Units.Interfaces
{
    public interface IIntractable 
    {
        bool IsInteractable {get; set;}
        bool IsInteracting {get; set;}
    }
}
