using UnityEngine;
using Common;
using System.Collections;
using PathSystem;
using System;

namespace Enemy
{
    public class EnemyController:IEnemyController
    {
        protected IEnemyService currentEnemyService;
        protected EnemyScriptableObject enemyScriptableObject;
        protected IPathService pathService;
        protected IEnemyView currentEnemyView;
        protected Vector3 spawnLocation;
        protected GameObject enemyInstance;
        protected int enemyID;

        public EnemyController(IEnemyService _enemyService, IPathService _pathService, Vector3 _spawnLocation,EnemyScriptableObject _enemyScriptableObject)
        {
            currentEnemyService = _enemyService;
            spawnLocation = _spawnLocation;
            enemyScriptableObject = _enemyScriptableObject;
            pathService = _pathService;

            SpawnEnemyView();
        }
    
        protected virtual void SpawnEnemyView()
        {
            //SPAWN ENEMY VIEW
            enemyInstance=GameObject.Instantiate(enemyScriptableObject.enemyPrefab.gameObject);
            currentEnemyView = enemyInstance.GetComponent<IEnemyView>();
            enemyInstance.transform.localPosition = spawnLocation;
            enemyInstance.transform.localRotation = enemyScriptableObject.enemyRotation;
        }

        public void SetID(int _ID)
        {
            enemyID = _ID;
        }

        public virtual void MoveToNextNode(int nodeID)
        {

        }
    }
}