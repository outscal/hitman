using UnityEngine;
using Common;
using SoundSystem;
using Enemy;

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

        public override void InteractablePickedUp()
        {
            base.InteractablePickedUp();
        }

        void Shoot(int targetNodeID)
        {
            Vector3 position = interactableManager.ReturnPathService()
                                .GetNodeLocation(targetNodeID);

            //enemy forward
            if(EnemyPresent(position,Vector3.forward) == true)
            {
                int enemyNodeID = interactableManager.ReturnPathService()
                                .GetNextNodeID(targetNodeID, Directions.UP);
                interactableManager.ReturnSignalBus().Fire(new EnemyDeathSignal()
                { nodeID = enemyNodeID });
            }

            //enemy back
            if (EnemyPresent(position, Vector3.back) == true)
            {
                int enemyNodeID = interactableManager.ReturnPathService()
                                .GetNextNodeID(targetNodeID, Directions.DOWN);
                interactableManager.ReturnSignalBus().Fire(new EnemyDeathSignal()
                { nodeID = enemyNodeID });
            }

            //enemy left
            if (EnemyPresent(position, Vector3.left) == true)
            {
                int enemyNodeID = interactableManager.ReturnPathService()
                                .GetNextNodeID(targetNodeID, Directions.LEFT);
                interactableManager.ReturnSignalBus().Fire(new EnemyDeathSignal()
                { nodeID = enemyNodeID });
            }

            //enemy right
            if (EnemyPresent(position, Vector3.right) == true)
            {
                int enemyNodeID = interactableManager.ReturnPathService()
                                .GetNextNodeID(targetNodeID, Directions.RIGHT);
                interactableManager.ReturnSignalBus().Fire(new EnemyDeathSignal()
                { nodeID = enemyNodeID });
            }

            interactableManager.ReturnSignalBus().TryFire(new SignalPlayOneShot() 
            { soundName = SoundName.dualGun });

            interactableManager.RemoveInteractable(this);

        }

        bool EnemyPresent(Vector3 origin, Vector3 direction)
        {
            GameObject enemy = null;
            RaycastHit hit;
            if (Physics.Raycast(origin, direction, out hit, 6f))
            {
                enemy = hit.collider.gameObject;
            }

            if (enemy != null)
            {
                if (enemy.GetComponent<IEnemyView>() != null)
                {
                    return true;
                }
            }

            return false;
        }

    }
}