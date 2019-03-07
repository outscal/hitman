using Common;
using GameState;
using PathSystem;
using Zenject;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class DogsEnemyController : EnemyController
    {
        int dogVision = 2;
        Directions newDirection;
        private SignalBus signalBus;
        public DogsEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, SignalBus _signalBus,Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection, bool _hasShield) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection,_hasShield)
        {
            enemyType = EnemyType.DOGS;
            signalBus = _signalBus;
            newDirection = spawnDirection;

            signalBus.Subscribe<NewDogDestinationSignal>(ChangeDestination);
        }

        private void ChangeDestination(NewDogDestinationSignal newDogDestinationSignal)
        {
            alertedPathNodes.Clear();
            //alertedPathNodes = pathService.GetAlertedNodes(newDogDestinationSignal.nodeID);
            alertedPathNodes = pathService.GetShortestPath(currentNodeID,newDogDestinationSignal.nodeID);
                Debug.Log(" stone thrown at"+ newDogDestinationSignal.nodeID);
            
            for (int i = 0; i < alertedPathNodes.Count; i++)
            {

                Debug.Log("alerted nodes after stone shit"+alertedPathNodes[i]);
            }
            alertMoveCalled = 0;
            stateMachine.ChangeEnemyState(EnemyStates.CHASE);
            currentEnemyView.AlertEnemyView();
        }

        async protected override Task MoveToNextNode(int nodeID)
        {

            if (nodeID == -1)
            {
                return;
            }
            int nodeToCheck = nodeID;
            if (stateMachine.GetEnemyState() == EnemyStates.CONSTANT_CHASE)
            {
                Debug.Log("Next node ID [move to next node]"+ nodeID);
                
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
                Vector3 rot = GetRotation(spawnDirection);
                await currentEnemyView.RotateEnemy(rot);

                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;

                AlertEnemy(currentEnemyService.GetPlayerNodeID());
                
            }
            else if(stateMachine.GetEnemyState()==EnemyStates.CHASE)
            {

                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
                Vector3 rot = GetRotation(spawnDirection);
                await currentEnemyView.RotateEnemy(rot);

                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;
            }

            if (CheckForPlayerPresence(nodeID))
            {
                if (!currentEnemyService.CheckForKillablePlayer())
                {
                    return;
                }

                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;
                currentEnemyService.TriggerPlayerDeath();
            }
            else
            {
                // int newNodeID = pathService.GetNextNodeID(nodeID, spawnDirection);
                if (stateMachine.GetEnemyState() == EnemyStates.IDLE)
                { CheckForNextNodeInStraightDirection(nodeToCheck); }           
            }

        }

        private void CheckForNextNodeInStraightDirection(int nodeToCheck)
        {
            int nextNodeCheck = pathService.GetNextNodeID(nodeToCheck, spawnDirection);

            if (nextNodeCheck == -1)
            {
                return;
            }
            if (CheckForPlayerPresence(nextNodeCheck))
            {
                Debug.Log("[dog enemy]Enemy alerted at : node" + nextNodeCheck);
                AlertEnemy(nextNodeCheck);
                return;
            }
        }

        protected override void SetController()
        {
            currentEnemyView.SetCurrentController(this);
        }

      async  private Task CheckForNextNodeInAllDirections(int nextNode)
        {
            for (int i = 0; i < directionList.Count; i++)
            {
                int nextNodeCheck = pathService.GetNextNodeID(nextNode, directionList[i]);
                Debug.Log("[check for all direc]" + nextNodeCheck);
                if (nextNodeCheck == -1)
                {
                    continue;
                }
                if (CheckForPlayerPresence(nextNodeCheck))
                {
                    Debug.Log("[dog enemy]Enemy alerted at : node" + nextNodeCheck);
                    AlertEnemy(nextNodeCheck);
                    break;
                }
            }
            await new WaitForEndOfFrame();
        }

        public override void AlertEnemy(int _destinationID)
        {
            int middleID = pathService.GetNextNodeID(currentNodeID, spawnDirection);
            Debug.Log("Alerted");
            if(alertedPathNodes.Count!=0)
            {
                middleID = alertedPathNodes[2];
            }
            alertedPathNodes.Clear();

            stateMachine.ChangeEnemyState(EnemyStates.CONSTANT_CHASE);
            alertedPathNodes.Add(currentNodeID);            
            alertedPathNodes.Add(middleID);
            alertedPathNodes.Add(_destinationID);

            alertMoveCalled = 0;
            currentEnemyView.AlertEnemyView();

        }

    }
}