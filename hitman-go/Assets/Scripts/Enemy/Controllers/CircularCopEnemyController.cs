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
                await MoveToNextNode(nextNodeID);

                int rotNode = originalMoveCalled + 1;
                if (rotNode >= originalPath.Count)
                {
                    rotNode = 0;
                }
                int toLookNode = originalPath[rotNode];


                Directions toLook = pathService.GetDirections(currentNodeID, toLookNode);
                Debug.Log("Direction to look" + toLook.ToString());
                currentEnemyView.RotateEnemy(GetRotation(toLook));
               

            }
            if (stateMachine.GetEnemyState() == EnemyStates.RETURN_TO_PATH)
            {
                currentEnemyView.DisableAlertView();
                returnToPathCalled++;
                int nextNodeID = alertedPathNodes[alertedPathNodes.Count-returnToPathCalled];
                spawnDirection = pathService.GetDirections(currentNodeID, nextNodeID);               
                await currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
                await MoveToNextNode(nextNodeID);
             
                //int rotNode = returnToPathCalled + 1;
                //if (rotNode >= alertedPathNodes.Count)
                //{
                //    rotNode = alertedPathNodes.Count-1;
                //}
                //int toLookNode = alertedPathNodes[alertedPathNodes.Count-rotNode];

                //Directions toLook = pathService.GetDirections(currentNodeID, toLookNode);
                //Debug.Log("Direction to look[return path]" + toLook.ToString());

                if (originalPath.Contains(nextNodeID))
                {
                    returnToPathCalled = 0;
                    stateMachine.ChangeEnemyState(EnemyStates.IDLE);                   
                    currentEnemyView.DisableAlertView();
                    originalMoveCalled = originalPath.IndexOf(nextNodeID);
                }
            }
            if (stateMachine.GetEnemyState() == EnemyStates.CHASE)
            {
                alertMoveCalled++;
                int nextNodeID = alertedPathNodes[alertMoveCalled];
                if (pathService.CanEnemyMoveToNode(currentNodeID, nextNodeID))
                {
                    spawnDirection = pathService.GetDirections(currentNodeID, nextNodeID);                    
                    await currentEnemyView.RotateEnemy(GetRotation(spawnDirection));
                    await MoveToNextNode(nextNodeID);
                }
                if (alertMoveCalled == alertedPathNodes.Count - 1)
                {
                    stateMachine.ChangeEnemyState(EnemyStates.RETURN_TO_PATH);
                    returnToPathCalled = 1;
                }
            }
           

        }

        private async Task LookAtNextNode()
        {
            
        }

        async protected override Task MoveToNextNode(int nodeID)
        {            
            currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
            currentNodeID = nodeID;

            if (CheckForPlayerPresence(nodeID))
            {
                if (!currentEnemyService.CheckForKillablePlayer(enemyType))
                {
                    return;
                }               
                currentEnemyService.TriggerPlayerDeath();
            }
           
            await new WaitForEndOfFrame();
        }

        public override void SetCircularCopID(int id)
        {
            currentEnemyID = id;
            originalPath = pathService.GetOriginalPath(currentEnemyID);
        }

       
    }
}