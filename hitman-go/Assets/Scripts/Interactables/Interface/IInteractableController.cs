using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public interface IInteractableController
    {
        InteractablePickup GetInteractablePickup();
        bool CanTakeAction(int playerNode, int nodeID);
        void TakeAction(int nodeID);
    }
}