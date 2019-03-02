using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public class DualGunInteractableController : InteractableController
    {
        private InteractableManager interactableManager;

        public DualGunInteractableController(Vector3 nodePos, InteractableManager interactableManager
                                            , InteractableView dualGunPrefab)
        {
            this.interactableManager = interactableManager;
            GameObject dualGun = GameObject.Instantiate<GameObject>(dualGunPrefab.gameObject);
            interactableView = dualGun.GetComponent<DualGunInteractableView>();
            interactableView.transform.position = nodePos;
        }

        protected override void OnInitialized()
        {
            interactablePickup = InteractablePickup.DUAL_GUN;
        }

        public override void TakeAction(int nodeID)
        {
            Shoot(nodeID);
        }

        void Shoot(int targetNodeID)
        {

        }
    }
}