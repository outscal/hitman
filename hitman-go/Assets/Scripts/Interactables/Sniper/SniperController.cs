using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public class SniperController : InteractableController
    {
        private InteractableManager interactableManager;

        int targetNodeID;

        public SniperController(Vector3 nodePos, InteractableManager interactableManager, InteractableView sniperPrefab)
        {
            this.interactableManager = interactableManager;
            GameObject sniper = GameObject.Instantiate<GameObject>(sniperPrefab.gameObject);
            interactableView = sniper.GetComponent<SniperView>();
            interactableView.SetController(this);
            interactableView.transform.position = nodePos;
        }

        protected override void OnInitialized()
        {
            interactablePickup = InteractablePickup.SNIPER_GUN;
        }

        public override void TakeAction(int nodeID)
        {

        }
    }
}