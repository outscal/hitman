using UnityEngine;
using Common;
using System.Collections;
using System;

namespace Enemy
{
    public class EnemyController:IEnemyController
    {
        private IEnemyService currentEnemyService;
        private Node spawnNode;
        public EnemyController(IEnemyService enemyService, Node _spawnNode)
        {
            currentEnemyService = enemyService;
            spawnNode = _spawnNode;

            SpawnEnemyView();
        }

        private void SpawnEnemyView()
        {
           //SPAWN ENEMY VIEW
        }
    }
}