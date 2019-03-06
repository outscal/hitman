using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using SoundSystem;

namespace InteractableSystem
{
    public class GuardUniformController : InteractableController
    {
        private InteractableManager interactableManager;
        int enemyNodeID;

        public GuardUniformController(Vector3 nodePos, InteractableManager interactableManager
                                            , InteractableView guardUniformPrefab)
        {
            this.interactableManager = interactableManager;
            GameObject guardUniform = GameObject.Instantiate<GameObject>(guardUniformPrefab.gameObject);
            interactableView = guardUniform.GetComponent<GuardUniformView>();
            interactableView.transform.position = nodePos;
        }

        protected override void OnInitialized()
        {
            interactablePickup = InteractablePickup.GUARD_DISGUISE;
        }

        public override void TakeAction(int nodeID)
        {

        }

        public override void InteractablePickedUp()
        {
            interactableManager.ReturnSignalBus().TryFire(new SignalPlayOneShot()
            { soundName = SoundName.guardUniformFX });
            base.InteractablePickedUp();
        }
    }
}