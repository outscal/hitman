using UnityEngine;
using Common;
using System.Collections;
using System;

namespace Enemy
{
    public class EnemyController:IEnemyController
    {
        private IEnemyService currentEnemyService;
        private EnemyScriptableObject enemyScriptableObject;
        private EnemyView currentEnemyView;
        private Node spawnNode;

        public EnemyController(IEnemyService enemyService, Node _spawnNode,EnemyScriptableObject _enemyScriptableObject)
        {
            currentEnemyService = enemyService;
            spawnNode = _spawnNode;
            enemyScriptableObject = _enemyScriptableObject;

            SpawnEnemyView();
        }

        private void SpawnEnemyView()
        {
            //SPAWN ENEMY VIEW
            currentEnemyView=enemyScriptableObject.enemyPrefab;
            GameObject enemyInstance=GameObject.Instantiate(currentEnemyView.gameObject);
            enemyInstance.transform.localPosition = spawnNode.nodePosition;
            enemyInstance.transform.localRotation = enemyScriptableObject.enemyRotation;
        }
    }
}