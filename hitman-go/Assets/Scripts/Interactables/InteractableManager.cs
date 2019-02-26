using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using PathSystem;

namespace InteractableSystem
{
    public class InteractableManager : IInteractable
    {
        private IPathService pathService;
        private List<InteractableController> interactableControllers;

        public InteractableManager(IPathService pathService)
        {
            this.pathService = pathService;
            interactableControllers = new List<InteractableController>();
        }

        public void SpawnInteractables(InteractablePickup pickups, int nodeID)
        {
            Vector3 nodePos = pathService.GetNodeLocation(nodeID);

            InteractablePickup pickup = pickups;
            switch (pickup)
            {
                case InteractablePickup.BREIFCASE:

                    break;
                case InteractablePickup.STONE:
                    interactableControllers.Add(new RockInteractableController(nodePos));
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
    }
}