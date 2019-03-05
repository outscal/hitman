using UnityEngine;
using Common;
using SoundSystem;
using Enemy;

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

        public override void TakeAction(int targetNodeID)
        {
            interactableManager.ReturnSignalBus().TryFire(new SignalPlayOneShot()
            { soundName = SoundName.sniper });
            interactableManager.ReturnSignalBus().TryFire(new EnemyKillSignal() { nodeID = targetNodeID });
            interactableManager.RemoveInteractable(this);
        }

        public override void InteractablePickedUp()
        {
            interactableManager.ReturnSignalBus().TryFire(new SignalPlayOneShot()
            { soundName = SoundName.pickUpGun });
            base.InteractablePickedUp();
        }
    }
}