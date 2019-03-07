using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Common;
using Enemy;

namespace InteractableSystem
{
    public class KeyController:InteractableController
    {
        private InteractableManager interactableManager;
        private KeyTypes keyType;

        public KeyController(Vector3 nodePos , InteractableManager interactableManager, InteractableView keyPrefab, KeyTypes keyType)
        {
            this.interactableManager = interactableManager;
            this.keyType = keyType;
            GameObject key = GameObject.Instantiate<GameObject>(keyPrefab.gameObject);
            interactableView = key.GetComponent<KeyView>();
            interactableView.SetController(this);
            interactableView.transform.position = nodePos;
        }

        protected override void OnInitialized()
        {
            interactablePickup = InteractablePickup.COLOR_KEY;
        }

        public override void TakeAction(int nodeID)
        {
            interactableManager.ReturnPathService().KeyCollected(keyType);
        }     

        public override void InteractablePickedUp()
        {
            base.InteractablePickedUp();
        }

        public void SetKeyType(KeyTypes keyType)
        {
            this.keyType = keyType;
        }

        public KeyTypes GetKeyType()
        {
            return keyType;
        }
    }
}