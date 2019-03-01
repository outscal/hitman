using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using PathSystem;
using Zenject;
using Enemy;
using GameState;

namespace InteractableSystem
{
    public class InteractableManager : IInteractable
    {
        readonly SignalBus signalBus;
        private IPathService pathService;
        private Dictionary<int, InteractableController> interactableControllers;
        private InteractableScriptableObj interactableScriptableObj;

        public InteractableManager(IPathService pathService, InteractableScriptableObj interactableScriptableObjList, SignalBus signalBus)
        {
            interactableScriptableObj = interactableScriptableObjList;
            this.signalBus = signalBus;
            this.pathService = pathService;
            interactableScriptableObj=interactableScriptableObjList;
            interactableControllers = new Dictionary<int, InteractableController>();
            signalBus.Subscribe<GameStartSignal>(GameStart);
            signalBus.Subscribe<ResetSignal>(ResetInteractableDictionary);

        }

        void GameStart()
        {
            SpawnPickups(interactableScriptableObj);
        }

        void ResetInteractableDictionary()
        {
            for (int i = 0; i < interactableControllers.Count; i++)
            {
                if (interactableControllers[i] != null)
                {
                    interactableControllers[i].Destroy();
                    interactableControllers[i] = null;
                }
                else
                {
                    Debug.Log("[InteractableManager] Item not in List"); 
                }
            }

            interactableControllers = new Dictionary<int, InteractableController>();
        }

        public void RemoveInteractable(InteractableController interactableController)
        {
            foreach(int i in interactableControllers.Keys)
            {
                if(interactableController == interactableControllers[i])
                {
                    interactableController.Destroy();
                    interactableController = null;
                    interactableControllers.Remove(i);
                    break; 
                }
            }
        }

        public void OnGameStart()
        {
            SpawnPickups(interactableScriptableObj);
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
            nodeID.Clear();
            int k = (int)interactablePickup;
            switch (interactablePickup)
            {
                case InteractablePickup.NONE:
                    break;
                case InteractablePickup.BREIFCASE:
                    nodeID = pathService.GetPickupSpawnLocation(InteractablePickup.BREIFCASE);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        InteractableView briefCaseView = interactableScriptableObj.interactableItems[k]
                                                       .interactableView;
                        Vector3 position = pathService.GetNodeLocation(nodeID[i]);
                        InteractableController briefCaseController = new BriefCaseController(position
                        , this
                        , briefCaseView);
                        interactableControllers.Add(nodeID[i], briefCaseController);
                    }
                    break;
                case InteractablePickup.STONE:
                    nodeID = pathService.GetPickupSpawnLocation(InteractablePickup.STONE);

                    for (int i = 0; i < nodeID.Count; i++)
                    {
                        InteractableView stoneView = interactableScriptableObj.interactableItems[k]
                                                       .interactableView;
                        Vector3 position = pathService.GetNodeLocation(nodeID[i]);
                        InteractableController rockController = new RockInteractableController(position
                        , this
                        , stoneView);
                        interactableControllers.Add(nodeID[i], rockController);
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

        public void SendBriefCaseSignal()
        {
            signalBus.TryFire<BriefCaseSignal>();
        }

        public Vector3 GetNodeLocation(int nodeID)
        {
            return pathService.GetNodeLocation(nodeID);
        }

        public IInteractableController ReturnInteractableController(int nodeID)
        {
            InteractableController interactableController;
            if (interactableControllers.TryGetValue(nodeID, out interactableController))
            {
                interactableController.DeactivateView();
                return interactableController;
            }

            return interactableController;
        }

        public bool CheckForInteractable(int nodeID)
        {
            foreach (int node in interactableControllers.Keys)
            {
                if(node == nodeID)
                {
                    return true; 
                }
            }

            return false;
        }
    }
}