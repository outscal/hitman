using UnityEngine;
using PathSystem;
using Zenject;
using Player;
using Common;
using System.Collections;

namespace Enemy
{
    public class StaticEnemyController : EnemyController
    {
        //readonly PlayerDeathSignal _playerDeathSignal;

        public StaticEnemyController(IEnemyService _enemyService, IPathService _pathService, Vector3 _spawnLocation, EnemyScriptableObject _enemyScriptableObject, int currentNodeID, Directions spawnDirection) : base(_enemyService,_pathService, _spawnLocation, _enemyScriptableObject,currentNodeID,spawnDirection)
        {
           // SpawnEnemyView();

        }
        protected override void MoveToNextNode(int nodeID)
        {
            //
            //currentEnemyView.GetGameObject().transform.position = pathService.GetNodeLocation(nodeID);
           Debug.Log("node id "+ nodeID);
        }

    }
}