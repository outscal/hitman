using GameState;
using PathSystem;
using Common;
using System.Threading.Tasks;
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

       async protected override Task MoveToNextNode(int nodeID)
        {    
            if (nodeID == -1)
            {
                ChangeDirection();
                nodeID = pathService.GetNextNodeID(currentNodeID, spawnDirection);
               await currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
            }
            if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {               
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
              await currentEnemyView.RotateEnemy(GetRotation(spawnDirection));

            }
            if (CheckForPlayerPresence(nodeID))
            {
                if(currentEnemyService.CheckForKillablePlayer())
                {                   
                    currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                    currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
                    currentEnemyService.TriggerPlayerDeath();
                }
            }
            currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
            currentNodeID = nodeID;

           
            int n = pathService.GetNextNodeID(currentNodeID, spawnDirection);
            if (n == -1)
            {
                ChangeDirection();
               
              await  currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
            }
        }

        

    }
}