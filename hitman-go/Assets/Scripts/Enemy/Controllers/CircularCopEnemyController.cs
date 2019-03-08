using Common;
using GameState;
using PathSystem;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemy
{
    public class CircularCopEnemyController : EnemyController
    {
        public CircularCopEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection, bool _hasShield) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection, _hasShield)
        {
            enemyType = EnemyType.CIRCULAR_COP;

        }
        protected override void SetController()
        {
            currentEnemyView.SetCurrentController(this);
        }

        async public override Task Move()
        {
            alertMoveCalled++;
           
            if (stateMachine.GetEnemyState() == EnemyStates.IDLE)
            {
                originalMoveCalled++;
                int nextNodeID = originalPath[originalMoveCalled];
                await MoveToNextNode(nextNodeID);

                if (originalMoveCalled == originalPath.Count - 1)
                {
                    originalMoveCalled = -1;
                }
            }
            else if (stateMachine.GetEnemyState() == EnemyStates.RETURN_TO_PATH)
            {
                returnToPathCalled++;
                int nextNode = alertedPathNodes[alertedPathNodes.Count-returnToPathCalled];
                if(originalPath.Contains(nextNode))
                {
                    returnToPathCalled = -1;
                    stateMachine.ChangeEnemyState(EnemyStates.IDLE);
                    originalMoveCalled = nextNode;
                }
            }
            else if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                int nextNodeID = alertedPathNodes[alertMoveCalled];
                if (pathService.CanEnemyMoveToNode(currentNodeID, nextNodeID))
                {
                    await MoveToNextNode(nextNodeID);
                }
                if (alertMoveCalled == alertedPathNodes.Count - 1)
                {
                    stateMachine.ChangeEnemyState(EnemyStates.RETURN_TO_PATH);
                }
            }
        }

        async protected override Task MoveToNextNode(int nodeID)
        {

        }
    }
}