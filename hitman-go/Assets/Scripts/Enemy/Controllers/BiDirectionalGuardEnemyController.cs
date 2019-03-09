using Common;
using PathSystem;
using Player;
using System.Threading.Tasks;
using GameState;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class BiDirectionalEnemyController : EnemyController
    {
        private Directions secondDirection;

        public BiDirectionalEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection, bool _hasShield) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection, _hasShield)
        {
            enemyType = EnemyType.BIDIRECTIONAL;
            SetSecondDirection();
        }
        
        async protected override Task MoveToNextNode(int nodeID)
        {
            if(stateMachine.GetEnemyState()==EnemyStates.CHASE)
            {
                spawnDirection= pathService.GetDirections(currentNodeID, nodeID);              
                Vector3 rot=GetRotation(spawnDirection);
                await currentEnemyView.RotateEnemy(rot);
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;             
            }

            if (CheckForPlayerPresence(nodeID))
            {
                if(!currentEnemyService.CheckForKillablePlayer(GetEnemyType()))
                {
                    return;
                }               
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));

                currentNodeID = nodeID;
                currentEnemyService.TriggerPlayerDeath();
            }
            else
            {
                int secondNodeCheck = pathService.GetNextNodeID(currentNodeID,secondDirection);
                if (CheckForPlayerPresence(secondNodeCheck))
                {
                    if (!currentEnemyService.CheckForKillablePlayer(GetEnemyType()))
                    {
                        return;
                    }
                    currentEnemyView.MoveToLocation(pathService.GetNodeLocation(secondNodeCheck));

                    currentNodeID = secondNodeCheck;
                    currentEnemyService.TriggerPlayerDeath();
                }
            }
          
        }
        private void SetSecondDirection()
        {
            if (spawnDirection == Directions.UP)
            {
                secondDirection = Directions.DOWN;
            }
            else if (spawnDirection == Directions.LEFT)
            {
                secondDirection = Directions.RIGHT;
            }
            else if (spawnDirection == Directions.DOWN)
            {
                secondDirection = Directions.UP;
            }
            else if (spawnDirection == Directions.RIGHT)
            {
                secondDirection = Directions.LEFT;
            }
        }
        protected override void SetController()
        {
            currentEnemyView.SetCurrentController(this);
        }

    }
}