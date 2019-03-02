using System.Collections.Generic;
using Common;
using UnityEngine;

namespace InteractableSystem
{
    public class InteractableFactory
    {
        private InteractableScriptableObj interactableScriptableObj;
        private InteractableManager interactableManager;

        public InteractableFactory(InteractableManager interactableManager, InteractableScriptableObj interactableScriptableObj)
        {
            this.interactableManager = interactableManager;
            this.interactableScriptableObj = interactableScriptableObj;
        }

        public void SpawnPickups()
        {
            for (int i = 0; i < interactableScriptableObj.interactableItems.Count; i++)
            {
                SpawnInteractables(interactableScriptableObj.interactableItems[i].interactablePickup);
            }
        }

        void SpawnInteractables(InteractablePickup interactablePickup)
        {
            List<int> nodeID = new List<int>();
            nodeID.Clear();
            int k = (int)interactablePickup;
            switch (interactablePickup)
            {
                case InteractablePickup.NONE:
                    break;

                case InteractablePickup.BREIFCASE:
                    nodeID = interactableManager.GetNodeIDOfController(InteractablePickup.BREIFCASE);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        InteractableView briefCaseView = interactableScriptableObj.interactableItems[k]
                                                       .interactableView;
                        Vector3 position = interactableManager.GetNodeLocation(nodeID[i]);
                        InteractableController briefCaseController = new BriefCaseController(position
                        , interactableManager
                        , briefCaseView);
                        interactableManager.AddInteractable(nodeID[i], briefCaseController);
                    }
                    break;

                case InteractablePickup.STONE:
                    nodeID = interactableManager.GetNodeIDOfController(InteractablePickup.STONE);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        InteractableView stoneView = interactableScriptableObj.interactableItems[k]
                                                       .interactableView;
                        Vector3 position = interactableManager.GetNodeLocation(nodeID[i]);
                        InteractableController rockController = new RockInteractableController(position
                        , interactableManager
                        , stoneView);
                        interactableManager.AddInteractable(nodeID[i], rockController);
                    }
                    break;

                case InteractablePickup.BONE:
                    nodeID = interactableManager.GetNodeIDOfController(InteractablePickup.BONE);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        InteractableView boneView = interactableScriptableObj.interactableItems[k]
                                                       .interactableView;
                        Vector3 position = interactableManager.GetNodeLocation(nodeID[i]);
                        InteractableController boneController = new BoneController(position
                        , interactableManager
                        , boneView);
                        interactableManager.AddInteractable(nodeID[i], boneController);
                    }
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