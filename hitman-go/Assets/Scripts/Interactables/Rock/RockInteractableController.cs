using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Common;
using Enemy;

namespace InteractableSystem
{
    public class RockInteractableController : InteractableController
    {
        private InteractableManager interactableManager;
        Hashtable hashtable = new Hashtable();
        int targetNodeID;
        public RockInteractableController(Vector3 nodePos , InteractableManager interactableManager, InteractableView rockPrefab)
        {
            this.interactableManager = interactableManager;
            GameObject rock = GameObject.Instantiate<GameObject>(rockPrefab.gameObject);
            interactableView = rock.GetComponent<RockInteractableView>();
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
            Vector3 position = interactableManager.ReturnPathService()
                                .GetNodeLocation(nodeID);
            hashtable.Add("position", position);
            iTween.MoveTo(interactableView.gameObject, hashtable);
        }

        public void Throw()
        {
            interactableManager.ReturnSignalBus().TryFire(new SignalAlertGuards()
            {
                nodeID = targetNodeID,
                interactablePickup = interactablePickup
            });
            Debug.Log("[RockController] Throw Task Done");

            interactableView.gameObject.SetActive(false);
            interactableManager.RemoveInteractable(this);
        }

        public override void InteractablePickedUp()
        {
            base.InteractablePickedUp();
        }
    }
}
