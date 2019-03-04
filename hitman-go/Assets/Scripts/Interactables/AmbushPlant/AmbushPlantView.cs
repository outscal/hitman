using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableSystem
{
    public class AmbushPlantView : InteractableView
    {
        private AmbushPlantController ambushPlanController;

        public override void SetController(InteractableController interactableController)
        {
            this.ambushPlanController = (AmbushPlantController)interactableController;
        }

    }
}