using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public class DualGunInteractableView : InteractableView
    {
        private DualGunInteractableController dualGunInteractableController;

        public void SetController(DualGunInteractableController dualGunInteractableController)
        {
            this.dualGunInteractableController = dualGunInteractableController;
        }
    }
}