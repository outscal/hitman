using Common;
using GameState;
using PathSystem;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class DogsEnemyController : EnemyController
    {
        int dogVision = 2;
        Directions oldDirection;
        public DogsEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {
            enemyType = EnemyType.DOGS;
        }

        async protected override Task MoveToNextNode(int nodeID)
        {
            if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                oldDirection = spawnDirection;
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);

                Vector3 rot = GetRotation(spawnDirection);
                await currentEnemyView.RotateEnemy(rot);
                currentNodeID = nodeID;
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));


            }

            if (CheckForPlayerPresence(nodeID))
            {
                if (!currentEnemyService.CheckForKillablePlayer())
                {
                    return;
                }
                Vector3 rot = GetRotation(spawnDirection);

                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;
                currentEnemyService.TriggerPlayerDeath();
            }
            else
            {
                int nextNodeCheck = pathService.GetNextNodeID(nodeID, oldDirection);

                if (CheckForPlayerPresence(nextNodeCheck))
                {
                    if (!currentEnemyService.CheckForKillablePlayer())
                    {
                        return;
                    }
                    Vector3 rot = GetRotation(oldDirection);

                    currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                    currentNodeID = nodeID;
                    currentEnemyService.TriggerPlayerDeath();
                }
                else
                {
                    return;
                }
            }

        }
    }
}