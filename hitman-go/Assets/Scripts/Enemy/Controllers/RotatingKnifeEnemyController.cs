using Common;
using GameState;
using PathSystem;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

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
            Debug.Log("move to next node called [enemy controller]");
            currentEnemyView.RotateInOppositeDirection();

            if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                spawnDirection = pathService.GetDirections(currentNodeID, nodeID);
                await currentEnemyView.RotateInOppositeDirection();
            }

            if (nodeID==-1)
            {
                Debug.Log("Node is -1 , direction is "+spawnDirection);
                ChangeDirection();
                return;
            }

            if (CheckForPlayerPresence(nodeID))
            {
                if (!currentEnemyService.CheckForKillablePlayer())
                {
                    return;
                }
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;
                currentEnemyService.TriggerPlayerDeath();

            }
            else { ChangeDirection();
                await new WaitForEndOfFrame();
            }
        }
        protected override void SetController()
        {
            currentEnemyView.SetCurrentController(this);
        }
    }
}