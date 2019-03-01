using GameState;
using PathSystem;
using Common;

using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class PatrollingEnemyController : EnemyController
    {


        public PatrollingEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {
            enemyType = EnemyType.PATROLLING;

        }

        protected override void MoveToNextNode(int nodeID)
        {    
            if (nodeID == -1)
            {
                ChangeDirection();

                nodeID = pathService.GetNextNodeID(currentNodeID, spawnDirection);
            }
            if (CheckForPlayerPresence(nodeID))
            {
                if(currentEnemyService.CheckForKillablePlayer())
                {                   
                 currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                 currentEnemyService.TriggerPlayerDeath();
                }
            }
            currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));

            currentNodeID = nodeID;
            int n = pathService.GetNextNodeID(currentNodeID, spawnDirection);
            if (n == -1)
            {
                currentEnemyView.RotateEnemy(new Vector3(0, 180, 0));
            }
        }

        

    }
}