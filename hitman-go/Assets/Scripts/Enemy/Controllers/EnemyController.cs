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
        protected int currentNodeID;
        protected int enemyID;
        private int alertMoveCalled;
        protected EnemyType enemyType;

        public EnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int _currentNodeID, Directions _spawnDirection)
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
            stateMachine = new EnemyStateMachine();
            SpawnEnemyView();
        }

        protected virtual void SpawnEnemyView()
        {

            enemyInstance = GameObject.Instantiate(enemyScriptableObject.enemyPrefab.gameObject);
            currentEnemyView = enemyInstance.GetComponent<IEnemyView>();
            currentEnemyView.SetPosition(spawnLocation);

            enemyInstance.transform.Rotate(GetRotation(spawnDirection), Space.World);
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

        public int GetCurrentID()
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

        async public Task Move()
        {
            alertMoveCalled++;
            if (stateMachine.GetEnemyState() == EnemyStates.IDLE)
            {
                int nextNodeID = pathService.GetNextNodeID(currentNodeID, spawnDirection);
                Debug.Log("nextNOde id " + nextNodeID);
                Debug.Log("current spawn direction " + spawnDirection.ToString());
                await MoveToNextNode(nextNodeID);
            }
            else if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {

                int nextNodeID = alertedPathNodes[alertMoveCalled];

               await MoveToNextNode(nextNodeID);

                if (alertMoveCalled == alertedPathNodes.Count - 1)
                {
                    stateMachine.ChangeEnemyState(EnemyStates.IDLE);
                    currentEnemyView.DisableAlertView();
                }
            }
            await new WaitForEndOfFrame();

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

        public void AlertEnemy(int _destinationID)
        {
            stateMachine.ChangeEnemyState(EnemyStates.CHASE);
            alertedPathNodes = pathService.GetShortestPath(currentNodeID, _destinationID);
            alertMoveCalled = 0;
            currentEnemyView.AlertEnemyView();
            int nextNodeToLook = pathService.GetNextNodeID(currentNodeID, spawnDirection);
            Vector3 _destinationLocation = pathService.GetNodeLocation(nextNodeToLook);
            currentEnemyView.RotateEnemy(_destinationLocation);

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

        public void KillPlayer()
        {
            currentEnemyService.TriggerPlayerDeath();
        }
    }
}