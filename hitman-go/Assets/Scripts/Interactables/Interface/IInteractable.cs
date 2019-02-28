using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public interface IInteractable
    {
        bool CheckForInteractable(int nodeID);
        IInteractableController ReturnInteractableController(int nodeID);
    }
}