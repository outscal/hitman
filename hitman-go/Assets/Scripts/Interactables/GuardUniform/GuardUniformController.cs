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

        private EnemyType currentEnemyType;

        public GuardUniformController(Vector3 nodePos, InteractableManager interactableManager
                                            , InteractableView guardUniformPrefab, EnemyType enemyType)
        {
            this.currentEnemyType = enemyType;
            this.interactableManager = interactableManager;
            GameObject guardUniform = GameObject.Instantiate<GameObject>(guardUniformPrefab.gameObject);
            interactableView = guardUniform.GetComponent<GuardUniformView>();
            interactableView.SetController(this);
            interactableView.transform.position = nodePos;
        }

        protected override void OnInitialized()
        {
            interactablePickup = InteractablePickup.GUARD_DISGUISE;
        }

        public override bool CanTakeAction(int playerNode, int nodeID)
        {
            return true;
        }

        public override void TakeAction(int nodeID)
        {
            interactableManager.ReturnSignalBus().TryFire(new DisguiseSignal() 
            { enemyType = currentEnemyType });
        }

        public override void InteractablePickedUp()
        {
            interactableManager.ReturnSignalBus().TryFire(new SignalPlayOneShot()
            { soundName = SoundName.guardUniformFX });
            base.InteractablePickedUp();
        }

        public EnemyType GetEnemyType()
        {
            return currentEnemyType;
        }
    }
}