using Common;
using PathSystem;
using Player;
using GameState;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class StaticEnemyController : EnemyController
    {
        

        public StaticEnemyController(IEnemyService _enemyService, IPathService _pathService, IGameService _gameService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _gameService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {
            

        }
        protected override void MoveToNextNode(int nodeID)
        {
            if(stateMachine.GetEnemyState()==EnemyStates.CHASE)
            {
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;
                spawnDirection= pathService.GetDirections(currentNodeID, nodeID);
                Vector3 rot=GetRotation(spawnDirection);
                currentEnemyView.SetRotation(rot);

            }
            if (CheckForPlayerPresence(nodeID))
            {
                if(!currentEnemyService.CheckForKillablePlayer())
                {
                    return;
                }
                Vector3 rot = GetRotation(spawnDirection);
                currentEnemyView.SetRotation(rot);
                currentEnemyView.MoveToLocation(pathService.GetNodeLocation(nodeID));
                currentNodeID = nodeID;
                currentEnemyService.TriggerPlayerDeath();
            }
        }

    }
}