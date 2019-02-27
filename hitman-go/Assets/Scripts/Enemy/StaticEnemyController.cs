using Common;
using PathSystem;
using Player;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class StaticEnemyController : EnemyController
    {
        //readonly PlayerDeathSignal _playerDeathSignal;

        public StaticEnemyController(IEnemyService _enemyService, IPathService _pathService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService, _pathService, _spawnLocation, _enemyScriptableObject, currentNodeID, spawnDirection)
        {
            SpawnEnemyView();

        }
        protected override void MoveToNextNode(int nodeID)
        {
            //if in range()
            //
            if (CheckForPlayerPresence(nodeID))
            {
                currentEnemyView.GetGameObject().transform.position = pathService.GetNodeLocation(nodeID);
                currentEnemyService.TriggerPlayerDeath();
            }

        }

    }
}