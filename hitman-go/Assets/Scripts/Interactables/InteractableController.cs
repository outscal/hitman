using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace InteractableSystem
{
    public class InteractableController : IInteractableController
    {
        protected InteractableView interactableView;
        protected InteractablePickup interactablePickup;

        public InteractableController()
        {
            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
            interactablePickup = InteractablePickup.NONE;
        }

        public virtual void TakeAction(int nodeID) 
        {

        }

        public InteractablePickup GetInteractablePickup()
        {
            return interactablePickup;
        }

        public virtual void InteractablePickedUp()
        {
            interactableView.gameObject.SetActive(false); 
        }

        public void Destroy()
        {
            GameObject.Destroy(interactableView.gameObject); 
        }
    }
}