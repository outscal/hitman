using Common;
using GameState;
using PathSystem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : IEnemyController
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
        protected List<Directions> directionList = new List<Directions>();
        protected int currentNodeID;
        protected int enemyID;
        protected int alertMoveCalled;
        protected EnemyType enemyType;
        protected bool hasShield;
        

        public EnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int _currentNodeID, Directions _spawnDirection,bool _hasShield)
        {
            currentEnemyService = _enemyService;
            if (currentEnemyService == null)
            {
                Debug.Log("null enemy service");
            }
            spawnLocation = _spawnLocation;
            enemyScriptableObject = _enemyScriptableObject;
            pathService = _pathService;
            spawnDirection = _spawnDirection;
            currentNodeID = _currentNodeID;
            gameService = _gameService;
            hasShield = _hasShield;
            stateMachine = new EnemyStateMachine();
            SpawnEnemyView();
            PopulateDirectionList();
        }

        private void PopulateDirectionList()
        {
            directionList.Add(Directions.DOWN);  
            directionList.Add(Directions.LEFT);  
            directionList.Add(Directions.RIGHT);  
            directionList.Add(Directions.UP);  
        }

        protected virtual void SpawnEnemyView()
        {

            enemyInstance = GameObject.Instantiate(enemyScriptableObject.enemyPrefab.gameObject,spawnLocation,Quaternion.Euler( GetRotation(spawnDirection)));
            currentEnemyView = enemyInstance.GetComponent<IEnemyView>();
            //currentEnemyView.SetPosition(spawnLocation);
            //currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
            //enemyInstance.transform.Rotate(GetRotation(spawnDirection));
            SetController();

        }

        protected virtual void SetController()
        {
            currentEnemyView.SetCurrentController(this);
        }

        public void Reset()
        {
            stateMachine = null;
            currentEnemyView.Reset();
        }

        public int GetCurrentNodeID()
        {
            return currentNodeID;
        }

        public void DisableEnemy()
        {
            currentEnemyView.DisableEnemy();
            currentEnemyView = null;
        }

        public void SetID(int _ID)
        {
            enemyID = _ID;
        }

        async protected virtual Task MoveToNextNode(int nodeID)
        {

        }

        async public virtual Task Move()
        {            
            alertMoveCalled++;
            if (stateMachine.GetEnemyState() == EnemyStates.IDLE)
            {
                int nextNodeID = pathService.GetNextNodeID(currentNodeID, spawnDirection);              

                await MoveToNextNode(nextNodeID);
            }

            else if (stateMachine.GetEnemyState() == EnemyStates.CHASE || stateMachine.GetEnemyState()==EnemyStates.CONSTANT_CHASE)
            {
                int nextNodeID = alertedPathNodes[alertMoveCalled];
                //currentEnemyView.RotateEnemy(pathService.GetNodeLocation(nextNodeID));
                if(pathService.CanEnemyMoveToNode(currentNodeID,nextNodeID))
                {
                   await MoveToNextNode(nextNodeID);
                }              
               
                
                if (alertMoveCalled == alertedPathNodes.Count - 1)
                {
                    stateMachine.ChangeEnemyState(EnemyStates.IDLE);
                    currentEnemyView.DisableAlertView();
                }
            }

            
           

        }

        protected virtual bool CheckForPlayerPresence(int _nextNodeID)
        {
            return (currentEnemyService.GetPlayerNodeID() == _nextNodeID);

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
            else if (spawnDirection == Directions.RIGHT)
            {
                spawnDirection = Directions.LEFT;

            }
        }

        public virtual EnemyType GetEnemyType()
        {
            return enemyType;
        }

        public virtual void AlertEnemy(int _destinationID)
        {
            if (stateMachine.GetEnemyState() != EnemyStates.CHASE)
            {
                stateMachine.ChangeEnemyState(EnemyStates.CHASE);
            }
            alertedPathNodes = pathService.GetShortestPath(currentNodeID, _destinationID);
            alertMoveCalled = 0;
            currentEnemyView.AlertEnemyView();
            Debug.Log("[Alert Enemy] called by dogs controller");

        }

        protected virtual Vector3 GetRotation(Directions _spawnDirection)
        {
            switch (_spawnDirection)
            {
                case Directions.DOWN:
                    return new Vector3(0, 0, 0);
                case Directions.LEFT:
                    return new Vector3(0, 90, 0);
                case Directions.RIGHT:
                    return new Vector3(0, -90, 0);
                case Directions.UP:
                    return new Vector3(0, 180, 0);
                default:
                    return Vector3.zero;

            }
        }

       async public Task KillPlayer()
        {
            currentEnemyService.TriggerPlayerDeath();
            await new WaitForEndOfFrame();
        }

        public void ChangeState(EnemyStates _state)
        {
            stateMachine.ChangeEnemyState(_state);
        }

        public EnemyStates GetEnemyState()
        {
            return stateMachine.GetEnemyState();
        }

        public Directions GetDirection()
        {
            return spawnDirection;
        }

        public virtual bool IsKillable(KillMode killMode)
        {
            if (hasShield && killMode == KillMode.SHOOT)
            {
                return false;
            }
            else { return true; }
        }
    }
}