using UnityEngine;
using Common;
using System.Collections;
using PathSystem;
using GameState.Interface;
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
        protected int currentNodeID;
        protected int enemyID;

        public EnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation,EnemyScriptableObject _enemyScriptableObject, int _currentNodeID,Directions _spawnDirection)
        {
            currentEnemyService = _enemyService;
            spawnLocation = _spawnLocation;
            enemyScriptableObject = _enemyScriptableObject;
            pathService = _pathService;
            spawnDirection = _spawnDirection;
            currentNodeID = _currentNodeID;
            gameService = _gameService;
            SpawnEnemyView();
        }
    
        protected virtual void SpawnEnemyView()
        {
            //SPAWN ENEMY VIEW
            enemyInstance=GameObject.Instantiate(enemyScriptableObject.enemyPrefab.gameObject);
            currentEnemyView = enemyInstance.GetComponent<IEnemyView>();
            enemyInstance.transform.localPosition = spawnLocation;
            switch(spawnDirection)
            {
                case Directions.DOWN:
                    enemyInstance.transform.localEulerAngles = new Vector3(0,-180f,0);
                    break;
                case Directions.UP:
                    enemyInstance.transform.localEulerAngles = new Vector3(0,180f,0);
                    break;
                case Directions.LEFT:
                    enemyInstance.transform.localEulerAngles = new Vector3(0,90f,0);
                    break;
                case Directions.RIGHT:
                    enemyInstance.transform.localEulerAngles = new Vector3(0,-90f,0);
                    break;
            }
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
            if(gameService.GetCurrentState()== GameStatesType.ENEMYSTATE)
            {
                Debug.Log("inside move");
                int nextNodeID = pathService.GetNextNodeID(currentNodeID,spawnDirection);
                MoveToNextNode(nextNodeID);                
            }
            
        }

        protected virtual bool CheckForPlayerPresence(int _nextNodeID)
        {
            if (currentEnemyService.GetPlayerNodeID() == _nextNodeID)
            {
                //trigger PlayerDeathSignal
                return true;
            }
            else
                return false;
        }
    }
}