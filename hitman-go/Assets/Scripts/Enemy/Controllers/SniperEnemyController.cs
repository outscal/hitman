using Common;
using GameState;
using PathSystem;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

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
            if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
                await currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;
            }
            PerformRaycast();

            await new WaitForFixedUpdate();
           

        }
    }
}