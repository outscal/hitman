using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableSystem
{
    public class RockInteractableController : InteractableController
    {
        private RockInteractableView rockInteractableView;

        public RockInteractableController(Vector3 nodePos)
        {
            rockInteractableView = new RockInteractableView();
            rockInteractableView.transform.position = nodePos;
        }

        public override void TakeAction()
        {
            base.TakeAction();
            RockAction();
        }

        void RockAction()
        {
             
        }
    }
}
