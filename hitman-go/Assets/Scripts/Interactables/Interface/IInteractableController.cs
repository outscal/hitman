using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public interface IInteractableController
    {
        InteractablePickup GetInteractablePickup();
        void TakeAction(int nodeID);
    }
}