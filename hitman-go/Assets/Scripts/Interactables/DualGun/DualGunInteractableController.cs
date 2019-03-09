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

        public override bool CanTakeAction(int playerNode, int nodeID)
        {
            return true;
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
            interactableManager.ReturnSignalBus().TryFire(new SignalPlayOneShot()
            { soundName = SoundName.dualGun });

            base.InteractablePickedUp();
        }

        void Shoot(int targetNodeID, Directions direction)
        {
            Vector3 position = interactableManager.ReturnPathService()
                                .GetNodeLocation(targetNodeID);

            if (EnemyPresent(position, GetDiectionInVector3(direction)) == true)
            {
                enemyNodeID = interactableManager.ReturnPathService()
                                .GetNextNodeID(targetNodeID, direction);

                interactableManager.ReturnSignalBus().Fire(new EnemyKillSignal()
                { nodeID = enemyNodeID, killMode= KillMode.SHOOT});
            }
            else
            {
                enemyNodeID = interactableManager.ReturnPathService()
                                .GetNextNodeID(targetNodeID, direction);
            }

            interactableManager.RemoveInteractable(this);
        }

        bool EnemyPresent(Vector3 origin, Vector3 direction)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin, direction, out hit, 6f))
            {
                if(hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject.GetComponent<IEnemyView>() != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        Vector3 GetDiectionInVector3(Directions directions)
        {
            Vector3 direction = Vector3.zero;
            if (directions == Directions.UP)
                return direction = interactableView.transform.TransformDirection(Vector3.back);
            if (directions == Directions.DOWN)
                return direction = interactableView.transform.TransformDirection(Vector3.forward);
            if (directions == Directions.LEFT)
                return direction = interactableView.transform.TransformDirection(Vector3.right);
            if (directions == Directions.RIGHT)
                return direction = interactableView.transform.TransformDirection(Vector3.left);
            return direction;
        }

    }
}