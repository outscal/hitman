using UnityEngine;
using Common;
using SoundSystem;
using Enemy;

namespace InteractableSystem
{
    public class DualGunInteractableController : InteractableController
    {
        private InteractableManager interactableManager;
        int enemyNodeID;

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
            Shoot(nodeID, Directions.UP);
            Shoot(nodeID, Directions.DOWN);
            Shoot(nodeID, Directions.LEFT);
            Shoot(nodeID, Directions.RIGHT);
        }

        public override void InteractablePickedUp()
        {
            base.InteractablePickedUp();
        }

        void Shoot(int targetNodeID, Directions direction)
        {
            Debug.Log("[DualGun] Direction:" + direction);
            Vector3 position = interactableManager.ReturnPathService()
                                .GetNodeLocation(targetNodeID);

            if (EnemyPresent(position, GetDiectionInVector3(direction)) == true)
            {
                enemyNodeID = interactableManager.ReturnPathService()
                                .GetNextNodeID(targetNodeID, direction);

                interactableManager.ReturnSignalBus().Fire(new EnemyDeathSignal()
                { nodeID = enemyNodeID });
            }

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

        Vector3 GetDiectionInVector3(Directions directions)
        {
            Vector3 direction = Vector3.zero;
            if (directions == Directions.UP) return direction = Vector3.up;
            if (directions == Directions.DOWN) return direction = Vector3.down;
            if (directions == Directions.LEFT) return direction = Vector3.left;
            if (directions == Directions.RIGHT) return direction = Vector3.right;
            return direction;
        }

    }
}