using System.Collections.Generic;
using UnityEngine;
using Common;
using PathSystem;
using Zenject;
using Enemy;
using GameState;
using System.Linq;

namespace InteractableSystem
{
    public class InteractableManager : IInteractable
    {
        readonly SignalBus signalBus;
        private IPathService pathService;
        private Dictionary<int, InteractableController> interactableControllers;
        private InteractableFactory interactableFactory;

        public InteractableManager(IPathService pathService, InteractableScriptableObj interactableScriptableObjList, SignalBus signalBus)
        {
            this.signalBus = signalBus;
            this.pathService = pathService;
            interactableControllers = new Dictionary<int, InteractableController>();
            interactableFactory = new InteractableFactory(this, interactableScriptableObjList);
            signalBus.Subscribe<GameStartSignal>(GameStart);
            signalBus.Subscribe<ResetSignal>(ResetInteractableDictionary);
        }

        void GameStart()
        {
            interactableFactory.SpawnPickups();
        }

        void ResetInteractableDictionary()
        {
            foreach (int i in interactableControllers.Keys)
            {
                interactableControllers[i].Destroy();
            }

            interactableControllers.Clear();
            interactableControllers = new Dictionary<int, InteractableController>();
        }

        public List<int> GetNodeIDOfController(InteractablePickup interactablePickup)
        {
            return pathService.GetPickupSpawnLocation(interactablePickup);
        }

        public void AddInteractable(int nodeID, InteractableController interactableController)
        {
            interactableControllers.Add(nodeID, interactableController);
        }

        public void RemoveInteractable(InteractableController interactableController)
        {
            foreach(int i in interactableControllers.Keys)
            {
                if(interactableController == interactableControllers[i])
                {
                    int key=interactableControllers.FirstOrDefault(x=>x.Value==interactableControllers[i]).Key;
                    interactableControllers.Remove(key);
                    interactableController.Destroy();
                    interactableController = null;
                    break; 
                }
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
                    return true;
            }
            return false;
        }
    }
}