using UnityEngine;
using Common;
using System.Collections;
using System;

namespace Enemy
{
    public class EnemyController:IEnemyController
    {
        protected IEnemyService currentEnemyService;
        protected EnemyScriptableObject enemyScriptableObject;
        protected IEnemyView currentEnemyView;
        protected Vector3 spawnLocation;
        protected int enemyID;

        public EnemyController(IEnemyService _enemyService, Vector3 _spawnLocation,EnemyScriptableObject _enemyScriptableObject)
        {
            currentEnemyService = _enemyService;
            spawnLocation = _spawnLocation;
            enemyScriptableObject = _enemyScriptableObject;

            SpawnEnemyView();
        }

        private void SpawnEnemyView()
        {
            //SPAWN ENEMY VIEW
            currentEnemyView=enemyScriptableObject.enemyPrefab;
            GameObject enemyInstance=GameObject.Instantiate(currentEnemyView.GetGameObject());
            enemyInstance.transform.localPosition = spawnLocation;
            enemyInstance.transform.localRotation = enemyScriptableObject.enemyRotation;
        }
        public void SetID(int _ID)
        {
            enemyID = _ID;
        }
    }
}