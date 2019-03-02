using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Common;

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
            interactablePickup = InteractablePickup.STONE;
            hashtable.Add("oncomplete", "Throw");
            hashtable.Add("time", 1f);
            //hashtable.Add("easetype", iTween.EaseType.easeOutCubic);
        }

        public override void TakeAction(int nodeID)
        {
            targetNodeID = nodeID;
            interactableView.gameObject.SetActive(true);
            Vector3 position = interactableManager.GetNodeLocation(nodeID);
            hashtable.Add("position", position);
            iTween.MoveTo(interactableView.gameObject, hashtable);
        }

        public void Throw()
        {
            interactableManager.SendEnemyAlertSignal(targetNodeID, InteractablePickup.BONE);
            Debug.Log("[BoneController] Throw Task Done");

            interactableView.gameObject.SetActive(false);
            interactableManager.RemoveInteractable(this);
        }
    }
}
