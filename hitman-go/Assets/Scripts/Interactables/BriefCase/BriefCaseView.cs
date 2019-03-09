using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableSystem
{
    public class BriefCaseView : InteractableView
    {
        private BriefCaseController briefCaseController;

        public void SetController(BriefCaseController briefCaseController)
        {
            this.briefCaseController = briefCaseController;
        }
    }
}