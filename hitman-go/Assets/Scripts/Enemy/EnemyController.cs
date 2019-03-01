using UnityEngine;
using Common;
using System.Collections.Generic;
using PathSystem;
using GameState;
using System;

namespace Enemy
{
    public class EnemyController:IEnemyController
    {
        protected IEnemyService currentEnemyService;
        protected EnemyScriptableObject enemyScriptableObject;
        protected IPathService pathService;
        protected IEnemyView currentEnemyView;
        protected IGameService gameService;
        protected Vector3 spawnLocation;
        protected GameObject enemyInstance;
        protected Directions spawnDirection;
        protected EnemyStateMachine stateMachine;
        protected List<int> alertedPathNodes = new List<int>();
        protected int currentNodeID;
        protected int enemyID;
        private int alertMoveCalled;
        protected EnemyType enemyType;

        public EnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation,EnemyScriptableObject _enemyScriptableObject, int _currentNodeID,Directions _spawnDirection)
        {
            currentEnemyService = _enemyService;
            spawnLocation = _spawnLocation;
            enemyScriptableObject = _enemyScriptableObject;
            pathService = _pathService;
            spawnDirection = _spawnDirection;
            currentNodeID = _currentNodeID;
            gameService = _gameService;
            stateMachine = new EnemyStateMachine();
            SpawnEnemyView();
        }
    
        protected virtual void SpawnEnemyView()
        {
            //SPAWN ENEMY VIEW
            enemyInstance=GameObject.Instantiate(enemyScriptableObject.enemyPrefab.gameObject);
            currentEnemyView = enemyInstance.GetComponent<IEnemyView>();
            currentEnemyView.SetPosition(spawnLocation);
            
            switch(spawnDirection)
            {
                case Directions.DOWN:               
                    enemyInstance.transform.Rotate(new Vector3(0, 0, 0));
                    break;                 
                case Directions.UP:        
                    enemyInstance.transform.Rotate(new Vector3(0,180f,0));
                    break;                 
                case Directions.LEFT:
                    enemyInstance.transform.Rotate(new Vector3(0, 90f, 0));
                    break;                 
                case Directions.RIGHT:      
                    enemyInstance.transform.Rotate(new Vector3(0,-90f,0));
                    break;
            
            }
        }

        public void Reset()
        {
            stateMachine = null;
            currentEnemyView.Reset();
        }

        public int GetCurrentID()
        {
            return currentNodeID;
        }

        public void DisableEnemy()
        {
            currentEnemyView.DisableEnemy();
            currentEnemyView=null;
        }

        public void SetID(int _ID)
        {
            enemyID = _ID;
        }

        protected virtual void MoveToNextNode(int nodeID)
        {
            
        }

        public void Move()
        {
            alertMoveCalled++;
            if (gameService.GetCurrentState() == GameStatesType.ENEMYSTATE)
            {
                if (stateMachine.GetEnemyState() == EnemyStates.IDLE)
                {
                    int nextNodeID = pathService.GetNextNodeID(currentNodeID, spawnDirection);
                    MoveToNextNode(nextNodeID);
                }
                else if(stateMachine.GetEnemyState()==EnemyStates.CHASE)
                {
                    
                    int nextNodeID=alertedPathNodes[alertMoveCalled];
                    Debug.Log("next node in chase state: "+ nextNodeID);
                    
                    MoveToNextNode(nextNodeID);
                    if (alertMoveCalled == alertedPathNodes.Count-1)
                    {
                        stateMachine.ChangeEnemyState(EnemyStates.IDLE);
                        currentEnemyView.DisableAlertView();
                    }

                }
            }
        }

        protected virtual bool CheckForPlayerPresence(int _nextNodeID)
        {
            if (currentEnemyService.GetPlayerNodeID() == _nextNodeID)
            {
                  return true;
            }
            else
                return false;
        }

        protected virtual void ChangeDirection()
        {
            if (spawnDirection == Directions.UP)
            {
                spawnDirection = Directions.DOWN;
            }
            else if (spawnDirection == Directions.LEFT)
            {
                spawnDirection = Directions.RIGHT;
            }
            else if (spawnDirection == Directions.DOWN)
            {
                spawnDirection = Directions.UP;
            }
            else
            {
                spawnDirection = Directions.LEFT;

            }
        }
    
        public virtual EnemyType GetEnemyType()
        {
            return enemyType;
        }

        public void AlertEnemy(int _destinationID)
        {
           stateMachine.ChangeEnemyState(EnemyStates.CHASE);
           alertedPathNodes= pathService.GetShortestPath(currentNodeID,_destinationID);
           alertMoveCalled = 0;            
           currentEnemyView.AlertEnemyView();
           Vector3 _destinationLocation = pathService.GetNodeLocation(_destinationID);
           currentEnemyView.LookAtNode(_destinationLocation);

        }
    }
}