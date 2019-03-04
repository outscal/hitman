using System.Collections.Generic;
using Common;
using UnityEngine;

namespace InteractableSystem
{
    public class InteractableFactory
    {
        private InteractableManager interactableManager;

        public InteractableFactory(InteractableManager interactableManager)
        {
            this.interactableManager = interactableManager;
        }

        public void SpawnPickups(InteractableScriptableObj interactableScriptableObj)
        {
            for (int i = 0; i < interactableScriptableObj.interactableItems.Count; i++)
            {
                SpawnInteractables(interactableScriptableObj.interactableItems[i].interactablePickup
                                 , interactableScriptableObj);
            }
        }

        void SpawnInteractables(InteractablePickup interactablePickup, InteractableScriptableObj interactableScriptableObj)
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
                        Vector3 position = interactableManager.ReturnPathService()
                                 .GetNodeLocation(nodeID[i]);
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
                        Vector3 position = interactableManager.ReturnPathService()
                                 .GetNodeLocation(nodeID[i]);
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
                        Vector3 position = interactableManager.ReturnPathService()
                                 .GetNodeLocation(nodeID[i]);
                        InteractableController boneController = new BoneController(position
                        , interactableManager
                        , boneView);
                        interactableManager.AddInteractable(nodeID[i], boneController);
                    }
                    break;

                case InteractablePickup.SNIPER_GUN:
                    nodeID = interactableManager.GetNodeIDOfController(InteractablePickup.SNIPER_GUN);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        InteractableView sniperView = interactableScriptableObj.interactableItems[k]
                                                       .interactableView;
                        Vector3 position = interactableManager.ReturnPathService()
                                 .GetNodeLocation(nodeID[i]);
                        InteractableController sniperController = new SniperController(position
                        , interactableManager
                        , sniperView);
                        interactableManager.AddInteractable(nodeID[i], sniperController);
                    }
                    break;

                case InteractablePickup.DUAL_GUN:
                    nodeID = interactableManager.GetNodeIDOfController(InteractablePickup.DUAL_GUN);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        InteractableView dualGunView = interactableScriptableObj.interactableItems[k]
                                                       .interactableView;
                        Vector3 position = interactableManager.ReturnPathService()
                                 .GetNodeLocation(nodeID[i]);
                        InteractableController dualGunController = new DualGunInteractableController(position
                        , interactableManager
                        , dualGunView);
                        interactableManager.AddInteractable(nodeID[i], dualGunController);
                    }
                    break;
                case InteractablePickup.TRAP_DOOR:
                    break;
                case InteractablePickup.COLOR_KEY:
                    break;
                case InteractablePickup.AMBUSH_PLANT:
                    nodeID = interactableManager.GetNodeIDOfController(InteractablePickup.AMBUSH_PLANT);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        InteractableView ambushPlantView = interactableScriptableObj.interactableItems[k]
                                                       .interactableView;
                        Vector3 position = interactableManager.ReturnPathService()
                                 .GetNodeLocation(nodeID[i]);
                        InteractableController ambushPlantController = new AmbushPlantController(position
                        , interactableManager
                        , ambushPlantView);
                        interactableManager.AddInteractable(nodeID[i], ambushPlantController);
                    }
                    break;
                case InteractablePickup.GUARD_DISGUISE:
                    break;
                default:
                    break;
            }
        }
    }
}