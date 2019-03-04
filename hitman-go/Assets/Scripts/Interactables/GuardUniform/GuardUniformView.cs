using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableSystem
{
    public class GuardUniformView : InteractableView
    {
        private GuardUniformController guardUniformController;

        public void SetController(GuardUniformController guardUniformController)
        {
            this.guardUniformController = guardUniformController;
        }
    }
}