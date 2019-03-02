using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableSystem
{
    public class BoneView : InteractableView
    {
        private BoneController boneInteractableController;

        public override void SetController(InteractableController interactableController)
        {
            this.boneInteractableController = (BoneController)interactableController;
        }

        void Throw()
        {
            boneInteractableController.Throw();
        }
    }
}