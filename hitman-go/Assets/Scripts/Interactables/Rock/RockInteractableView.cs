using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableSystem
{
    public class RockInteractableView : InteractableView
    {
        private RockInteractableController rockInteractableController;

        public override void SetController(InteractableController interactableController)
        {
            this.rockInteractableController = (RockInteractableController)interactableController;
        }

        void Throw()
        {
            rockInteractableController.Throw();
        }
    }
}