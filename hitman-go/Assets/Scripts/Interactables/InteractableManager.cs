using System.Collections.Generic;
using UnityEngine;
using Common;
using PathSystem;
using Zenject;
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
        private InteractableScriptableObj interactableScriptableObjList;

        public InteractableManager(IPathService pathService, InteractableScriptableObj interactableScriptableObjList
        , SignalBus signalBus)
        {
            this.signalBus = signalBus;
            this.pathService = pathService;
            this.interactableScriptableObjList = interactableScriptableObjList;
            interactableControllers = new Dictionary<int, InteractableController>();
            interactableFactory = new InteractableFactory(this);
            signalBus.Subscribe<GameStartSignal>(GameStart);
            signalBus.Subscribe<ResetSignal>(ResetInteractableDictionary);
        }

        void GameStart()
        {
            interactableFactory.SpawnPickups(interactableScriptableObjList);
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
                    int key = interactableControllers
                             .FirstOrDefault
                                (
                                    x => x.Value == interactableControllers[i]
                                ).Key;

                    interactableControllers.Remove(key);
                    interactableController.Destroy();
                    interactableController = null;
                    break; 
                }
            }
        }

        public SignalBus ReturnSignalBus()
        {
            return signalBus; 
        }

        public IPathService ReturnPathService()
        {
            return pathService;
        }

        public IInteractableController ReturnInteractableController(int nodeID)
        {
            InteractableController interactableController;
            if (interactableControllers.TryGetValue(nodeID, out interactableController))
            {
                interactableController.InteractablePickedUp();
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