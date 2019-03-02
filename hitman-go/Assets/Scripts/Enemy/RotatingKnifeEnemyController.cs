using UnityEngine;
using PathSystem;
using System.Threading.Tasks;
using Common;
using GameState;
using System.Collections;

namespace Enemy
{
    public class RotatingKnifeEnemyController : EnemyController
    {


        public RotatingKnifeEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {
            enemyType = EnemyType.ROTATING_KNIFE;

        }

        async protected override Task MoveToNextNode(int nodeID)
        {
            if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
            }
            else
            {
                ChangeDirection();
            }


            if (CheckForPlayerPresence(nodeID))
            {
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentEnemyService.TriggerPlayerDeath();
            }
            else
            {
                currentEnemyView.RotateInOppositeDirection();
            }
        }
    }
}