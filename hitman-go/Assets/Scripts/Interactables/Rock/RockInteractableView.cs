using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class RockInteractableView : InteractableView
    {
        private RockInteractableController rockInteractableController;

        public void SetController(RockInteractableController rockInteractableController)
        {
            this.rockInteractableController = rockInteractableController; 
        }
    }
}