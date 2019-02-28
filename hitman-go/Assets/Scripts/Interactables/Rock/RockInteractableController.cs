using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Common;

namespace InteractableSystem
{
    public class RockInteractableController : InteractableController, IThrowableItem
    {
        private RockInteractableView rockInteractableView;
        private InteractableManager interactableManager;

        public RockInteractableController(Vector3 nodePos , InteractableManager interactableManager, InteractableView rockPrefab)
        {
            this.interactableManager = interactableManager;
            GameObject rock = GameObject.Instantiate<GameObject>(rockPrefab.gameObject);
            rockInteractableView = rock.GetComponent<RockInteractableView>();
            rockInteractableView.transform.position = nodePos;
        }

        public void ThrowAction(int targetNodeID)
        {
            Throw(targetNodeID);
        }

        async void Throw(int targetNodeID)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            interactableManager.SendEnemyAlertSignal(targetNodeID, InteractablePickup.STONE);
            rockInteractableView.gameObject.SetActive(false);
        }
    }
}
