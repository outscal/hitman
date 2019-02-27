using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using PathSystem;
using Zenject;
using Enemy;

namespace InteractableSystem
{
    public class InteractableManager : IInteractable
    {
        readonly SignalBus signalBus;
        private IPathService pathService;
        private List<InteractableController> interactableControllers;

        public InteractableManager(IPathService pathService, InteractableScriptableObj interactableScriptableObjList, SignalBus signalBus)
        {
            this.signalBus = signalBus;
            this.pathService = pathService;
            interactableControllers = new List<InteractableController>();
            SpawnPickups(interactableScriptableObjList);
        }

        void SpawnPickups(InteractableScriptableObj interactableScriptableObj)
        {
            for (int i = 0; i < interactableScriptableObj.interactableItems.Count; i++)
            {
                SpawnInteractables(interactableScriptableObj.interactableItems[i].interactablePickup);
            }
        }

        public void SpawnInteractables(InteractablePickup interactablePickup)
        {
            List<int> nodeID = new List<int>();

            switch (interactablePickup)
            {
                case InteractablePickup.BREIFCASE:
                    break;
                case InteractablePickup.STONE:
                    nodeID.Clear();

                    nodeID = pathService.GetPickupSpawnLocation(InteractablePickup.STONE);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        Vector3 position = pathService.GetNodeLocation(nodeID[i]);
                        InteractableController rockController = new RockInteractableController(position, this);
                        interactableControllers.Add(rockController);
                    }

                    break;
                case InteractablePickup.BONE:
                    break;
                case InteractablePickup.SNIPER_GUN:
                    break;
                case InteractablePickup.DUAL_GUN:
                    break;
                case InteractablePickup.TRAP_DOOR:
                    break;
                case InteractablePickup.COLOR_KEY:
                    break;
                case InteractablePickup.AMBUSH_PLANT:
                    break;
                case InteractablePickup.GUARD_DISGUISE:
                    break;
                default:
                    break;
            }
        }

        public void SendEnemyAlertSignal(int nodeID, InteractablePickup interactablePickup)
        {
            signalBus.TryFire(new SignalAlertGuards() { nodeID = nodeID , 
            interactablePickup = interactablePickup});
        }


    }
}