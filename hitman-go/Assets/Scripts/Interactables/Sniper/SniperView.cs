using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableSystem
{
    public class SniperView : InteractableView
    {
        private SniperController sniperController;

        public override void SetController(InteractableController interactableController)
        {
            this.sniperController = (SniperController)interactableController;
        }

    }
}