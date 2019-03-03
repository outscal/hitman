using UnityEngine;
using GameState;
using Common;
using PathSystem;
using System.Collections;
using System;
using System.Threading.Tasks;

namespace Enemy
{
    public class SniperEnemyController : EnemyController
    {


        public SniperEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {
            enemyType = EnemyType.SNIPER;
        }

        protected override void SetController()
        {
            currentEnemyView.SetCurrentController(this);
           
        }
        private void PerformRaycast()
        {
            currentEnemyView.PerformRaycast();
        }
       async protected override Task MoveToNextNode(int nodeID)
        {
            PerformRaycast();
            await new WaitForEndOfFrame();
        }
    }
}