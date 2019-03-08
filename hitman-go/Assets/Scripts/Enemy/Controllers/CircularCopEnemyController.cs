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
        private int currentEnemyID;
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
           
            if (stateMachine.GetEnemyState() == EnemyStates.IDLE)
            {
                if (originalMoveCalled >= originalPath.Count - 1)
                {
                    originalMoveCalled = -1;
                }

                originalMoveCalled++;
                int nextNodeID = originalPath[originalMoveCalled];
                spawnDirection = pathService.GetDirections(currentNodeID, nextNodeID);
                await currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
                await MoveToNextNode(nextNodeID);

            }
            else if (stateMachine.GetEnemyState() == EnemyStates.RETURN_TO_PATH)
            {
                returnToPathCalled++;
                int nextNode = alertedPathNodes[alertedPathNodes.Count-returnToPathCalled];
                await MoveToNextNode(nextNode);

                if(originalPath.Contains(nextNode))
                {
                    returnToPathCalled = 0;
                    stateMachine.ChangeEnemyState(EnemyStates.IDLE);
                    originalMoveCalled = originalPath.IndexOf(nextNode);
                }
            }
            else if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                alertMoveCalled++;
                int nextNodeID = alertedPathNodes[alertMoveCalled];
                if (pathService.CanEnemyMoveToNode(currentNodeID, nextNodeID))
                {
                    await MoveToNextNode(nextNodeID);
                }
                if (alertMoveCalled == alertedPathNodes.Count - 1)
                {
                    stateMachine.ChangeEnemyState(EnemyStates.RETURN_TO_PATH);
                    returnToPathCalled = 1;
                }
            }
        }

        async protected override Task MoveToNextNode(int nodeID)
        {            
            currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
            currentNodeID = nodeID;

            if (CheckForPlayerPresence(nodeID))
            {
                if (!currentEnemyService.CheckForKillablePlayer())
                {
                    return;
                }
               // currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));

              //  currentNodeID = nodeID;
                currentEnemyService.TriggerPlayerDeath();
            }
        }

        public override void SetCircularCopID(int id)
        {
            currentEnemyID = id;
            originalPath = pathService.GetOriginalPath(currentEnemyID);
        }
    }
}