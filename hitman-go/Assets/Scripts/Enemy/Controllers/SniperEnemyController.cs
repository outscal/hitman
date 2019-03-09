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


        public SniperEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection, bool _hasShield) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection, _hasShield)
        {
            enemyType = EnemyType.SNIPER;
            
           
        }

        protected override void SetController()
        {
            currentEnemyView.SetCurrentController(this);

        }
        async private Task PerformRaycast()
        {
            await currentEnemyView.PerformRaycast();
        }

        async protected override Task MoveToNextNode(int nodeID)
        {
            if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                StopRaycast();
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
                currentNodeID = nodeID;
                await currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));

            }
            else {
               await PerformRaycast();
              
            }           

            await new WaitForEndOfFrame();
           
        }

        private void StopRaycast()
        {
            currentEnemyView.StopRaycast();
        }
    }
}