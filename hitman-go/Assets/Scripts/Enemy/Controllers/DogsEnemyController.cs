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
                
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
                Vector3 rot = GetRotation(spawnDirection);
                await currentEnemyView.RotateEnemy(rot);

                currentNodeID = nodeID;
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                if (currentNodeID == alertedPathNodes[alertedPathNodes.Count - 1])
                {
                    stateMachine.ChangeEnemyState(EnemyStates.IDLE);
                    currentEnemyView.DisableAlertView();

                }
                else { AlertEnemy(currentEnemyService.GetPlayerNodeID()); }
                
            }
            else if(stateMachine.GetEnemyState()==EnemyStates.CHASE)
            {

                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
                Vector3 rot = GetRotation(spawnDirection);
                await currentEnemyView.RotateEnemy(rot);

                currentNodeID = nodeID;
               await currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                if (currentNodeID == alertedPathNodes[alertedPathNodes.Count - 1])
                {
                    stateMachine.ChangeEnemyState(EnemyStates.IDLE);
                    currentEnemyView.DisableAlertView();
                }
                
            }

            if (CheckForPlayerPresence(nodeID))
            {
                if (!currentEnemyService.CheckForKillablePlayer(GetEnemyType()))
                {
                    return;
                }

                currentNodeID = nodeID;
              await  currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
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
                
                if (nextNodeCheck == -1)
                {
                    continue;
                }
                if (CheckForPlayerPresence(nextNodeCheck))
                {
                    
                    AlertEnemy(nextNodeCheck);
                    break;
                }
            }
            await new WaitForEndOfFrame();
        }

        public override void AlertEnemy(int _destinationID)
        {
            int middleID = pathService.GetNextNodeID(currentNodeID, spawnDirection);
            
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