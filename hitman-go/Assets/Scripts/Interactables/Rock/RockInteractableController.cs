using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public class RockInteractableController : InteractableController
    {
        private InteractableManager interactableManager;

        public RockInteractableController(Vector3 nodePos , InteractableManager interactableManager, InteractableView rockPrefab)
        {
            this.interactableManager = interactableManager;
            GameObject rock = GameObject.Instantiate<GameObject>(rockPrefab.gameObject);
            interactableView = rock.GetComponent<RockInteractableView>();
            interactableView.transform.position = nodePos;
        }

        protected override void OnInitialized()
        {
            interactablePickup = InteractablePickup.STONE;
        }

        public override void TakeAction(int nodeID)
        {
            Throw(nodeID);
        }

        async void Throw(int targetNodeID)
        {
            //await Task.Delay(TimeSpan.FromSeconds(1));
            Vector3 position = interactableManager.GetNodeLocation(targetNodeID);
            interactableView.transform.position = position;
            interactableManager.SendEnemyAlertSignal(targetNodeID, InteractablePickup.STONE);
            await Task.Delay(TimeSpan.FromSeconds(0.25f));
            Debug.Log("[RockController] Throw Task Done");
            interactableView.gameObject.SetActive(false);
            interactableManager.RemoveInteractable(this);
        }
    }
}
