using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Enemy;

namespace InteractableSystem
{
    public class BoneController : InteractableController
    {
        private InteractableManager interactableManager;
        Hashtable hashtable = new Hashtable();
        int targetNodeID;
        public BoneController(Vector3 nodePos, InteractableManager interactableManager, InteractableView bonePrefab)
        {
            this.interactableManager = interactableManager;
            GameObject bone = GameObject.Instantiate<GameObject>(bonePrefab.gameObject);
            interactableView = bone.GetComponent<BoneView>();
            interactableView.SetController(this);
            interactableView.transform.position = nodePos;
        }

        protected override void OnInitialized()
        {
            interactablePickup = InteractablePickup.BONE;
            hashtable.Add("oncomplete", "Throw");
            hashtable.Add("time", 1f);
            //hashtable.Add("easetype", iTween.EaseType.easeOutCubic);
        }

        public override void TakeAction(int nodeID)
        {
            targetNodeID = nodeID;
            interactableView.gameObject.SetActive(true);
            Vector3 position = interactableManager.ReturnPathService()
                                .GetNodeLocation(nodeID);
            hashtable.Add("position", position);
            iTween.MoveTo(interactableView.gameObject, hashtable);
        }

        public void Throw()
        {
            interactableManager.ReturnSignalBus().TryFire(new NewDogDestinationSignal()
            {
                nodeID = targetNodeID
                //interactablePickup = interactablePickup
            });

            Debug.Log("[BoneController] Throw Task Done");

            interactableView.gameObject.SetActive(false);
            interactableManager.RemoveInteractable(this);
        }

        public override void InteractablePickedUp()
        {
            base.InteractablePickedUp();
        }
    }
}
