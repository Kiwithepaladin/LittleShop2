using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void StayInteract(RaycastHit2D target);
    void EnterInteract(RaycastHit2D target);
    void ExitInteract();
}
