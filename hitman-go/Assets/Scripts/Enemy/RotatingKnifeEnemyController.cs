using UnityEngine;
using PathSystem;
using Common;
using GameState;
using System.Collections;

namespace Enemy
{
    public class RotatingKnifeEnemyController : EnemyController
    {


        public RotatingKnifeEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {


        }

        protected override void MoveToNextNode(int nodeID)
        {
            ChangeDirection();
            currentEnemyView.RotateEnemy(new Vector3(0, 180, 0));
      

            if(CheckForPlayerPresence(nodeID))
            {
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentEnemyService.TriggerPlayerDeath();
            }
        }
    }
}