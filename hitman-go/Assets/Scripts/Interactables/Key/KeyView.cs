using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public class KeyView : InteractableView
    {
        private KeyController keyController;
        [SerializeField]private Color red,blue,yellow;
        [SerializeField]private Renderer rendrer;

        public override void SetController(InteractableController interactableController)
        {
            this.keyController = (KeyController)interactableController;
            KeyTypes keyType = keyController.GetKeyType();

            if (keyType == KeyTypes.BLUE)
            {
                rendrer.material.color = blue;
            }
            else if (keyType == KeyTypes.RED)
            {
                rendrer.material.color = red;
            }
            else if (keyType == KeyTypes.YELLOW)
            {
                rendrer.material.color = yellow;
            }
        }

    }
}